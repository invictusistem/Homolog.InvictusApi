using AutoMapper;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Application.FinanceiroAppication.interfaces;
using Invictus.Application.Queries.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Financeiro.Fornecedor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/adm")]
    public class AdministrativoController : ControllerBase
    {
        private readonly IModuloQueries _moduloQueries;
        private readonly IMatriculaQueries _matQueries;
        private readonly IAlunoQueries _alunoQueries;
        private readonly IFinanceiroApp _financeiroApp;
        private readonly IMapper _mapper;
        private readonly InvictusDbContext _db;
        private readonly IHttpContextAccessor _userHttpContext;
        private readonly string unidade;

        public AdministrativoController(IModuloQueries moduloQueries, IAlunoQueries alunoQueries, IMatriculaQueries matQueries,
            InvictusDbContext db, IMapper mapper, IHttpContextAccessor userHttpContext, IFinanceiroApp financeiroApp)
        {
            _moduloQueries = moduloQueries;
            _alunoQueries = alunoQueries;
            _matQueries = matQueries;
            _db = db;
            _mapper = mapper;
            _userHttpContext = userHttpContext;
            unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            _financeiroApp = financeiroApp;
        }

        #region GET

        [HttpGet]
        [Route("modulos/{unidadeCode}")]
        public async Task<ActionResult<IEnumerable<ModuloDto>>> GetModulos(string unidadeCode)
        {
            var modulos = await _moduloQueries.GetModulos(unidadeCode);

            return Ok(modulos);
        }

        [HttpGet]
        [Route("turma-create")]
        public async Task<ActionResult> TurmaCreate()
        {
            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();

            var modulos = await _moduloQueries.GetModulosCreateTurma(unidadeId);//  _db.Modulos.Where(m => m.Unida deId == unidadeId).ToListAsync(); // set typePacote desc
            if (modulos.Count() == 0) return NotFound(new { mensagem = "Não há pacotes cadastrados para poder abrir uma turma. Por favor, cadastre um pacote." });
            var salas = await _db.Salas.Where(s => s.UnidadeId == unidadeId).ToListAsync();
            if (salas.Count() == 0) return NotFound(new { mensagem = "Não há salas cadastradas para essa unidade. Por favor, cadastrar ao menos uma para pode abrir uma turma." });
            var planos = await _db.Planos.Where(s => s.UnidadeId == unidadeId).ToListAsync();

            var modulosDto = _mapper.Map<List<ModuloDto>>(modulos);

            var salasDato = _mapper.Map<List<SalaDto>>(salas);

            var planosDto = _mapper.Map<List<PlanoPagamentoDto>>(planos);

            var turmaCreateViewModel = new TurmaCreateViewModel();
            turmaCreateViewModel.modulos = modulosDto;
            turmaCreateViewModel.salas = salasDato;
            turmaCreateViewModel.planos = planosDto;
            // TODO informar caso nao tenha módulos cadastrados OU salas nao criadas
            return Ok(turmaCreateViewModel);
        }



        [HttpGet]
        [Route("aluno/{alunoId}")]
        public async Task<ActionResult<AlunoDto>> GetAluno(int alunoId)
        {
            //Thread.Sleep(3000);
            var aluno = await _alunoQueries.GetAluno(alunoId);// await _db.Alunos.Include(c => c.Responsaveis).Where(c => c.Id == alunoId).FirstOrDefaultAsync();//   .Include.FindAsync(alunoId);
                                                              //var resps = _db.Responsaveis.Where(r => r.AlunoId == alunoId).ToList().Select(r => r.TipoResponsavel);

            return Ok(aluno);
        }

        [HttpGet]
        [Route("aluno/email/{email}")]
        public async Task<ActionResult<AlunoDto>> SearchEmail(string email)
        {
            //Thread.Sleep(3000);
            var pessoa = await _matQueries.SearchEmail(email);

            if (pessoa.Count() > 0) return Conflict();

            return Ok();
        }

        [HttpGet]
        [Route("aluno/cpf/{cpf}")]
        public async Task<ActionResult<AlunoDto>> SearchCPF(string cpf)
        {
            //Thread.Sleep(3000);
            var pessoa = await _matQueries.SearchCPF(cpf);

            if (pessoa.Count() > 0) return Conflict();

            return Ok();
        }

        [HttpGet]
        [Route("fornecedor")]
        public async Task<ActionResult> GetFornecedores([FromQuery] string query)
        {
            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).ToListAsync();
            var param = JsonConvert.DeserializeObject<QueryDto>(query);

            var fornecedores = await _financeiroApp.SearchFornecedor(param);

            return Ok(fornecedores);
        }

        #endregion

        #region POST

        [HttpPost]
        [Route("fornecedor")]
        public IActionResult SaveFornecedor([FromBody] FornecedorDto model)
        {
            var fornecedor = _mapper.Map<Fornecedor>(model);

            var unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();

            fornecedor.SetUnidadeId(unidadeId);

            fornecedor.ActiveFornecedor();

            _db.Fornecedores.Add(fornecedor);

            _db.SaveChanges();

            return Ok();
        }

        #endregion

        #region PUT
        [HttpPut]
        [Route("aluno/{alunoId}")]
        public IActionResult UpdateAluno(int alunoId, [FromBody] AlunoDto model)
        {

            /// var aluno = _db.Alunos.Find(alunoId);
            // model.ApplyTo(aluno);
            var aluno = _mapper.Map<AlunoDto, Aluno>(model);
            _db.Alunos.Update(aluno);
            //var aluno = _mapper.Map<AlunoDto, Aluno>(alunoEdit);
            //var aluno = await _alunoQueries.GetAluno(alunoId);// await _db.Alunos.Include(c => c.Responsaveis).Where(c => c.Id == alunoId).FirstOrDefaultAsync();//   .Include.FindAsync(alunoId);
            //var resps = _db.Responsaveis.Where(r => r.AlunoId == alunoId).ToList().Select(r => r.TipoResponsavel);

            // _db.Alunos.Update(aluno);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("fornecedor")]
        public IActionResult EditFornecedor([FromBody] FornecedorDto editedForcenedor)
        {
            var fornecedor = _mapper.Map<Fornecedor>(editedForcenedor);

            _db.Fornecedores.Update(fornecedor);

            _db.SaveChanges();

            return Ok();
        }

        #endregion
    }


}

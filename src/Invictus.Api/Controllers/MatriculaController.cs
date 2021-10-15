using AutoMapper;
using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using Invictus.Core;
using Invictus.Core.Enums;
using Invictus.Data.Context;
using Invictus.Domain;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.AlunoAggregate.Interfaces;
using Invictus.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/matricula")]
    public class MatriculaController : ControllerBase
    {
        private readonly IMatriculaQueries _matriculaQueries;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _userHttpContext;
        private InvictusDbContext _db;
        private readonly string unidade;
        public MatriculaController(IMatriculaQueries matriculaQueries, IAlunoRepository alunoRepository, IMapper mapper, InvictusDbContext db,
            IHttpContextAccessor userHttpContext)
        {
            _matriculaQueries = matriculaQueries;
            _alunoRepository = alunoRepository;
            _mapper = mapper;
            _db = db;
            _userHttpContext = userHttpContext;
            unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
        }

        #region GET
        [HttpGet]
        //public async Task<IActionResult> BuscarAluno([FromQuery] string email, [FromQuery] string cpf, [FromQuery] string nome)
        public async Task<IActionResult> BuscarAluno([FromQuery] string query)
        {


            //var param = JsonConvert.DeserializeObject<QueryDto>(query);
            //param.cpf = param.cpf.Replace(".", "").Replace("-", "");
            var cpf = await _db.Alunos.Where(a => a.CPF == query).ToListAsync();

            if (cpf.Count() > 0) return Ok(new { message = "Já existe um cadastro com o CPF informado." });

            return Ok(new { message = "" });
        }

        [HttpGet]
        [Route("alunos")]        
        public async Task<ActionResult<PaginatedItemsViewModel<AlunoDto>>> BuscarCadastroAluno([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var parametros = JsonConvert.DeserializeObject<ParametrosDTO>(paramsJson);
            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();

            //var param = JsonConvert.DeserializeObject<QueryDto>(query);
            //var pessoas = await _queries.GetColaboradores(itemsPerPage, currentPage, parametros, unidadeId);

            var pessoas = await _matriculaQueries.BuscaAlunos(itemsPerPage, currentPage, parametros, unidadeId);

            //BindCPF(alunos: ref pessoas.Data);

            return Ok(pessoas);
        }

        #endregion
        //[HttpPost]
        //public IActionResult SalvarCadastroAluno([FromBody] AlunoDto novoCadastro )
        [HttpPost]
        public IActionResult SalvarCadastroAluno([FromBody] FormData novoCadastro)
        {
            var unidadeUsuario = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            var unidadeId = _db.Unidades.Where(u => u.Sigla == unidadeUsuario).Select(s => s.Id).SingleOrDefault();
            novoCadastro.alunoDto.unidadeCadastrada = unidadeId;

            var aluno = _mapper.Map<Aluno>(novoCadastro.alunoDto);
            aluno.AtivarAluno();
            aluno.SetDataCadastro();



            aluno.CreateList();

            var EhMenor = aluno.EhMenor(novoCadastro.alunoDto.nascimento);

            if (EhMenor)
            {
                var respMenor = _mapper.Map<Responsavel>(novoCadastro.respMenorDto);
                aluno.SetRespMenor(true);
                //var respMenor = new Responsavel(0, novoCadastro.respMenorDto.nome, novoCadastro.respMenorDto.cpf, novoCadastro.respMenorDto.rg, novoCadastro.respMenorDto.nascimento, novoCadastro.respMenorDto.naturalidade,
                //    novoCadastro.respMenorDto.naturalidadeUF, novoCadastro.respMenorDto.email, novoCadastro.respMenorDto.telCelular, novoCadastro.respMenorDto.telResidencial, novoCadastro.respMenorDto.telWhatsapp,
                //    novoCadastro.respMenorDto.cep, novoCadastro.respMenorDto.logradouro, novoCadastro.respMenorDto.complemento, novoCadastro.respMenorDto.cidade, novoCadastro.respMenorDto.uf, novoCadastro.respMenorDto.bairro,
                //    novoCadastro.respMenorDto.observacoes);
                //respMenor
                respMenor.SetTipoResponsavel(TipoResponsavel.ResponsavelMenor);
                //aluno.Responsaveis.Add(respMenor);
                aluno.AddResponsavel(respMenor);

                if (!novoCadastro.respMenorDto.eRespFinanc)
                {
                    var respFinanc = _mapper.Map<Responsavel>(novoCadastro.respAlunoDto);
                    respFinanc.SetTipoResponsavel(TipoResponsavel.ResponsavelFinanceiro);
                    aluno.AddResponsavel(respFinanc);
                    aluno.SetRespFin(true);
                }
                else
                {
                    aluno.SetRespFin(false);
                }

            }
            else
            {
                aluno.SetRespMenor(false);
                if (novoCadastro.alunoDto.temRespFin)
                {
                    var respFinanc = _mapper.Map<Responsavel>(novoCadastro.respAlunoDto);
                    respFinanc.SetTipoResponsavel(TipoResponsavel.ResponsavelFinanceiro);
                    aluno.Responsaveis.Add(respFinanc);
                    aluno.SetRespFin(true);
                }
                aluno.SetRespFin(false);
            }

            var alunoId = _alunoRepository.AddAluno(aluno);

            //var dto = new RespFinancDto();
            return Ok(new { alunoCriado = alunoId });
        }

        [HttpGet]
        [Route("alunosidsmatriculas")]
        public async Task<IActionResult> IdsAlunosMatriculados()
        {
            var ids = await _db.Matriculados.Select(m => m.AlunoId).ToListAsync();

            return Ok(new { ids = ids });
        }

        [HttpPost]
        [Route("juntasdocaluno")]
        public async Task<IActionResult> JuntarDocsAluno()
        {
            var ids = await _db.Matriculados.Select(m => m.AlunoId).ToListAsync();

            return Ok(new { ids = ids });
        }

        public void BindCPF(ref List<AlunoDto> alunos)
        {
            foreach (var item in alunos)
            {

                string substr = item.cpf.Substring(6, 3);
                item.cpf = "******." + substr + "-**";
            }

        }
    }
}

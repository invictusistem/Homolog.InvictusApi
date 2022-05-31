using Invictus.Application.AdmApplication;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.ReportService;
using Invictus.Application.ReportService.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/contrato")]
    [Authorize]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoQueries _contratoQuery;
        private readonly IContratoApplication _contratoApplication;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAspNetUser _aspNetUser;
        private readonly IReportServices _reportService;
        public ContratoController(IContratoQueries contratoQuery,
                                 IContratoApplication contratoApplication,
                                 IUnidadeQueries unidadeQueries,
                                 IAspNetUser aspNetUser,
                                 IReportServices reportService
            )
        {
            _contratoQuery = contratoQuery;
            _contratoApplication = contratoApplication;
            _unidadeQueries = unidadeQueries;
            _aspNetUser = aspNetUser;
            _reportService = reportService;
        }

        [HttpGet]
        //[Route("contrato")]
        public async Task<IActionResult> GetContratos()
        {
            var contratos = await _contratoQuery.GetContratosViewModel();//.CreateUnidade(command);

            return Ok(new { contratos = contratos });
        }

        [HttpGet]
        [Route("{contratoId}")]
        public async Task<ActionResult> GetContratoById(Guid contratoId)
        {

            var contrato = await _contratoQuery.GetContratoById(contratoId);

            return Ok(new { contrato = contrato });

        }

        [HttpGet]
        [Route("exemplo/{contratoId}")]
        public async Task<IActionResult> GetExemploContratoPDF(Guid contratoId)
        {
            //var usuarioId = _aspNetUser.ObterUsuarioId();
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();
            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);
            // var colaborador = await _colabQueries.GetColaboradoresById(usuarioId);
            //var aluno = await _alunoQueries.GetAlunoById(_alunoId);
            //var menorDeIdade = await _alunoQueries.GetIdadeAluno(_alunoId);
            //int age = 0;
            //age = DateTime.Now.Subtract(menorDeIdade).Days;
            //age = age / 365;
            //var menor = true;
            //if (age >= 18) menor = false;

            //if (menor)
            //{
            //    _temRespMenor = true;
            //}

            //if (_command.temRespFin)
            //{
            //    _temRespFinanc = true;
            //}

            var infosToPrintPDF = new GenerateContratoDTO()
            {
                nome = "",
                cpf = "",
                cnpj = unidade.cnpj,
                bairro = unidade.bairro,
                complemento = unidade.complemento,
                logradouro = unidade.logradouro,
                numero = unidade.numero,
                cidade = unidade.cidade,
                uf = unidade.uf

            };

            var contratoFile = await _reportService.GenerateContratoExemplo(infosToPrintPDF, contratoId);

            //var doc = await _reportService.GeneratePendenciaDocs(matriculaId);

            var memory = new MemoryStream(contratoFile);

            return File(memory, "application/pdf", "contrato-exemplo.pdf");


            //var doc = new AlunoDocumento(_newMatriculaId, "contrato", "contrato", true, true, true, 0, _turmaId);
            //doc.AddDocumento(contratoFile, "contrato", ".pdf", "application/pdf", contratoFile.Length);
            //doc.SetDataCriacao();
            //doc.SetDocClassificacao(ClassificacaoDoc.Outros);

            //await _alunoRepo.SaveAlunoDoc(doc);

            //return Ok(new { contratos = contratos });
        }

        [HttpGet]
        [Route("type-pacote/{typePacoteId}")]
        public async Task<ActionResult> GetContratoByTypePacote(Guid typePacoteId, [FromQuery]bool ativo)
        {

            var contratos = await _contratoQuery.GetContratoByTypePacote(typePacoteId, ativo);

            if (!contratos.Any()) return NotFound();

            return Ok(new { contratos = contratos });

        }

        [HttpPost]
        //[Route("contrato")]
        public async Task<IActionResult> SaveContrato([FromBody] ContratoDto newContrato)
        {
            await _contratoApplication.AddContrato(newContrato);

            return Ok();
        }

        [HttpPut]
       // [Route("contrato")]
        public async Task<IActionResult> EditContrato([FromBody] ContratoDto editedContrato)
        {
            await _contratoApplication.EditContrato(editedContrato);

            return Ok();
        }
    }
}

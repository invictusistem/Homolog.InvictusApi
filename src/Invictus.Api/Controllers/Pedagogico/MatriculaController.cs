using Dapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.RegistroMatricula;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Padagogico.NotasTurmas;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Pedagogico
{
    [Route("api/pedag/matricula")]
    [Authorize]
    [ApiController]
    public class MatriculaController : ControllerBase
    {

        private readonly IMatriculaQueries _matriculaQueries;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IMatriculaApplication _matriculaApplication;
        private readonly IPedagMatriculaQueries _pedagMatriculaQueries;
        private readonly IAspNetUser _aspNetUser;
        private readonly IConfiguration _config;
        private readonly IUtils _utils;
        private readonly InvictusDbContext _db;
        public MatriculaController(IMatriculaQueries matriculaQueries, IMatriculaApplication matriculaApplication, IPedagMatriculaQueries pedagMatriculaQueries,
            IUtils utils, InvictusDbContext db, IAspNetUser aspNetUser, IConfiguration config, ITurmaQueries turmaQueries)
        {
            _matriculaQueries = matriculaQueries;
            _matriculaApplication = matriculaApplication;
            _pedagMatriculaQueries = pedagMatriculaQueries;
            _aspNetUser = aspNetUser;
            _utils = utils;
            _db = db;
            _config = config;
            _turmaQueries = turmaQueries;
        }



        [HttpGet]
        [Route("{alunoId}")]
        public async Task<IActionResult> GetTypeLiberadosParaMatricula(Guid alunoId)
        {
            var types = await _matriculaQueries.GetTypesLiberadorParaMatricula(alunoId);

            if (types.Count() == 0) return NotFound();

            return Ok(new { types = types });
        }

        [HttpGet]
        [Route("anotacao/{matriculaId}")]
        public async Task<IActionResult> GetInformacoesAluno(Guid matriculaId)
        {

            var anotacoes = await _pedagMatriculaQueries.GetAnotacoesMatricula(matriculaId);


            return Ok(new { anotacoes = anotacoes });
        }

        [HttpGet]
        //[Route("anotacao")]
        public async Task<IActionResult> GetMatriculadosFromUnidade()
        {
            var matriculados = await _pedagMatriculaQueries.GetMatriculadosFromUnidade();

            return Ok(new { matriculados = matriculados });
        }

        [HttpGet]
        [Route("aluno-indicacao")]
        public async Task<IActionResult> GetAlunosIndicacao()
        {
            // cadastro aluno baseado na matricula

            var alunos = await _pedagMatriculaQueries.GetAlunosIndicacao();



            return Ok(new { alunos = alunos });
        }
        #region TRANSFERENCIA
        [HttpGet]
        [Route("transf-turma/{matricula}")]
        public async Task<IActionResult> GetMatriculaTransfTurma(string matricula)
        {
            // cadastro aluno baseado na matricula

            var aluno = await _pedagMatriculaQueries.GetMatriculaByNumeroMatricula(matricula);

            if (aluno == null) return NotFound();

            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            if (unidadeId != aluno.unidadeId) return NotFound();

            var turmas = await _db.Turmas.Where(t => t.UnidadeId == unidadeId & t.Id != aluno.turmaId).ToListAsync();

            return Ok(new { aluno = aluno, turmas = turmas });
        }



        [HttpGet]
        [Route("transf-unidade/{matricula}")]
        public async Task<IActionResult> GetAlunosIndicacao(string matricula)
        {
            var matriculaAluno = await _db.Matriculas.Where(m => m.NumeroMatricula == matricula).SingleOrDefaultAsync();

            if (matriculaAluno == null) return NotFound();

            var validar = await _db.Boletos.Where(b => b.PessoaId == matriculaAluno.Id & b.StatusBoleto == StatusPagamento.Vencido.DisplayName).ToListAsync();

            var podeTransf = true;

            if (validar.Any()) podeTransf = false;

            var aluno = await _pedagMatriculaQueries.GetMatriculaByNumeroMatricula(matricula);

            var unidades = await _db.Unidades.Where(u => u.Id != aluno.unidadeId).ToListAsync();

            if (aluno == null) return NotFound();

            return Ok(new { aluno = aluno, podeTransf = podeTransf, unidades = unidades });
        }

        [HttpGet]
        [Route("transf-unidade-turmas/{matriculaId}/{unidadeId}")]
        public async Task<IActionResult> GetTurmasTransfs(Guid matriculaId, Guid unidadeId)
        {
            var matriculaAtual = await _db.Matriculas.Where(m => m.Id == matriculaId).SingleOrDefaultAsync();

            var turmas = await _db.Turmas.Where(t => t.Id != matriculaAtual.TurmaId & t.UnidadeId != unidadeId
            & (t.StatusAndamento == StatusTurma.AguardandoInicio.DisplayName || t.StatusAndamento == StatusTurma.EmAndamento.DisplayName)).ToListAsync();

            if (!turmas.Any()) return NotFound();

            return Ok(new { turmas = turmas });
        }

        private async Task<List<ListaPresencaDto>>  GetPresencaAntiga(Guid matriculaId)
        {
            var query = @"Select 
                        Calendarios.MateriaId,
                        TurmasPresencas.IsPresent
                        from Calendarios
                        inner join TurmasPresencas on Calendarios.Id = TurmasPresencas.CalendarioId
                        WHERE TurmasPresencas.MatriculaId = @matriculaId ";



            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<ListaPresencaDto>(query, new { matriculaId = matriculaId });

                connection.Close();

                return result.ToList();
            }
        }


        [HttpPut]
        [Route("transf-turma/{matriculaId}/{newTurmaId}")]
        public async Task<IActionResult> TransfTurma(Guid matriculaId, Guid newTurmaId)
        {
            // VerificarSeTemVagaNaTurmaDestino()

            

            // EfeticarTransfeRenciaDocumentosAluno()
            var alunosDocumentos = await _db.AlunosDocs.Where(a => a.MatriculaId == matriculaId).ToListAsync();
            foreach (var doc in alunosDocumentos)
            {
                doc.TransfTurma(newTurmaId);
            }
            
            _db.AlunosDocs.UpdateRange(alunosDocumentos);

            // EfetivarTransferenciaTurmaMatricuia()
            var matricula = await _db.Matriculas.FindAsync(matriculaId);

            matricula.TransfTurma(newTurmaId);

            _db.Matriculas.Update(matricula);


            // TransferiarTurmaNotas()
            /*
             NOTAS:
                CRIAR NOVA NOTAS DO ALUNO
                loop nas antigas, uma por uma ve se na nova tem com msm matérias e copiar notas
                salvar novas
                apagar antigas
             */

            var turmasMaterias = await _turmaQueries.GetMateriasDaTurma(newTurmaId);
            var newTurmaNotas = new List<TurmaNotas>();
            foreach (var materia in turmasMaterias)
            {
                var nota = new TurmaNotas();
                newTurmaNotas.Add(nota.CreateNotaDisciplinas(materia.nome, materia.id, matriculaId, newTurmaId));
            }


            var turmasNotasAtuais = await _db.TurmasNotas.Where(t => t.MatriculaId == matriculaId).ToListAsync();

            foreach (var nota in newTurmaNotas)
            {
                var correspondente = turmasNotasAtuais.Where(t => t.MateriaId == nota.MateriaId).SingleOrDefault();

                if(correspondente != null)
                {
                    nota.CopiarNotas(correspondente);
                }
            }

            _db.TurmasNotas.RemoveRange(turmasNotasAtuais);

            await _db.TurmasNotas.AddRangeAsync(newTurmaNotas);




            // TransferirPresenca()
            /*
             pega o calendario da turma de destiano
            pega a lista de presenca atual por materiaId e presenca
            dou um loop no calendario vejo se tem na lista de presena item com a msm materia Id
                se tiver, pego o item, crio uma nova presenca com esse valor da presenca velha
                pego a lsita da presençca velha e tiro o item da lista

             */
            var calendarios = await _db.Calendarios.Where(c => c.TurmaId == newTurmaId).ToListAsync();
            var calendarioOrder = calendarios.OrderBy(c => c.DiaAula);

            var presencas = new List<Presenca>();

            var presencaAntigas = await GetPresencaAntiga(matriculaId);

            foreach (var calendario in calendarioOrder)
            {
                var oldNota = presencaAntigas.Where(p => p.materiaId == calendario.MateriaId).FirstOrDefault();

                bool? prese = null;
                if(oldNota != null)
                {
                    prese = oldNota.isPresent;

                    presencaAntigas.Remove(oldNota);
                }
                var presenca = new Presenca(calendario.Id, prese, matricula.AlunoId, matriculaId, null);

                presencas.Add(presenca);

            }
            // primeiro pagar as antes p depois salvar as novas
            var oldPresenca = await _db.Presencas.Where(p => p.MatriculaId == matriculaId).ToListAsync();

            _db.Presencas.RemoveRange(oldPresenca);

            await _db.Presencas.AddRangeAsync(presencas);






            _db.SaveChanges();

         

            /*
             TABELA PARA MUDAR:
            Pessoas = unidadeId
            AlunosDocumentos = TurmaId
             Boletos = CentrocustoUnidadeId (só os q ainda irao vencer)
            Matriculas = TurmaId
            TurmasNotas = TurmaId
            TurmasPresencas = MUDAR JUNTO COM CALENDARIO PRA FRENTE????????? ANALISAR
             */

            /* REFACT
             NOTAS:
             CRIAR NOVA NOTAS DO ALUNO
                loop nas antigas, uma por uma ve se na nova tem com msm matérias e copiar notas
                salvar novas
                apagar antigas

            PRESENCA
            CRIAR NOVA PRESENCA ALUNO
                dar um loopr no calendario nova 
                    para cada, criar presenca nova
                    em cada ver se tem P ou F
                    
                 
             
             */

            return Ok();

        }

        [HttpPut]
        [Route("transf-unidade")]
        public async Task<IActionResult> TransfUnidade([FromBody] AnotacaoDto anotacao)
        {
            //await _matriculaApplication.SetAnotacao(anotacao);

            return Ok();

        }

        #endregion

        [HttpGet]
        [Route("relatorio")]
        public async Task<IActionResult> GetRelatorio([FromQuery] string paramJson)
        {
            var matriculas = await _pedagMatriculaQueries.GetRelatorioMatriculas(paramJson);

            if (!matriculas.Any()) return NotFound();

            return Ok(new { matriculas = matriculas });
        }

        [HttpPost]
        [Route("{turmaId}/{alunoId}")]
        public async Task<IActionResult> Matricular(Guid turmaId, Guid alunoId, [FromBody] MatriculaCommand command)
        {

            _matriculaApplication.AddParams(turmaId, alunoId, command);
            var matriculaId = await _matriculaApplication.Matricular();

            return Ok(new { matriculaId = matriculaId });

            //  return Ok();
        }

        [HttpPost]
        [Route("anotacao")]
        public async Task<IActionResult> SetAnotacao([FromBody] AnotacaoDto anotacao)
        {
            await _matriculaApplication.SetAnotacao(anotacao);

            return Ok();

        }


    }


}

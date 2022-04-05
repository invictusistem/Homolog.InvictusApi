using Invictus.Data.Context;
using Invictus.Domain.Financeiro;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/dev")]
    public class DesenvolvimentoController : ControllerBase
    {
        private readonly IUnidadeQueries _unidadeQueries;
        public UserManager<IdentityUser> UserManager { get; set; }
        private readonly InvictusDbContext _db;
        public DesenvolvimentoController(InvictusDbContext db, UserManager<IdentityUser> userMgr, IUnidadeQueries unidadeQueries)
        {
            _db = db;
            UserManager = userMgr;
            _unidadeQueries = unidadeQueries;
        }

        [HttpDelete]
        [Route("deletar-turma/{turmaId}")]
        public async Task<IActionResult> DeleteTurma(Guid turmaId)
        {
            var turmasProfs = await _db.TurmasProfessores.Where(t => t.TurmaId == turmaId).ToListAsync();
            _db.TurmasProfessores.RemoveRange(turmasProfs);

            var turmasPrevisoes = await _db.Previsoes.Where(t => t.TurmaId == turmaId).ToListAsync();

            _db.Previsoes.RemoveRange(turmasPrevisoes);


            var turmasNotas = await _db.TurmasNotas.Where(t => t.TurmaId == turmaId).ToListAsync();
            _db.TurmasNotas.RemoveRange(turmasNotas);

            var turmasMaterias = await _db.TurmasMaterias.Where(t => t.TurmaId == turmaId).ToListAsync();
            _db.TurmasMaterias.RemoveRange(turmasMaterias);

            var turmasHorarios = await _db.Horarios.Where(t => t.TurmaId == turmaId).ToListAsync();
            _db.Horarios.RemoveRange(turmasHorarios);

            var calendarios = await _db.Calendarios.Where(c => c.TurmaId == turmaId).ToListAsync();

            //List<int> Ids = searchList.Select(o => o.Id).ToList();
            //var customerList = this.GetQuery<Customer>().Any(c => Ids.Contains(c.ID)).ToList();
            var listCalendId = calendarios.Select(c => c.Id);
            var turmasPresencas = _db.Presencas.Where(p => listCalendId.Contains(p.CalendarioId)).ToList();

            _db.Presencas.RemoveRange(turmasPresencas);
            _db.Calendarios.RemoveRange(calendarios);

            var turma = await _db.Turmas.Where(c => c.Id == turmaId).FirstOrDefaultAsync();
            if (turma != null) _db.Turmas.Remove(turma);

            //await _db.SaveChangesAsync();

            var logTurma = await _db.LogTurmas.Where(t => t.TurmaId == turmaId).ToListAsync();
            _db.LogTurmas.RemoveRange(logTurma);
            _db.SaveChanges();

            /*
             Tabelas:
                Turmas
                TurmasHorarios
                TurmasMaterias
                TurmasNotas                
                TurmasPrevisoes
                TurmasProfessores
                
                Calendarios
                TurmasPresencas (alunoId - CalendarioId)
            */

            var mat = await _db.Matriculas.Where(m => m.TurmaId == turmaId).ToListAsync();

            if (mat.Any())
            {
                //remover usuario do Identity
                foreach (var item in mat)
                {
                    var aluno = await _db.Alunos.Where(a => a.Id == item.AlunoId).FirstOrDefaultAsync();
                   
                    if (!String.IsNullOrEmpty(aluno.Email))
                    {   
                        var usuario = await UserManager.FindByEmailAsync(aluno.Email);

                        if (usuario != null) await UserManager.DeleteAsync(usuario);
                    }

                }

                var listMatIds = mat.Select(c => c.Id);

                var resps = await _db.Responsaveis.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();

                var alunoAnots = await _db.AlunosAnotacoes.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();

                var estagiosMats = await _db.MatriculasEstagios.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();
                var estagiosMatsIds = estagiosMats.Select(c => c.Id);

                var estagioDocs = await _db.DocumentosEstagio.Where(m => estagiosMatsIds.Contains(m.Id)).ToListAsync();

                var logMat = await _db.LogMatriculas.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();

                var alunosDocs = await _db.AlunosDocs.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();

                var alunosPlanos = await _db.AlunoPlanos.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();
                var infoDebitos = new List<InformacaoDebito>();
                try
                {
                    infoDebitos = await _db.InformacoesDebito.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();
                }
                catch (Exception ex)
                {

                }
                var listInfoDebsIds = infoDebitos.Select(c => c.Id);

                var boletos = await _db.Boletos.Where(m => listInfoDebsIds.Contains(m.InformacaoDebitoId)).ToListAsync();

                var listBoletosId = boletos.Select(c => c.Id);
                //var logBoletos = _db.LogBoletos.Where(p => listBoletosId.Contains(p.BoletoId)).ToList();


               // _db.LogBoletos.RemoveRange(logBoletos);
                _db.Boletos.RemoveRange(boletos);
                _db.InformacoesDebito.RemoveRange(infoDebitos);

                _db.AlunoPlanos.RemoveRange(alunosPlanos);
                _db.AlunosDocs.RemoveRange(alunosDocs);

                //_db.LogMatriculas.RemoveRange(logMat);

                _db.Matriculas.RemoveRange(mat);

                _db.Responsaveis.RemoveRange(resps);

                _db.AlunosAnotacoes.RemoveRange(alunoAnots);

                _db.DocumentosEstagio.RemoveRange(estagioDocs);
                _db.MatriculasEstagios.RemoveRange(estagiosMats);

                _db.SaveChanges();
            }
            /*
            Removendo as matrículas delas
                Matriculas  (turmaId) -
                LogMatriculas (matriculaId) -
                AlunosDocumentos (matriculaId)  -
                AlunosPlanoPagamento (matriculaId) -
                InformãcoesDebitos (matriculaId) -
                Boletos (informacaoDebitoId)
                LogBoletos (boletoId)
                
             */


            return Ok();

        }


        [HttpDelete]
        [Route("deletar-aluno/{alunoId}")]
        public async Task<IActionResult> DeleteAluno(Guid alunoId)
        {
            var aluno = await _db.Alunos.Where(a => a.Id == alunoId).FirstOrDefaultAsync();

            _db.Alunos.Remove(aluno);

            await _db.SaveChangesAsync();
            // var alunoFoto = await _db.aluno.Where(a => a.a == alunoId).FirstOrDefaultAsync();

            return Ok();

        }

        [HttpDelete]
        [Route("deletar-matricula/{matId}")]
        public async Task<IActionResult> DeleteMatricula(Guid matId)
        {
            // get matricula
            var mat = await _db.Matriculas.Where(m => m.Id == matId).FirstOrDefaultAsync();
            /*
             -TurmasPresencas (alunoId e CalendId-DA TURMA)
             -TurmasNotas (matriculaId)
             Responsaveis (matriculaId)
             LogMatriculas (matriculaId)
             LogBoleos (boletoId)
             InformacoesDebitos (matriculaId)
             EstagiosMatricula (matriculaId)
             Boletos (informacoesDebitoId)
             AlunosDocumentos
             */
          
            var calendarios = await _db.Calendarios.Where(c => c.TurmaId == mat.TurmaId).ToListAsync();

            var listCalendId = calendarios.Select(c => c.Id);
            var turmasPresencas = _db.Presencas.Where(p => listCalendId.Contains(p.CalendarioId) & p.AlunoId == mat.AlunoId).ToList();
            _db.Presencas.RemoveRange(turmasPresencas);
            var turmasNotas = await _db.TurmasNotas.Where(t => t.MatriculaId == mat.Id).ToListAsync();
            _db.TurmasNotas.RemoveRange(turmasNotas);

          

            await _db.SaveChangesAsync();


            /*
             Tabelas:
                Turmas
                TurmasHorarios
                TurmasMaterias
                TurmasNotas                
                TurmasPrevisoes
                TurmasProfessores
                
                Calendarios
                TurmasPresencas (alunoId - CalendarioId)
            */
            /*
             - TurmasPresencas(alunoId e CalendId - DA TURMA)
             - TurmasNotas(matriculaId)
             - Responsaveis(matriculaId)
             - LogMatriculas(matriculaId)
             - LogBoleos(boletoId)
             - InformacoesDebitos(matriculaId)
             EstagiosMatricula(matriculaId)
             Estagiodocumentos 
             - Boletos(informacoesDebitoId)
             AlunosDocumentos
             AlunoAnotacoes
            */


            // var listMatIds = mat.Select(c => c.Id);

            var resps = await _db.Responsaveis.Where(m => m.MatriculaId == mat.Id).ToListAsync();

            var logMat = await _db.LogMatriculas.Where(m => m.MatriculaId == mat.Id).ToListAsync();

            var infoDebitos = await _db.InformacoesDebito.Where(m => m.MatriculaId == mat.Id).ToListAsync();

            var listInfoDebsIds = infoDebitos.Select(c => c.Id);

            var boletos = await _db.Boletos.Where(m => listInfoDebsIds.Contains(m.InformacaoDebitoId)).ToListAsync();

            //var listBoletosId = boletos.Select(c => c.Id);
            //var logBoletos = _db.LogBoletos.Where(p => listBoletosId.Contains(p.BoletoId)).ToList();


            var alunoAnots = await _db.AlunosAnotacoes.Where(m => m.MatriculaId == mat.Id).ToListAsync();

            var estagiosMats = await _db.MatriculasEstagios.Where(m => m.MatriculaId == mat.Id).ToListAsync();
            var estagiosMatsIds = estagiosMats.Select(c => c.Id);

            var estagioDocs = await _db.DocumentosEstagio.Where(m => estagiosMatsIds.Contains(m.Id)).ToListAsync();



            var aluno = await _db.Alunos.Where(a => a.Id == mat.AlunoId).FirstOrDefaultAsync();

            if (!String.IsNullOrEmpty(aluno.Email))
            {
                var usuario = await UserManager.FindByEmailAsync(aluno.Email);

                if (usuario != null) await UserManager.DeleteAsync(usuario);
            }

            var alunosDocs = await _db.AlunosDocs.Where(m => m.MatriculaId == mat.Id).ToListAsync();

            var alunosPlanos = await _db.AlunoPlanos.Where(m => m.MatriculaId == mat.Id).ToListAsync();
           
            


            //_db.LogBoletos.RemoveRange(logBoletos);
            _db.Boletos.RemoveRange(boletos);
            _db.InformacoesDebito.RemoveRange(infoDebitos);

            _db.AlunoPlanos.RemoveRange(alunosPlanos);
            _db.AlunosDocs.RemoveRange(alunosDocs);

            //_db.LogMatriculas.RemoveRange(logMat);

            _db.Matriculas.RemoveRange(mat);

            _db.Responsaveis.RemoveRange(resps);

            _db.AlunosAnotacoes.RemoveRange(alunoAnots);

            _db.DocumentosEstagio.RemoveRange(estagioDocs);
            _db.MatriculasEstagios.RemoveRange(estagiosMats);

            try
            {
                _db.SaveChanges();
            }catch(Exception ex)
            {

            }

            /*
            Removendo as matrículas delas
                Matriculas  (turmaId) -
                LogMatriculas (matriculaId) -
                AlunosDocumentos (matriculaId)  -
                AlunosPlanoPagamento (matriculaId) -
                InformãcoesDebitos (matriculaId) -
                Boletos (informacaoDebitoId)
                LogBoletos (boletoId)
                
             */


            return Ok();

        }

        [HttpPut]
        [Route("atualizar-boletos")]
        public async Task<IActionResult> AtualizarBoletos()
        {
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            var boletos = await _db.Boletos.Where(b => b.Vencimento < hoje &
                                            b.StatusBoleto == "Em aberto").ToListAsync();

            foreach (var boleto in boletos)
            {
                boleto.SetBoletoVencido();
            }

            _db.Boletos.UpdateRange(boletos);

            _db.SaveChanges();

            return Ok();
        }
    }
}

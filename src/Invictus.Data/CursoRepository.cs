using Invictus.Core;
using Invictus.Core.Util;
using Invictus.Data.Context;
using Invictus.Domain;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Pedagogico;
using Invictus.Domain.Pedagogico.TurmaAggregate;
using Invictus.Domain.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.EF;

namespace Invictus.Data
{
    public class CursoRepository : ICursoRepository
    {
        private readonly InvictusDbContext _db;
        public CursoRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public void AddAlunoTurma(LivroMatricula alunoTurma, int idTurma, string cienciaCurso)
        {
            //_db.AlunosTurma.Add(alunoTurma);
            ////_db.SaveChanges();

            //var aluno = _db.Alunos.Find(alunoTurma.AlunoId);
            //aluno.SetCienciaDoCurso(cienciaCurso);
            //_db.Alunos.Update(aluno);

            //var turma = _db.Turmas.Find(idTurma);
            //turma.AddAlunoNaTurma();

            //_db.Turmas.Update(turma);
            //_db.SaveChanges();

            //var historico = new HistoricoAluno(0, aluno.Id, idTurma);
            //var listaNota = new List<ListaNotas>();

            //var materias = _db.Materias.Where(m => m.ModuloId == turma.ModuloId);
            //foreach (var item in materias)
            //{
            //    listaNota.Add(new ListaNotas(0,null, null, null, item.Descricao,null, null, null, 0));
            //}

            //historico.AddListaNotas(listaNota);

            //_db.HistoricosAlunos.Add(historico);
            //_db.SaveChanges();

        }

        public Turma AddCurso(Turma turma)
        {
            _db.Turmas.Add(turma);
            _db.SaveChanges();

            //List<Tuple<int, int, int>> professoresTurma = new List<Tuple<int, int, int>>();

            //foreach (var item in professoresIds)
            //{
            //    professoresTurma.Add(new Tuple<int, int, int>(0, item, turma.Id));
            //}

            //_db.ProfessoresTurma.AddRange(professoresTurma);
            //_db.SaveChanges();
            return turma;
        }

        //public void AddProfsCurso(List<Tuple<int, int, int>> addProfsCommmands)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddProfsCurso(List<Tuple<int, int, int>> addProfsCommmands)
        //{
        //    _db.ProfessoresTurma.AddRange(addProfsCommmands);
        //    _db.SaveChanges();
        //}

        public void AdiarInicio(int turmaId)
        {

            //Item item = db.Items
            //  .Include(i => i.Category)
            //  .Include(i => i.Brand)
            //  .FirstOrDefault(x => x.ItemId == id);

            var turma = _db.Turmas.Find(turmaId);
            //var previsoes = _db.Find(Previsoes).pr
            //var contacts = context.Contacts.OrderBy(contact => EF.Property<DateTime>(contact, "LastUpdated"));
            var previsoes = _db.Previsoes.FromSqlRaw("Select * from Previsoes where turmaId = {0}", turmaId).SingleOrDefault();
                    //.SqlQuery("Select * from Previsoes where turmaId = @id", new SqlParameter("@id", turmaId))
                    //.FirstOrDefault();

            ///var previsoes = _db.Previsoes.OrderBy(contact => Property<int>(contact, "TurmaId")).SingleOrDefault();
            //_db..Entry(previsoes).Property("TurmaId").CurrentValue = turma.Id;
            turma.AdiarInicio(turma.Previsao, previsoes);

            _db.Turmas.Update(turma);
            _db.SaveChanges();
        }

        public void DeleteCurso(int cursoId)
        {
            var curso = _db.Turmas.Find(cursoId);
            _db.Turmas.Remove(curso);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void ExcluirProfessorTurma(int profId, int turmaId)
        {
            var profTurma = _db.ProfessoresNew.Where(p => p.ProfId == profId & p.TurmaId == turmaId).SingleOrDefault();

            _db.MateriasDaTurma.Where(m => m.ProfessorId == profTurma.Id).DeleteFromQuery();

            _db.ProfessoresNew.Remove(profTurma);
            //var data = _db.ProfessoresTurma.Where(c => c.Item2 == profId & c.Item3 == turmaId).SingleOrDefault();
            //_db.ProfessoresTurma.Remove(data);
            _db.SaveChanges();

            //var profTurma = _db.ProfessoresMaterias.Where(c => c.ProfId == profId & c.TurmaId == turmaId).ToList();

            //foreach (var prof in profTurma)
            //{
            //    prof.RemoveProfsFromTurma();
            //}

            //_db.ProfessoresMaterias.UpdateRange(profTurma);
            //_db.SaveChanges();
        }

        public void IniciarTurma(int turmaId)
        {

            var turma = _db.Turmas.Find(turmaId);
            turma.IniciarTurma();
            _db.Turmas.Update(turma);
            _db.SaveChanges();

            // var horarios = _db.HorariosBase.FromSqlRaw("Select * from HorarioBase where turmaId = {0}", turmaId).SingleOrDefault();

            //// TODO: mudar previsao atual ta tabela Turma
            // var calendarioAtual = _db.Calendarios.FromSqlRaw("Select * from Calendarios where turmaId = {0} and diaaula > {1} order by diaaula asc", turmaId, DateTime.Now).ToList();

            // if (horarios.WeekDayTwo == null)
            // {
            //     var dataFinal = calendarioAtual[0].DiaAula.AddMonths(20);
            //     var calendario = new Calendario();
            //     var datas = calendario.GerarDias(calendarioAtual[0].DiaAula, dataFinal, horarios.WeekDayOne, null);
            //     var calendarios = new List<Calendario>();
            //     calendario.SetDatas(datas, calendarios);
            //     calendario.SetSala("Sala 1", calendarios);
            //     calendario.SetTurma(turma.Identificador, turma.Id, calendarios);
            //     calendario.SetUnidade("Campo Grande", calendarios);
            //     calendario.SetHorariosAndTurnos(calendarios, calendarioAtual[0].DiaDaSemana.ToString(), null, calendarioAtual[0].HoraInicial, null,
            //         calendarioAtual[0].HoraFinal, null);

            //     _db.Calendarios.RemoveRange(calendarioAtual);
            //     _db.SaveChanges();

            //     _db.Calendarios.AddRange(calendarios);
            //     _db.SaveChanges();
            // }
            // else
            // {

            // }

        }
    }
}

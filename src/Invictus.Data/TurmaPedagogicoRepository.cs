using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Pedagogico.TurmaAggregate;
using Invictus.Domain.Pedagogico.TurmaAggregate.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Invictus.Data
{
    public class TurmaPedagogicoRepository : ITurmaPedagRepository
    {
        private readonly InvictusDbContext _db;
        
        public TurmaPedagogicoRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public Aluno AddAlunoTurma(int idAluno, int idTurma, string ciencia)
        {
            var aluno = _db.Alunos.Find(idAluno);
            aluno.SetCienciaDoCurso(ciencia);
            _db.Alunos.Update(aluno);

            var turmaPedag = _db.TurmaPedag.Where(c => c.TurmaId == idTurma).SingleOrDefault();
            var livroMat = _db.LivroMatricula.Where(m => m.TurmaId == turmaPedag.Id).SingleOrDefault();

            var alunoMat = _db.LivroMatriculaAlunos.Add(new LivroMatriculaAlunos(0, idAluno, "Matriculado", livroMat.Id));

            _db.SaveChanges();

            var turma = _db.Turmas.Find(idTurma);

            turma.AddAlunoNaTurma();

            _db.SaveChanges();

            return aluno;
        }

        public void AddProfInTurma(List<int> profsIds, int turmaId)
        {
            //var turmaPedag = _db.TurmaPedag.Where(t => t.TurmaId == turmaId).SingleOrDefault();

            //var professores = new List<Professor>();
            var professores = new List<ProfessorNew>();
            foreach (var id in profsIds)
            {
                professores.Add(new ProfessorNew(0, null, null, id, turmaId));
            }

            _db.ProfessoresNew.AddRange(professores);
            _db.SaveChanges();
        }

        public void CreateMateriasPedag(List<MateriaPedag> materias)
        {
            _db.ProfMaterias.AddRange(materias);
            _db.SaveChanges();
        }

        public void CreateTurmaPedag(TurmaPedagogico turmaPedag)
        {
            _db.TurmaPedag.Add(turmaPedag);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
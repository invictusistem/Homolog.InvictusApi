using System.Collections.Generic;

namespace Invictus.Domain.Administrativo.Models
{
    public class ProfessorNew
    {
        public ProfessorNew() { }
        public ProfessorNew(
                                int id,
                                //int materiaId,
                                string nome,
                                string email,
                                int profId,
                                int turmaId
                                //int materiaId
            )
        //string turma
        //)
        {
            Id = id;
            //MateriaId = materiaId;
            Nome = nome;
            Email = email;
            ProfId = profId;
            TurmaId = turmaId;
            //MateriaId = materiaId;
            //Turma = turma;
            Materias = new List<MateriasDaTurma>();
        }
        public int Id { get; private set; }
        //public int MateriaId { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public int ProfId { get; private set; }
        public int TurmaId { get; private set; }
        //public int MateriaId { get; private set; }
        //public int MateriaId { get; private set; }
        public List<MateriasDaTurma> Materias { get; private set; }
        // public virtual List<Materia> Materias { get; private set; }

        public void RemoveProfsFromTurma()
        {
            ProfId = 0;

        }
    }
}

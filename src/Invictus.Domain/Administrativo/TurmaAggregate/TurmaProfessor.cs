using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.TurmaAggregate
{
    public class TurmaProfessor : Entity
    {
        public TurmaProfessor(
                                //int id,
                                //int materiaId,
                                //string nome,
                                // string email,
                                Guid professorId,
                                Guid turmaId
            //int materiaId
            )
        //string turma
        //)
        {
            //  Id = id;
            //MateriaId = materiaId;
            //Nome = nome;
            // Email = email;
            ProfessorId = professorId;
            TurmaId = turmaId;
            //MateriaId = materiaId;
            //Turma = turma;
          //  Materias = new List<MateriasDaTurma>();
        }
       // public int Id { get; private set; }
        //public int MateriaId { get; private set; }
       // public string Nome { get; private set; }
      //  public string Email { get; private set; }
        public Guid ProfessorId { get; private set; }
        public Guid TurmaId { get; private set; }
        //public int MateriaId { get; private set; }
        //public int MateriaId { get; private set; }
      //  public List<MateriasDaTurma> Materias { get; private set; }
        // public virtual List<Materia> Materias { get; private set; }

        public void RemoveProfsFromTurma()
        {
           /// ProfessorId = 0;

        }

        #region EF

        public TurmaProfessor() { }

        #endregion
    }
}

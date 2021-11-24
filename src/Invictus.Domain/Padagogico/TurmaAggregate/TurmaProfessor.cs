using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Padagogico.TurmaAggregate
{
    class TurmaProfessor : Entity
    {
        public TurmaProfessor() { }
        public TurmaProfessor(
                                string nome,
                                string email,
                                Guid profId,
                                Guid turmaId

            )

        {

            Nome = nome;
            Email = email;
            ProfId = profId;
            TurmaId = turmaId;

        }
       // public int Id { get; private set; }
        //public int MateriaId { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public Guid ProfId { get; private set; }
        public Guid TurmaId { get; private set; }
        //public int MateriaId { get; private set; }
        //public int MateriaId { get; private set; }
        // public List<MateriasDaTurma> Materias { get; private set; }
        // public virtual List<Materia> Materias { get; private set; }

        //public void RemoveProfsFromTurma()
        //{
        //    ProfId = 0;

        //}
    }
}
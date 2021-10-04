using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Pedagogico.TurmaAggregate
{
    public class Professor
    {
        public Professor() { }
        public Professor(
                                int id,
                                //int materiaId,
                                string nome,
                                string email,
                                int profId,
                                int materiaId)
                                //string turma
                                //)
        {
            Id = id;
            //MateriaId = materiaId;
            Nome = nome;
            Email = email;
            ProfId = profId;
            MateriaId = materiaId;
            //Turma = turma;
            // Materias = new List<Materia>();

        }
        public int Id { get; private set; }
        //public int MateriaId { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public int ProfId { get; private set; }
        public int MateriaId { get; private set; }
        public virtual MateriaPedag Materia { get; private set; }
       // public virtual List<Materia> Materias { get; private set; }

        public void RemoveProfsFromTurma()
        {
            ProfId = 0;

        }
    }
}

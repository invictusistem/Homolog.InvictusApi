using Invictus.Core.Interfaces;
using System.Collections.Generic;

namespace Invictus.Domain.Pedagogico.TurmaAggregate
{
    public class TurmaPedagogico : IAggregateRoot
    {
        public TurmaPedagogico() { }
        public TurmaPedagogico(int id,
                               int turmaId,
                               int moduloId)
        {
            Id = id;
            TurmaId = turmaId;
            ModuloId = moduloId;
            Materias = new List<MateriaPedag>();

        }
        public int Id { get; private set; }
        public int TurmaId { get; private set; }
        public int ModuloId { get; private set; }
        //public List<Professor> Professores { get; private set; }
        public List<MateriaPedag> Materias { get; private set; }
        public LivroMatricula LivroMatriculas { get; private set; }

        public void CreateTurmaPedagogico(int turmaId, int moduloId)
        {
            //var turmaPedag = new TurmaPedagogico();
            ModuloId = moduloId;
            TurmaId = turmaId;
            LivroMatriculas = new LivroMatricula();

        }
    }
}

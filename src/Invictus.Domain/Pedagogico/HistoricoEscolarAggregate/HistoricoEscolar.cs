using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate.enums;
using System.Collections.Generic;

namespace Invictus.Domain.Pedagogico.HistoricoEscolarAggregate
{
    public class HistoricoEscolar : IAggregateRoot
    {
        public HistoricoEscolar()
        {

        }
        public HistoricoEscolar(int id,
                                string aluno,
                                int alunoId,
                                int turmaId
                                )
        {
            Id = id;
            Aluno = aluno;
            AlunoId = alunoId;
            TurmaId = turmaId;
            //ListaPresencas = new List<ListaPresenca>();
            BoletinsEscolares = new List<BoletimEscolar>();

        }
        public int Id { get; private set; }
        public string Aluno { get; private set; }
        public int AlunoId { get; private set; }
        public int TurmaId { get; private set; }
        //public virtual List<ListaPresenca> ListaPresencas { get; private set; }
        public virtual List<BoletimEscolar> BoletinsEscolares { get; private set; }
        
        public void SetTurmaId(int turmaId)
        {
            TurmaId = turmaId;
        }

        public void CreateBoletimEscolar(string nomeAluno, int alunoId, int turmaId, List<string> disciplinas)
        {   
            Aluno = nomeAluno;
            AlunoId = alunoId;
            TurmaId = turmaId;

            var boletins = new List<BoletimEscolar>();

            foreach (var disciplina in disciplinas)
            {
                boletins.Add(new BoletimEscolar(0, disciplina));
            }

            BoletinsEscolares = boletins;
        }

        
        public void AddListaNotas(IEnumerable<BoletimEscolar> lista)
        {
            BoletinsEscolares.AddRange(lista);
        }
    }
}

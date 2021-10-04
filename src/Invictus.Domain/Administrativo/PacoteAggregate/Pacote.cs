using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Invictus.Domain.Administrativo.PacoteAggregate
{
    public class Pacote : IAggregateRoot
    {
        public Pacote() { }
        public Pacote(int id,
                      string descricao,
                     // string tipo,
                      int unidadeId,
                      int duracaoMeses,
                      decimal preco
                      //DateTime dataCriacao
                      )
        {
            Id = id;
            Descricao = descricao;
           //Tipo = tipo;
            UnidadeId = unidadeId;
            DuracaoMeses = duracaoMeses;
            Preco = preco;
            /// DataCriacao = DateTime.Now;// dataCriacao;
            Materias = new List<Materia>();
            Documentos = new List<DocumentacaoExigencia>();
        }

        public int Id { get; private set; }
        public string Descricao { get; private set; }
       // public string Tipo { get; private set; }
        public int DuracaoMeses { get; private set; }
        public int TotalHoras { get; private set; }

        public decimal Preco { get; private set; }
        public int UnidadeId { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public int TypePacoteId { get; private set; }
        // public virtual Unidade Unidade { get; private set; }
        public List<Materia> Materias { get; private set; }
       public List<DocumentacaoExigencia> Documentos { get; private  set; }

        public void SetCreateDate()
        {
            DataCriacao = DateTime.Now;
        }

        public void SetUnidadeId(int unidadeId)
        {
            UnidadeId = unidadeId;
        }

        public void SetTotalHours(List<Materia> materias)
        {
            int total = materias.Select(m => m.CargaHoraria).Sum();

            TotalHoras = total;
        }
    }
    
}

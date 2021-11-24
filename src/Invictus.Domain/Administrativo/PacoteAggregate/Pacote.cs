using Invictus.Core;
using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Invictus.Domain.Administrativo.PacoteAggregate
{
    public class Pacote : Entity, IAggregateRoot
    {
        public Pacote() { }
        public Pacote(//int id,
                      string descricao,
                      // string tipo,
                      Guid unidadeId,
                      //int duracaoMeses,
                      int totalHoras,
                      //decimal preco,
                      Guid typePacoteId,
                      bool ativo
                      //DateTime dataCriacao
                      )
        {
            //Id = id;
            Descricao = descricao;
            //Tipo = tipo;
            UnidadeId = unidadeId;
            //DuracaoMeses = duracaoMeses;
            TotalHoras = totalHoras;
           // Preco = preco;
            TypePacoteId = typePacoteId;// dataCriacao;
            Materias = new List<PacoteMateria>();
            Ativo = ativo;
            DocumentosExigidos = new List<DocumentacaoExigencia>();
        }

        // public int Id { get; private set; }
        public string Descricao { get; private set; }
        // public string Tipo { get; private set; }
       // public int DuracaoMeses { get; private set; }
        public int TotalHoras { get; private set; }

        
        public Guid UnidadeId { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Guid TypePacoteId { get; private set; }
        public bool Ativo { get; private set; }
        // public virtual Unidade Unidade { get; private set; }
        public List<PacoteMateria> Materias { get; private set; }
        public List<DocumentacaoExigencia> DocumentosExigidos { get; private set; }

        
        public void SetCreateDate()
        {
            DataCriacao = DateTime.Now;
        }

        public void SetUnidadeId(Guid unidadeId)
        {
            UnidadeId = unidadeId;
        }

        public void SetTotalHours(List<PacoteMateria> materias)
        {
            int total = materias.Select(m => m.CargaHoraria).Sum();

            TotalHoras = total;
        }
    }

}

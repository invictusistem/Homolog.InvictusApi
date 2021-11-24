using Invictus.Core;
using System;

namespace Invictus.Domain.Administrativo.UnidadeAggregate
{
    public class Sala : Entity
    {
        public Sala(string descricao,
                    string comentarios,
                    int capacidade,
                    //int unidadeId,
                    bool ativo)
        {
            
            Descricao = descricao;
            Comentarios = comentarios;
            Capacidade = capacidade;
            //UnidadeId = unidadeId;
            Ativo = ativo;
        }
        
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Comentarios { get; private set; }
        public int Capacidade { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public bool Ativo { get; private set; }
        public Guid UnidadeId { get; private set; }
        //EF
        public virtual Unidade Unidade { get; private set; }

        public void SetSalaTitulo(int qntSalaAtual)
        {
            Titulo = "Sala " + Convert.ToString(qntSalaAtual + 1);
        }

        public void SetDataCriacao()
        {
            DataCriacao = DateTime.Now;
        }

        public Sala(){}
    }
}
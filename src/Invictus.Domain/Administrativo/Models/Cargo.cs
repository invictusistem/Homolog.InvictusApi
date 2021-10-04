using System;

namespace Invictus.Domain.Administrativo.Models
{
    public class Cargo
    {
        public Cargo(string descricao,
                     string nome,
                     bool ativo
                     //DateTime dataCriacao
                     )
        {
            Nome = nome;
            Descricao = descricao;
            //DataCriacao = dataCriacao;
            Ativo = ativo;

        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public void SetDataCriacao()
        {
            DataCriacao = DateTime.Now;
        }

    }
}

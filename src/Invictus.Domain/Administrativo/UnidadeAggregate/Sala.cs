using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.UnidadeAggregate
{
    public class Sala
    {
        public Sala()
        {

        }
        public Sala(//int id,
                    string descricao,
                    string comentarios,
                    int capacidade,
                    //int unidadeId,
                    bool ativo
                   // DateTime dataCriacao
                    )
        {
           // Id = id;
            Descricao = descricao;
            Comentarios = comentarios;
            Capacidade = capacidade;
            //UnidadeId = unidadeId;
           // DataCriacao = SetDataCriacao();// DateTime.Now;
            Ativo = ativo;
        }
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Comentarios { get; private set; }
        public int Capacidade { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public bool Ativo { get; private set; }
        public int UnidadeId { get; private set; }
        public virtual Unidade Unidade { get; private set; }

        public void SetSalaTitulo(int qntSalaAtual)
        {
            Titulo = "Sala " + Convert.ToString(qntSalaAtual + 1);
        }

        public void SetDataCriacao()
        {
            DataCriacao = DateTime.Now;
        }
    }
}

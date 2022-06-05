using Invictus.Core;
using Invictus.Core.Interfaces;
using System;

namespace Invictus.Domain.Administrativo.Models
{
    public class ProdutoLog : Entity
    {
        public ProdutoLog(string metodo,
                            Guid userId,
                            Guid unidadeId,
                            string observacao,
                            DateTime horario)
        {
            Metodo = metodo;
            UserId = userId;
            UnidadeId = unidadeId;
            Observacao = Observacao;
            Horario = horario;
        }

        public string Metodo { get; private set; }
        public Guid UserId { get; private set; }
        public Guid UnidadeId { get; private set; }
        public string Observacao { get; private set; }
        public DateTime Horario { get; private set; }
    }
    public class Produto : Entity, IAggregateRoot
    {

        public Produto(string codigoProduto,
                        string nome,
                        string descricao,
                        decimal preco,
                        decimal precoCusto,
                        int quantidade,
                        int nivelMinimo,
                        Guid unidadeId,
                        DateTime dataCadastro,
                        bool ativo,
                        string observacoes
                        )
        {   
            CodigoProduto = codigoProduto;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            PrecoCusto = precoCusto;
            Quantidade = quantidade;
            NivelMinimo = nivelMinimo;
            UnidadeId = unidadeId;
            DataCadastro = dataCadastro;
            Ativo = ativo;
            Observacoes = observacoes;
        }
        public string CodigoProduto { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public decimal PrecoCusto { get; private set; }
        public int Quantidade { get; private set; }
        public int NivelMinimo { get; private set; }
        public Guid UnidadeId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }
        public string Observacoes { get; private set; }

        public void AddCodigoProduto(int totalProductsInDataBase)
        {
            var total = totalProductsInDataBase + 1;
            var totalChars = total.ToString().Length;
            var length = 8;
            var numeracaoToString = "";
            for (int i = 0; i < length - totalChars; i++)
            {
                numeracaoToString += "0";
            }

            numeracaoToString += total.ToString();

            CodigoProduto = numeracaoToString;
        }



        public void SetUnidadeId(Guid unidadeId)
        {
            UnidadeId = unidadeId;
        }

        public void SetDataCadastro()
        {
            DataCadastro = DateTime.Now;
        }

        public void RemoveProduto(int qnt)
        {
            Quantidade -= qnt;
        }

        public void AddProduto(int qnt)
        {
            Quantidade += qnt;
        }

    }
}
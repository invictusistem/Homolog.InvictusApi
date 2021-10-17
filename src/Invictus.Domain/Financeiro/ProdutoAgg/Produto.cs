using Invictus.Core.Interfaces;
using System;

namespace Invictus.Domain.Financeiro.NewFolder
{
    public class Produto : IAggregateRoot
    {

        public Produto(int id,
                        string codigoProduto,
                        string nome,
                        string descricao,
                        decimal preco,
                        decimal precoCusto,
                        int quantidade,
                        int nivelMinimo,
                        int unidadeId,
                        DateTime dataCadastro,
                        string observacoes
                        )
        {
            Id = id;
            CodigoProduto = codigoProduto;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            PrecoCusto = precoCusto;
            Quantidade = quantidade;
            NivelMinimo = nivelMinimo;
            UnidadeId = unidadeId;
            DataCadastro = dataCadastro;
            Observacoes = observacoes;

        }
        public int Id { get; private set; }
        public string CodigoProduto { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public decimal PrecoCusto { get; private set; }
        public int Quantidade { get; private set; }
        public int NivelMinimo { get; private set; }
        public int UnidadeId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Observacoes { get; private set; }

        public void AddCodigoProduto(string codigo)
        {
            CodigoProduto = codigo;
        }

        public void SetUnidadeId(int unidadeId)
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

using Invictus.Core.Enums;
using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Financeiro.VendaProduto
{
    public class VendaProdutoAggregate : IAggregateRoot
    {
        public VendaProdutoAggregate() { }
        public VendaProdutoAggregate(int id,
                                    DateTime dataVenda,
                                    decimal valorTotalVenda,
                                    int respVendaId,
                                    int unidadeId,
                                    string cpf_comprador,
                                    int matriculaComprador,
                                    MeioPagamento meioPagamento,
                                    string identificadorPagamento,
                                    int parcelas
                                    )
        {
            Id = id;
            DataVenda = dataVenda;
            ValorTotalVenda = valorTotalVenda;
            RespVendaId = respVendaId;
            UnidadeId = unidadeId;
            CPF_Comprador = cpf_comprador;
            MatriculaComprador = matriculaComprador;
            MeioPagamento = meioPagamento.DisplayName;
            IdentificadorPagamento = identificadorPagamento;
            Parcelas = parcelas;
            ProdutosVenda = new List<ProdutoVenda>();

        }
        public int Id {get; private set;}
        public DateTime DataVenda { get; private set; }
        public decimal ValorTotalVenda { get; private set; }
        public int RespVendaId { get; private set; }
        public int UnidadeId { get; private set; }
        public string CPF_Comprador { get; private set; }
        public int MatriculaComprador { get; private set; }
        public string MeioPagamento { get; private set; }
        public string IdentificadorPagamento { get; private set; }
        public int Parcelas { get; private set; }
        public List<ProdutoVenda> ProdutosVenda { get; private set; }

       
    }
}

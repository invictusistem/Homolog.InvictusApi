using Invictus.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Fornecedor
{
    public class FornecedorEntrada
    {
        public FornecedorEntrada() { }
        public FornecedorEntrada(long id,
        DateTime dataCriacao,
        DateTime dataVencimento,
        decimal valor,
        decimal valorComDesconto,
        //decimal valorPago,
        //DateTime diaPagamento,
        MeioPagamento meioPagamento,
        StatusPagamento statusPagamento,
        string comentario,
        string descricaoTransacao,
        long fornecedorId,
        int unidadeId

        )
        {
            Id = id;
            DataCriacao = dataCriacao;
            DataVencimento = dataVencimento;
            Valor = valor;
            ValorComDesconto = valorComDesconto;
            //ValorPago = valorPago;
            //DiaPagamento = diaPagamento;
            MeioPagamento = meioPagamento.DisplayName;
            StatusPagamento = statusPagamento.DisplayName;
            DescricaoTransacao = descricaoTransacao;
            Comentario = comentario;
            FornecedorId = fornecedorId;
            UnidadeId = unidadeId;


        }
        public long Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public decimal Valor { get; private set; }
        public decimal ValorComDesconto { get; private set; }
        public decimal ValorPago { get; private set; }
        public DateTime? DiaPagamento { get; private set; }
        public string MeioPagamento { get; private set; }
        public string StatusPagamento { get; private set; }
        public string DescricaoTransacao { get; private set; }
        public string Comentario { get; private set; }
        public long FornecedorId { get; private set; }
        public int UnidadeId { get; private set; }
    }
}

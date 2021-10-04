using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Transacoes
{
    public class TransacaoCartaoAggregate : IAggregateRoot
    {
        public TransacaoCartaoAggregate() { }
        public TransacaoCartaoAggregate(int id,
                                string status,
                                string codigoTransacao,
                                string codigoVenda,
                                string valor,
                                string taxas,
                                string totalLiquido,
                                string meioPagamento,
                                string cv,
                                string nsu,
                                string numeroSerie,
                                string aid,
                                string arqc
                                )
        {
            Id = id;
            Status = status;
            CodigoTransacao = codigoTransacao;
            CodigoVenda = codigoVenda;
            Valor = valor;
            Taxas = taxas;
            TotalLiquido = totalLiquido;
            MeioPagamento = meioPagamento;
            CV = cv;
            NSU = nsu;
            NumeroSerie = numeroSerie;
            AID = aid;
            ARQC = arqc;

        }
        public int Id { get; private set; }
        public string Status { get; private set; }
        public string CodigoTransacao { get; private set; }
        public string CodigoVenda { get; private set; }
        public string Valor { get; private set; }
        public string Taxas { get; private set; }
        public string TotalLiquido { get; private set; }
        public string MeioPagamento { get; private set; }
        public string CV { get; private set; }
        public string NSU { get; private set; }
        public string NumeroSerie { get; private set; }
        public string AID { get; private set; }
        public string ARQC { get; private set; }

        //public int MyProperty { get; set; }
    }
}

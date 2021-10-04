using Invictus.Core.Interfaces;
using System;

namespace Invictus.Domain.Financeiro.Transacoes
{
    public class TransacaoBoletoAggregate : IAggregateRoot
    {
        public TransacaoBoletoAggregate() { }
        public TransacaoBoletoAggregate(int id,
                            string status,
                            string codigoTransacao,
                            string codigoReferencia,
                            string valor,
                            string taxas,
                            string totalLiquido,
                            DateTime dataVencimento,
                            string meioPagamento,
                            string compradorNome,
                            string compradorEmail,
                            string compradorTelefone
                            )
        {
            Id = id;
            Status = status;
            CodigoTransacao = codigoTransacao;
            CodigoReferencia = codigoReferencia;
            Valor = valor;
            Taxas = taxas;
            TotalLiquido = totalLiquido;
            DataVencimento = dataVencimento;
            MeioPagamento = meioPagamento;
            CompradorNome = compradorNome;
            CompradorEmail = compradorEmail;
            CompradorTelefone = compradorTelefone;

        }

        public int Id { get; private set; }
        public string Status { get; private set; }
        public string CodigoTransacao { get; private set; }
        public string CodigoReferencia { get; private set; }
        public string Valor { get; private set; }
        public string Taxas { get; private set; }
        public string TotalLiquido { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public string MeioPagamento { get; private set; }
        public string CompradorNome { get; private set; }
        public string CompradorEmail { get; private set; }
        public string CompradorTelefone { get; private set; }
        public TransacaoBoletoEndereco Endereco { get; private set; }
      
        public void AddEndereco(TransacaoBoletoEndereco endereco)
        {
            Endereco = endereco;
        }

    }

    public class Boleto
    {
        public Boleto()
        {

        }
        public long Id { get; set; }
        public string id_unico { get; set; }
        public string id_unico_original { get; set; }
        public string status { get; set; }
        public string msg { get; set; }
        public string nossonumero { get; set; }
        public string linkBoleto { get; set; }
        public string linkGrupo { get; set; }
        public string linhaDigitavel { get; set; }
        public string pedido_numero { get; set; }
        public string banco_numero { get; set; }
        public string token_facilitador { get; set; }
        public string credencial { get; set; }

      
    }
}

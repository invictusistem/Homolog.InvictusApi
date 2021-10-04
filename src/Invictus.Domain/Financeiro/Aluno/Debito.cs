using Invictus.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Aluno
{
    public class Debito
    {
        public Debito()
        {

        }
        public Debito(int id,
                    DateTime competencia,
                    int idUnidadeResponsavel,
                    StatusPagamento status,
                    decimal valorTitulo,
                    //decimal valorPago,
                    DateTime dataVencimento,
                    MeioPagamento meioPagamento,
                    int parcelaNumero,
                    int transacaoId,
                    //DateTime dataPagamento,
                    string descricao,
                    string comentario
                    )
        {
            Id = id;
            Competencia = competencia;
            IdUnidadeResponsavel = idUnidadeResponsavel;
            Status = status.DisplayName;
            ValorTitulo = valorTitulo;
            //ValorPago = valorPago;
            DataVencimento = dataVencimento;
            MeioPagamento = meioPagamento.DisplayName;
            ParcelaNumero = parcelaNumero;
            TransacaoId = transacaoId;
            //DataPagamento = dataPagamento;
            Descricao = descricao;
            Comentario = comentario;

        }
        public int Id { get; private set; }
        public DateTime Competencia { get; private set; }
        public int IdUnidadeResponsavel { get; private set; }
        public string Status { get; private set; }
        public decimal ValorTitulo { get; private set; }
        public decimal ValorPago { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public DateTime? DataPagamento { get; private set; }
        public string MeioPagamento { get; private set; }
        public int ParcelaNumero { get; private set; }
        public int TransacaoId { get; private set; }
        public string Descricao { get; private set; }
        public string Comentario { get; private set; }
        public int BoletoId { get; private set; }
        public int InfoFinancId { get; private set; }
        public int IdDebitoOriginal { get; private set; }
        public string HistoricoDivida { get; private set; }
        public virtual InformacaoFinanceiraAggregate InformacaoFinanceiraAggregate { get; private set; }

        public void PagarBoleto()
        {
            Status = StatusPagamento.Pago.DisplayName;
            DataPagamento = DateTime.Now;
            ValorPago = ValorTitulo;
        }

        public void GerarComentario()
        {

        }

        public void SetBoletoId(int id)
        {
            BoletoId = id;
        }
        public void ChangeStatusToVencido()
        {
            Status = StatusPagamento.Vencido.DisplayName;
        }
        public void ChangeStatusToReparcelado()
        {
            Status = StatusPagamento.Reparcelado.DisplayName;// "Reparcelado";
        }

        public void SetIdDebitoOriginal(int debitoId)
        {
            IdDebitoOriginal = debitoId;
        }


    }
}

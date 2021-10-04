//using Invictus.Core.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Invictus.Domain.Financeiro.Aluno
//{
//    public class ValorEntrada
//    {
//        public ValorEntrada()
//        {

//        }
//        public ValorEntrada(
//                            int id,
//                            string descricao,
//                            decimal valor,
//                            int idUnidadeResponsavel,
//                            DateTime vencimento,
//                            MeioPagamento meioPagamento,
//                            int transacaoId
//                            //DateTime dataPagamento
//                            )
//        {
//            Id = id;
//            Descricao = descricao;
//            Valor = valor;
//            IdUnidadeResponsavel = idUnidadeResponsavel;
//            Vencimento = vencimento;
//            MeioPagamento = meioPagamento.DisplayName;
//            TransacaoId = transacaoId;
//            //DataPagamento = DataPagamento;
//        }
//        public int Id { get; private set; }
//        public string Descricao { get; private set; }
//        public decimal Valor { get; private set; }
//        public int IdUnidadeResponsavel { get; private set; }
//        public DateTime Vencimento { get; private set; }
//        public DateTime DataPagamento { get; private set; }
//        public string MeioPagamento { get; private set; }
//        public int TransacaoId { get; private set; }
//        public int InformacaoFinanceiraId { get; private set; }
//        public virtual InformacaoFinanceiraAggregate InformacaoFinanceiraAggregate { get; private set; }
//    }
//}

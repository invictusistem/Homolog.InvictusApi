using Invictus.Core;
using Invictus.Core.Enumerations;
using System;

namespace Invictus.Domain.Financeiro
{    
    public class Boleto : Entity
    {

        public Boleto(DateTime vencimento,
                    decimal valor,
                    int juros,
                    int jurosFixo,
                    string multa,
                    string multaFixo,
                    string desconto,
                    TipoLancamento tipo,
                    string diasDesconto,
                    StatusPagamento statusBoleto,
                    Guid centroCustoUnidadeId,
                    Guid responsavelCadastroId,
                    BoletoResponseInfo infoBoletos,
                    DateTime dataCadastro)
        {
            Vencimento = vencimento;
            Valor = valor;
            Juros = juros;
            JurosFixo = jurosFixo;
            Multa = multa;
            MultaFixo = multaFixo;
            Desconto = desconto;
            Tipo = tipo.DisplayName;
            DiasDesconto = diasDesconto;
            StatusBoleto = statusBoleto.DisplayName;
            CentroCustoUnidadeId = centroCustoUnidadeId;
            ResponsavelCadastroId = responsavelCadastroId;
            InfoBoletos = infoBoletos;
            DataCadastro = dataCadastro;
        }

        public DateTime Vencimento { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public decimal Valor { get; private set; }
        public decimal ValorPago { get; private set; }
        public int Juros { get; private set; }
        public int JurosFixo { get; private set; }
        public string Multa { get; private set; }
        public string MultaFixo { get; private set; }
        public string Desconto { get; private set; }
        public string Tipo { get; private set; }
        public string DiasDesconto { get; private set; }
        public string StatusBoleto { get; private set; }
        public string Historico { get; private set; }
        public string SubConta { get; private set; }
        public bool Ativo { get; private set; }
        public Guid? SubContaId { get; private set; }
        public Guid? BancoId { get; private set; }
        public Guid? CentroCustoId { get; private set; }
        public Guid? MeioPagamentoId { get; private set; }
        public string FormaPagamento { get; private set; }
        public string DigitosCartao { get; private set; }
        public bool EhFornecedor { get; private set; }
        public string TipoPessoa { get; private set; }
        public Guid PessoaId { get; private set; } // colaborador ou matricula
        public DateTime DataCadastro { get; private set; }
        public Guid ReparcelamentoId { get; private set; }
        public Guid CentroCustoUnidadeId { get; private set; }
        //public Guid InformacaoDebitoId { get; private set; }
        public Guid ResponsavelCadastroId { get; private set; }
        public BoletoResponseInfo InfoBoletos { get; private set; }

        public void InativarConta()
        {
            Ativo = false;
        }
        public void SetMeioPgm(Guid meioPgmId)
        {
            MeioPagamentoId = meioPgmId;
        }

        public void SetTipoPessoa(string pessoa)
        {
            TipoPessoa = pessoa;
        }


        public void SetCentroCustoId(Guid centroCustoId)
        {
            CentroCustoId = centroCustoId;
        }
        public static Boleto CadastrarBoletoMatriculaFactory(DateTime vencimento,
                   decimal valor,
                   //int juros,
                  // int jurosFixo,
                  // string multa,
                   //string multaFixo,
                   string desconto,
                   TipoLancamento tipo,
                   string diasDesconto,
                   //StatusPagamento statusBoleto,
                   //bool ehFornecedor,
                   Guid pessoaId,
                   Guid centroCustoUnidadeId,
                   Guid responsavelCadastroId,
                   string historico,
                   Guid? subcontaId,
                   Guid? bancoId)
        {
            var boleto = new Boleto()
            {
                Vencimento = vencimento,
                Valor = valor,
                Juros = 0,
                JurosFixo = 0,
                Multa = "",
                MultaFixo = "",
                Desconto = desconto,
                Tipo = tipo.DisplayName,
                DiasDesconto = diasDesconto,
                StatusBoleto = StatusPagamento.EmAberto.DisplayName,
                Historico = historico,
                SubContaId = subcontaId,
                BancoId = bancoId,
                EhFornecedor = false,
                PessoaId = pessoaId,
                CentroCustoUnidadeId = centroCustoUnidadeId,
                ResponsavelCadastroId = responsavelCadastroId,
                DataCadastro = DateTime.Now,
                Ativo = true

            };

            return boleto;
        }

        public static Boleto CadastrarContaPagarFactory(DateTime vencimento,
                    decimal valor,
                    int juros,
                    int jurosFixo,
                    string multa,
                    string multaFixo,
                    string desconto,
                    TipoLancamento tipo,
                    string diasDesconto,
                    StatusPagamento statusBoleto,
                    bool ehFornecedor,
                    Guid pessoaId,
                    Guid centroCustoUnidadeId,
                    Guid responsavelCadastroId,
                    string historico,
                    Guid? subcontaId,
                    Guid? bancoId,
                    Guid? meioPgmId,
                    Guid? centroCustoId)
        {
            var boleto = new Boleto()
            {
                Vencimento = vencimento,
                Valor = valor,
                Juros = juros,
                JurosFixo = jurosFixo,
                Multa = multa,
                MultaFixo = multaFixo,
                Desconto = desconto,
                Tipo = tipo.DisplayName,
                DiasDesconto = diasDesconto,
                StatusBoleto = statusBoleto.DisplayName,
                Historico = historico,
                SubContaId = subcontaId,
                BancoId = bancoId,
                EhFornecedor = ehFornecedor,
                PessoaId = pessoaId,
                CentroCustoUnidadeId = centroCustoUnidadeId,
                ResponsavelCadastroId = responsavelCadastroId,
                DataCadastro = DateTime.Now,
                MeioPagamentoId  = meioPgmId,
                CentroCustoId = centroCustoId,
                Ativo = true

            };

            return boleto;
        }

        public static Boleto CadastrarContaReceberFactory(DateTime vencimento,
                    decimal valor,
                    int juros,
                    int jurosFixo,
                    string multa,
                    string multaFixo,
                    string desconto,
                    TipoLancamento tipo,
                    string diasDesconto,
                    StatusPagamento statusBoleto,
                    bool ehFornecedor,
                    Guid pessoaId,
                    Guid centroCustoUnidadeId,                    
                    Guid responsavelCadastroId,
                    string historico,
                    Guid? subcontaId,
                    Guid? bancoId)
        {
            var boleto = new Boleto()
            {
                Vencimento = vencimento,
                Valor = valor,
                Juros = juros,
                JurosFixo = jurosFixo,
                Multa = multa,
                MultaFixo = multaFixo,
                Desconto = desconto,
                Tipo = tipo.DisplayName,
                DiasDesconto = diasDesconto,
                StatusBoleto = statusBoleto.DisplayName,
                Historico = historico,
                SubContaId = subcontaId,
                BancoId = bancoId,
                EhFornecedor = ehFornecedor,
                PessoaId = pessoaId,
                CentroCustoUnidadeId = centroCustoUnidadeId,
                ResponsavelCadastroId = responsavelCadastroId,
                DataCadastro = DateTime.Now,
                Ativo = true

            };

            return boleto;
        }
        public void SetInfoBoletos(BoletoResponseInfo infoBoletos)
        {
            InfoBoletos = infoBoletos;
        }
        public void ChangeStatusToReparcelado()
        {
            StatusBoleto = StatusPagamento.Reparcelado.DisplayName;// "Reparcelado";
        }
        public void CancelarBoleto()
        {
            StatusBoleto = StatusPagamento.Cancelado.DisplayName;

            SubConta = "Caixa da escola";
        }

        public void SetBoletoDateCadastro(DateTime date)
        {
            DataCadastro = date;
        }

        public void SetResponsavelTipo()
        {
            Tipo = TipoLancamento.Credito.DisplayName;
            ResponsavelCadastroId = new Guid("e94d7dd8-8fef-4c14-8907-88ed8dc934c8");
        }

        public void ReceberBoleto(decimal valorPago, string formaPagamento, string digitosCartao)
        {
            DataPagamento = DateTime.Now;
            ValorPago = valorPago;
            StatusBoleto = StatusPagamento.Pago.DisplayName;
            FormaPagamento = formaPagamento;
            SubConta = "Caixa da escola";
            if (formaPagamento != "dinheiro")
            {
                DigitosCartao = digitosCartao;
            }

        }
        public void SetBoletoVencido()
        {
            StatusBoleto = StatusPagamento.Vencido.DisplayName;
        }
        public void SetSubConta(string subConta)
        {
            SubConta = subConta;
        }

        public void SetSubContaId(Guid subcontaId)
        {
            SubContaId = subcontaId;
        }

        public void SetHistorico(string historico)
        {
            Historico = historico;
        }

        #region EF

        protected Boleto() { }
        //public virtual InformacaoDebito InformacaoDebito { get; private set; }

        #endregion

    }
}

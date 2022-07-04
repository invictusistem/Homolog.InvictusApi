using Invictus.Core;
using System;


namespace Invictus.Domain.Financeiro.Configuracoes
{
    public class Banco : Entity
    {
        public Banco(string nome,
                    bool ehCaixaEscola,
                    string agencia,
                    string agenciaDigito,
                    string conta,
                    string contaDigito,
                    bool ativo,
                    DateTime dataCadastro,
                    bool utilizadoParaImpressao,
                    decimal saldo

                    
                    )
        {
            Nome = nome;
            EhCaixaEscola = ehCaixaEscola;
            Agencia = agencia;
            AgenciaDigito = agenciaDigito;
            Conta = conta;
            ContaDigito = contaDigito;
            Ativo = ativo;
            DataCadastro = dataCadastro;
            UtilizadoParaImpressao = utilizadoParaImpressao;
            Saldo = saldo;

        }
        public string Nome { get; private set; }
        public bool EhCaixaEscola { get; private set; }
        public string Agencia { get; private set; }
        public string AgenciaDigito { get; private set; }
        public string Conta { get; private set; }
        public string ContaDigito { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool UtilizadoParaImpressao { get; private set; }
        public Guid UnidadeId { get; private set; }
        public decimal Saldo { get; private set; }

        public void RegistroEntrada(decimal valor)
        {
            Saldo += valor;
        }

        public void EstornarEntrada(decimal valor)
        {
            Saldo -= valor;
        }
        protected Banco()
        {

        }
    }
}

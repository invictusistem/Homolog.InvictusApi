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
                    bool utilizadoParaImpressao
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

        protected Banco()
        {

        }
    }
}

﻿using Invictus.Core.Interfaces;

namespace Invictus.Domain.Financeiro.Fornecedor
{
    public class Fornecedor : IAggregateRoot
    {
        public Fornecedor() { }
        public Fornecedor(int id,
                        string razaoSocial,
                        string ie_rg,
                        string cnpj_cpf,
                        string email,
                        string telContato,
                        string whatsApp,
                        string nomeContato,
                        string cep,
                        string logradouro,
                        string complemento,
                        string cidade,
                        string uf,
                        string bairro,
                        bool ativo,
                        string observacoes
                        )
        {
            Id = id;
            RazaoSocial = razaoSocial;
            IE_RG = ie_rg;
            CNPJ_CPF = cnpj_cpf;
            Email = email;
            TelContato = telContato;
            WhatsApp = whatsApp;
            NomeContato = nomeContato;
            CEP = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Cidade = cidade;
            UF = uf;
            Bairro = bairro;
            Ativo = ativo;
            Observacoes = observacoes;

        }
        public int Id { get; private set; }
        public string RazaoSocial { get; private set; }
        public string IE_RG { get; private set; }
        public string CNPJ_CPF { get; private set; }
        public string Email { get; private set; }
        public string TelContato { get; private set; }
        public string WhatsApp { get; private set; }
        public string NomeContato { get; private set; }
        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public string Bairro { get; private set; }
        public bool Ativo { get; private set; }
        public int UnidadeId { get; private set; }
        public string Observacoes { get; private set; }
        
        public void SetUnidadeId(int unidadeId)
        {
            UnidadeId = unidadeId;
        }

        public void ActiveFornecedor()
        {
            Ativo = true;
        }

        public void DeactiveFornecedor()
        {
            Ativo = false;
        }

    }
}

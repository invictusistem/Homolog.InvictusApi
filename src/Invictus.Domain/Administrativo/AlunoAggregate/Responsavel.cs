using Invictus.Core.Enums;
using System;

namespace Invictus.Domain.Administrativo.AlunoAggregate
{
    public class Responsavel
    {
        public Responsavel() { }
        public Responsavel(int id,
                    TipoResponsavel tipo,
                    string nome,
                    //string nomeSocial,
                    string cpf,
                    string rg,
                     DateTime nascimento,
                    string naturalidade,
                    string naturalidadeUF,
                    string email,
                    //string telReferencia,
                    //string nomeContatoReferencia,
                    string telCelular,
                    string telResidencia,
                    string telWhatsapp,
                    string cep,
                     string logradouro,
                    string complemento,
                    string cidade,
                     string uf,
                    string bairro,
                    string observacoes,
                    int alunoId

                    )
        {
            Id = id;
            Tipo = tipo.DisplayName;
            Nome = nome;
            //NomeSocial = nomeSocial;
            CPF = cpf;
            RG = rg;
            Nascimento = nascimento;
            Naturalidade = naturalidade;
            NaturalidadeUF = naturalidadeUF;
            Email = email;
            //TelReferencia = telReferencia;
            //NomeContatoReferencia = nomeContatoReferencia;
            TelCelular = telCelular;
            TelResidencial = telResidencia;
            TelWhatsapp = telWhatsapp;
            CEP = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Cidade = cidade;
            UF = uf;
            Bairro = bairro;
            Observacoes = observacoes;
            AlunoId = alunoId;

        }

        public int Id { get; private set; }
        // public TipoResponsavel TipoResponsavel { get; private set; }
        public string Tipo { get; private set; }
        public string Nome { get; private set; }
        // public string NomeSocial { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public DateTime Nascimento { get; private set; }
        public string Naturalidade { get; private set; }
        public string NaturalidadeUF { get; private set; }
        public string Email { get; private set; }
        //public string TelReferencia { get; private set; }
        //public string NomeContatoReferencia { get; private set; }
        public string TelCelular { get; private set; }
        public string TelResidencial { get; private set; }
        public string TelWhatsapp { get; private set; }
        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public string Bairro { get; private set; }
        public string Observacoes { get; private set; }
        public int AlunoId { get; private set; }
        public virtual Aluno Aluno { get; private set; }

        public void SetTipoResponsavel(TipoResponsavel tipo)
        {
            Tipo = tipo.DisplayName;
        }

    }

}

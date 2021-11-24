using Invictus.Core;
using Invictus.Core.Enumerations;
using System;

namespace Invictus.Domain.Pedagogico.Responsaveis
{
    public class Responsavel : Entity
    {
        public Responsavel() { }
        public Responsavel(//int id,
                    TipoResponsavel tipo,
                    string nome,
                    //string nomeSocial,
                    string cpf,
                    string rg,
                    DateTime? nascimento,
                    string naturalidade,
                    string naturalidadeUF,
                    string email,
                    //string telReferencia,
                    //string nomeContatoReferencia,
                    string telCelular,
                    string telResidencia,
                    string telWhatsapp,
                    Guid matriculaId,
                    ResponsavelEndereco endereco
                    //  string observacoes,
                    

                    )
        {
            // Id = id;
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
            MatriculaId = matriculaId;
            Endereco = endereco;
            //Observacoes = observacoes;


        }

        // public int Id { get; private set; }
        // public TipoResponsavel TipoResponsavel { get; private set; }
        public string Tipo { get; private set; }
        public string Nome { get; private set; }
        // public string NomeSocial { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public DateTime? Nascimento { get; private set; }
        public string Naturalidade { get; private set; }
        public string NaturalidadeUF { get; private set; }
        public string Email { get; private set; }
        //public string TelReferencia { get; private set; }
        //public string NomeContatoReferencia { get; private set; }
        public string TelCelular { get; private set; }
        public string TelResidencial { get; private set; }
        public string TelWhatsapp { get; private set; }
        public bool TemRespFin { get; private set; }
        public Guid MatriculaId { get; private set; }

        public ResponsavelEndereco Endereco { get; private set; }

        public void SetTipoResponsavel(TipoResponsavel tipo)
        {
            Tipo = tipo.DisplayName;
        }

        public void SetRespFinanceiro(bool temRespFin)
        {
            TemRespFin = temRespFin;
        }

    }

}

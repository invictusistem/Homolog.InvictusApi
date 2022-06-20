using Invictus.Core;
using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.AlunoAggregate
{
    public class Aluno : Entity, IAggregateRoot
    {
        public Aluno() { }
        public Aluno(//int id,
                    string nome,
                    // string numeroMatricula,
                    string nomeSocial,
                    string cpf,
                    string rg,
                    string nomePai,
                    string nomeMae,
                    DateTime nascimento,
                    string naturalidade,
                    string naturalidadeUF,
                    string email,
                    string telReferencia,
                    string nomeContatoReferencia,
                    string telCelular,
                    string telResidencia,
                    string telWhatsapp,
                    AlunoEndereco endereco
                   // string cep,
                   // string logradouro,
                   //string complemento,
                   // string cidade,
                   //string uf,
                   //string bairro,
                   // string cienciaCurso,
                   // string observacoes,
                   // int unidadeCadastrada
                   )
        {
            // Id = id;
            Nome = nome;
            // NumeroMatricula = numeroMatricula;
            NomeSocial = nomeSocial;
            CPF = cpf;
            RG = rg;
            NomePai = nomePai;
            NomeMae = nomeMae;
            Nascimento = nascimento;
            Naturalidade = naturalidade;
            NaturalidadeUF = naturalidadeUF;
            Email = email;
            TelReferencia = telReferencia;
            NomeContatoReferencia = nomeContatoReferencia;
            TelCelular = telCelular;
            TelResidencial = telResidencia;
            TelWhatsapp = telWhatsapp;
            Endereco = endereco;
            // CEP = cep;
            // Logradouro = logradouro;
            // Complemento = complemento;
            //  Cidade = cidade;
            //  UF = uf;
            // Bairro = bairro;
            //CienciaCurso = cienciaCurso;
            // Observacoes = observacoes;
            //UnidadeCadastrada = unidadeCadastrada;
            //Ativo = ativo;
            //  Responsaveis = new List<Responsavel>();
        }

        // public int Id { get; private set; }
        public string Nome { get; private set; }
        // public string NumeroMatricula { get; private set; }
        public string NomeSocial { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public string NomePai { get; private set; }
        public string NomeMae { get; private set; }
        public DateTime Nascimento { get; private set; }
        public string Naturalidade { get; private set; }
        public string NaturalidadeUF { get; private set; }
        public string Email { get; private set; }
        public string TelReferencia { get; private set; }
        public string NomeContatoReferencia { get; private set; }
        // public string CienciaCurso { get; private set; }
        public string TelCelular { get; private set; }
        public string TelResidencial { get; private set; }
        public string TelWhatsapp { get; private set; }
        //  public string CEP { get; private set; }
        //  public string Logradouro { get; private set; }
        // public string Complemento { get; private set; }
        //  public string Cidade { get; private set; }
        // public string UF { get; private set; }
        // public string Bairro { get; private set; }
        // public string Observacoes { get; private set; }
        //public bool TemRespMenor { get; private set; }
        //public bool TemRespFin { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Guid ColaboradorRespCadastroId { get; private set; }
        public bool Ativo { get; private set; }
        public Guid UnidadeId { get; private set; }

        public AlunoEndereco Endereco { get; set; }

        public void SetColaboradorResponsavelPeloCadastro(Guid colaboradorId)
        {
            ColaboradorRespCadastroId = colaboradorId;
        }
        public void AtivarAluno()
        {
            Ativo = true;
        }

        public void SetDataCadastro()
        {
            DataCadastro = DateTime.Now;
        }

        public void SetDataCadastro(DateTime data)
        {
            DataCadastro = data;
        }

        public void DesativarAluno()
        {
            Ativo = false;
        }

        public void SetUnidadeId(Guid unidadeId)
        {
            UnidadeId = unidadeId;
        }

        //public void SetRespFin(bool valor)
        //{
        //    TemRespFin = valor;
        //}

        //public void SetRespMenor(bool valor)
        //{
        //    TemRespMenor = valor;
        //}

        public void NormalizeEmail()
        {
            Email.ToLower();

        }
        //public void CreateList()
        //{
        //    Responsaveis = new List<Responsavel>();
        //}

        //public void SetCienciaDoCurso(string cienciaDoCurso)
        //{
        //    CienciaCurso = cienciaDoCurso;
        //}

        //public void AddResponsavel(Responsavel responsavel)
        //{
        //    Responsaveis.Add(responsavel);
        //}
        public bool EhMenor(DateTime idade)
        {
            var nascimento = int.Parse(new DateTime(idade.Year, idade.Month, idade.Day, 0, 0, 0).ToString("yyyyMMdd"));

            var hoje = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

            var idadeAnos = (hoje - nascimento) / 10000;

            if (idadeAnos >= 18)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
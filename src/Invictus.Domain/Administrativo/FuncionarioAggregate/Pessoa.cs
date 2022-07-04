using Invictus.Core;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.ProfessorAggregate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Invictus.Domain.Administrativo.FuncionarioAggregate
{
    public class Pessoa : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string NomeSocial { get; private set; }
        public string Pai { get; private set; }
        public string Mae { get; private set; }
        public DateTime Nascimento { get; private set; }
        public string Naturalidade { get; private set; }
        public string NaturalidadeUF { get; private set; }
        public string RazaoSocial { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public string CNPJ { get; private set; }
        //public string CNPJ_CPF { get; private set; }
        public string IE_RG { get; private set; }
        public Guid? CargoId { get; private set; }
        public string Email { get; private set; }
        public string NomeContato { get; private set; }
        public string TelefoneContato { get; private set; }
        public string Celular { get; private set; }
        public string TelResidencial { get; private set; }
        public string TelWhatsapp { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Guid PessoaRespCadastroId { get; private set; }
        public string TipoPessoa { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime? DataEntrada { get; private set; }
        public DateTime? DataSaida { get; private set; }
        public Guid UnidadeId { get; private set; }
        public Endereco Endereco { get; private set; }
        public List<MateriaHabilitada> Materias { get; private set; }

        public void AtualizarNome()
        {
            Nome = RazaoSocial;
        }
        public void TratarEmail(string email)
        {
            Email = RemoveDiacritics(email);
        }

        public void TransfTurma(Guid newUnidadeId)
        {
            UnidadeId = newUnidadeId;
        }

        public void SetTipoPessoa(Invictus.Core.Enumerations.TipoPessoa tipoPessoa)
        {
            TipoPessoa = tipoPessoa.DisplayName;
        }
        public void SetRespCadastroId(Guid userId)
        {
            PessoaRespCadastroId = userId;
        }
        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public static Pessoa ColaboradorFactory(/*Guid id, */string nome, string email, string cpf, string celular, Guid cargoId, Guid unidadeId,
            bool ativo/*, DateTime dataCriacao, */,string bairro, string cep, string complemento, string logradouro, string numero, string cidade, string uf/*, Guid respId*/)
        {
            var endereco = Endereco.EnderecoFactory(bairro, cep, complemento, logradouro, numero, cidade, uf/*, id*/);


            var colaborador = new Pessoa()
            {
                //Id = id,
                Nome = nome,
                CPF = cpf,
                CargoId = cargoId,
                Email = email,
                Celular = celular,
                //DataCadastro = dataCriacao,
                Ativo = ativo,
                UnidadeId = unidadeId,
                Endereco = endereco,
                TipoPessoa = Invictus.Core.Enumerations.TipoPessoa.Colaborador.DisplayName,
                //PessoaRespCadastroId = respId

            };

            return colaborador;
        }

        public static Pessoa ProfessorFactory(Guid id, string nome, string email, string cpf, string celular, Guid unidadeId,
            bool ativo, DateTime dataCriacao, string bairro, string cep, string complemento,
            string logradouro, string numero, string cidade, string uf, Guid respId, string cnpj, DateTime? dataEntrada, DateTime? dataSaida, string nomeContato,
            string Telefonecontato)
        {
            var endereco = Endereco.EnderecoFactory(bairro, cep, complemento, logradouro, numero, cidade, uf/*, id*/);


            var colaborador = new Pessoa()
            {
                Id = id,
                Nome = nome,
                CPF = cpf,
                //CargoId = cargoId,
                Email = email,
                Celular = celular,
                DataCadastro = dataCriacao,
                Ativo = ativo,
                UnidadeId = unidadeId,
                Endereco = endereco,
                TipoPessoa = Invictus.Core.Enumerations.TipoPessoa.Professor.DisplayName,
                PessoaRespCadastroId = respId,
                CNPJ = cnpj,
                DataEntrada = dataEntrada,
                DataSaida = dataSaida,
                NomeContato = nomeContato,
                TelefoneContato = Telefonecontato
            };

            return colaborador;
        }

        public static Pessoa FornecedorFactory(Guid id, string nome, string email, string cnpj_cpf, string ie_rg, string nomecontato,
            string telContato, string whatsapp, DateTime dataCadastro, bool ativo, string bairro, string cep, string complemento, string logradouro,
            string numero, string cidade, string uf, Guid respId, Guid unidadeId)
        {
            var endereco = Endereco.EnderecoFactory(bairro, cep, complemento, logradouro, numero, cidade, uf/*, id*/);


            var colaborador = new Pessoa()
            {
                Id = id,
                RazaoSocial = nome,
                CNPJ = cnpj_cpf,
                IE_RG = ie_rg,
                NomeContato = nomecontato,
                TelefoneContato = telContato,
                TelWhatsapp = whatsapp,
                DataEntrada = dataCadastro,
                Ativo = ativo,
                Email = email,
                UnidadeId = unidadeId,
                Endereco = endereco,
                TipoPessoa = Invictus.Core.Enumerations.TipoPessoa.Fornecedor.DisplayName,
                PessoaRespCadastroId = respId

            };

            return colaborador;
        }

        public static Pessoa AlunoFactory(Guid id, string nome, string nomeSocial, string pai, string mae, DateTime nascimento, string naturalidade,
                                        string naturalidadeUF, string email, string cpf, string rg, string nomecontato, string telReferencia, 
                                        string telCelular, string telResidencial, string whatsapp,DateTime dataCadastro, bool ativo, string bairro, 
                                        string cep, string complemento, string logradouro,string numero, string cidade, string uf, Guid respId, Guid unidadeId)
        {
            var endereco = Endereco.EnderecoFactory(bairro, cep, complemento, logradouro, numero, cidade, uf/*, id*/);

            var colaborador = new Pessoa()
            {
                Id = id,
                Nome = nome,
                NomeSocial = nomeSocial,
                CPF = cpf,
                RG = rg,
                Pai = pai,
                Mae = mae,
                Nascimento = nascimento,
                Naturalidade = naturalidade,
                NaturalidadeUF = naturalidadeUF,
                Email = email,
                TelefoneContato = telReferencia,
                NomeContato = nomecontato,
                Celular = telCelular,
                TelResidencial = telResidencial,
                TelWhatsapp = whatsapp,
                DataCadastro = dataCadastro,
                Ativo = ativo,
                UnidadeId = unidadeId,
                Endereco = endereco,
                TipoPessoa = Invictus.Core.Enumerations.TipoPessoa.Aluno.DisplayName,
                PessoaRespCadastroId = respId
            };

            return colaborador;
        }

        #region EF
        protected Pessoa() { }

        #endregion
    }


    public class Endereco : Entity
    {
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Complemento { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public Guid PessoaId { get; private set; }
        public virtual Pessoa Pessoa { get; private set; }

        public static Endereco EnderecoFactory(string bairro, string cep, string complemento, string logradouro,
            string numero, string cidade, string uf/*, Guid funcionario*/)
        {
            var endereco = new Endereco()
            {
                Bairro = bairro,
                CEP = cep,
                Complemento = complemento,
                Logradouro = logradouro,
                Numero = numero,
                Cidade = cidade,
                UF = uf

            };

            return endereco;

        }

        protected Endereco() { }

    }

    public class DadosBancarios : Entity
    {
        public string BancoNumero { get; private set; }
        public string Agencia { get; private set; }
        public string Conta { get; private set; }
        public string TipoConta { get; private set; }

        protected DadosBancarios() { }
    }

    public class Disponibilidade : Entity
    {
        public bool Domingo { get; private set; }
        public bool Segunda { get; private set; }
        public bool Terca { get; private set; }
        public bool Quarta { get; private set; }
        public bool Quinta { get; private set; }
        public bool Sexta { get; private set; }
        public bool Sabado { get; private set; }
        public Guid UnidadeId { get; private set; }
        public Guid PessoaId { get; private set; }
        public DateTime DataAtualizacao { get; private set; }      

        #region EF
        protected Disponibilidade() { }
        //public Guid ProfessorId { get; private set; }
        //public virtual Professor Professor { get; private set; }
        #endregion

    }

    //public class MateriaHabilitada : Entity
    //{
    //    protected MateriaHabilitada() { }

    //    public Guid PacoteMateriaId { get; private set; }
    //    public Guid FuncionarioId { get; private set; }


    //}
}
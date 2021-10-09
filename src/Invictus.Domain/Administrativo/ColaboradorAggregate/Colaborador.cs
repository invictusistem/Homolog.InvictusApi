using Invictus.Core.Interfaces;
using System;

namespace Invictus.Domain.Administrativo.ColaboradorAggregate
{
    public class Colaborador : IAggregateRoot
    {
        public Colaborador() { }
        public Colaborador(int id,
                            string nome,
                            string email,
                            string cpf,
                            string celular,
                            string cargo,
                            int cargoId,
                            int unidadeId,
                            string perfil,
                            bool perfilAtivo,
                            bool ativo,
                            string cep,
                            string logradouro,
                            string complemento,
                            string bairro,
                            string cidade,
                            string uf)
        {   
            Id = id;
            Nome = nome;
            Email = email;
            CPF = cpf;
            Celular = celular;
            Cargo = cargo;
            CargoId = cargoId;
            UnidadeId = unidadeId;
            Perfil = perfil;
            PerfilAtivo = perfilAtivo;
            Ativo = ativo;
            CEP = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            DataCriacao = DateTime.Now;
        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string Celular { get; private set; }
        public string Cargo { get; private set; }
        public int CargoId { get; private set; }
        public string Numero { get; private set; }
        public int UnidadeId { get; private set; }
        public string Perfil { get; private set; }
        public bool PerfilAtivo { get; private set; }
        public bool Ativo { get; private set; }
        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public void SetPerfil(string perfil)
        {
            Perfil = perfil;
        }

        public void AtivarPerfil(bool value)
        {
            PerfilAtivo = value;
        }

        public void DesativarColaborador()
        {
            Ativo = false;
        }

        public void SetPerfil(string perfil, bool perfilAtivo)
        {
            Perfil = perfil;
            PerfilAtivo = perfilAtivo;
        }

        public void SetUnidadeId(int unidadeId)
        {
            UnidadeId = unidadeId;
        }
    }
}

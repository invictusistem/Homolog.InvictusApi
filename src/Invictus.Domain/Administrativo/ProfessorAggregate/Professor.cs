using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.ProfessorAggregate
{
    public class Professor : IAggregateRoot
    {
        public Professor() { }
        public Professor(//int id,
                            string nome,
                            string email,
                            string cpf,
                            string celular,
                            string cargo,
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
            Id = Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            CPF = cpf;
            Celular = celular;
            Cargo = cargo;
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

        }
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string Celular { get; private set; }
        public string Cargo { get; private set; }
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
        public MateriasHabilitadas materias { get; private set; }

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
    }
}

using Invictus.Core;
using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.ProfessorAggregate
{
    public class Professor : Entity, IAggregateRoot
    {   
        public Professor(string nome,
                           string email,
                           string cpf,
                           string celular,
                           Guid unidadeId,
                           bool ativo,
                           ProfessorEndereco endereco,
                           DadosBancarios dadosBancarios)
        {

            Nome = nome;
            Email = email;
            CPF = cpf;
            Celular = celular;
            UnidadeId = unidadeId;
            Ativo = ativo;
            Endereco = endereco;
            DadosBancarios = dadosBancarios;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string Celular { get; private set; }
        public Guid UnidadeId { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public ProfessorEndereco Endereco { get; private set; }
        public List<MateriaHabilitada> Materias { get; private set; }
        public DadosBancarios DadosBancarios { get; private set; }

        public void InativarProfessor()
        {
            Ativo = false;
        }


        public Professor() { }

    }
}


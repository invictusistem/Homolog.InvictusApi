using Invictus.Core.Interfaces;
using Invictus.Domain.Pedagogico.Models;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Pedagogico.EstagioAggregate
{
    public class EstagioMatricula : IAggregateRoot
    {

        public EstagioMatricula()
        {

        }
        public EstagioMatricula(//int id,
                                    int alunoId,
                                    string nome,
                                    string email,
                                    string cpf,
                                    int estagioId
                                    )
        {
            //Id = id;
            AlunoId = alunoId;
            Nome = nome;
            Email = email;
            CPF = cpf;
            EstagioId = estagioId;

        }
        public int Id { get; private set; }
        public int AlunoId { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public List<Documento> Documentos { get; private set; }
        // navigation
        public int EstagioId { get; private set; }
        public virtual Estagio Estagio { get; private set; }
    }
}

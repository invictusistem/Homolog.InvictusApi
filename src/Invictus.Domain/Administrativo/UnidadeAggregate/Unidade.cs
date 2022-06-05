using Invictus.Core;
using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.UnidadeAggregate
{
    public class Unidade : Entity, IAggregateRoot
    {   
        public Unidade(string sigla,
                       string cnpj,
                       string descricao,
                       bool ativo,
                       bool isUnidadeGlobal,
                       Endereco endereco)
        {           
            Sigla = sigla;
            CNPJ = cnpj;
            Descricao = descricao;            
            Ativo = ativo;
            IsUnidadeGlobal = isUnidadeGlobal;
            Endereco = endereco;
            Salas = new List<Sala>();
        }

        public string Sigla { get; private set; }
        public string CNPJ { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public bool IsUnidadeGlobal { get; private set; }
        public Endereco Endereco { get; private set; }
        public List<Sala> Salas { get; private set; }

        public void AddSala(Sala newSala)
        {
            Salas.Add(newSala);
        }

        //EF

        public Unidade(){}


    }
}
using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Administrativo.UnidadeAggregate
{
    public class Unidade : IAggregateRoot
    {
        public Unidade()
        {

        }
        public Unidade(//int id,
                       string sigla,
                       string descricao,
                       string bairro,
                       string cep,
                       string complemento,
                       string logradouro,
                       string numero,
                       string cidade,
                       string uf,
                      // DateTime dataCriacao,
                       bool ativo
            )
        {
            //Id = id;
            Sigla = sigla;
            Descricao = descricao;
            Bairro = bairro;
            CEP = cep;
            Complemento = complemento;
            Logradouro = logradouro;
            Numero = numero;
            Cidade = cidade;
            UF = uf;
            //DataCriacao = SetDataCriacao();// DateTime.Now;
            Ativo = ativo;
            //Modulos = new List<Modulo>();
            Salas = new List<Sala>();
        }


        public int Id { get; private set; }
        public string Sigla { get; private set; }
        public string CNPJ { get; private set; }
        public string Descricao { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Complemento { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public bool Ativo { get; private set; }
        // public List<Modulo> Modulos { get; private set; }
        public List<Sala> Salas { get; private set; }

        public void AddSala(Sala newSala)
        {
            Salas.Add(newSala);
        }

        public void SetDataCriacao()
        {
            DataCriacao = DateTime.Now;

            //return DateTime.Now;
        }
    }
}

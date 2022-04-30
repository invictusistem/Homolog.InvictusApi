using Invictus.Core;
using System;

namespace Invictus.Domain.Padagogico.Estagio
{
    public class Estagio : Entity
    {
        protected Estagio()
        {

        }
        public Estagio(
                        string nome,
                        string cnpj,
                        DateTime dataInicio,
                        //int trimestre,
                        int vagas,
                        string cep,
                        string logradouro,
                        string numero,
                        string complemento,
                        string cidade,
                        string uf,
                        string bairro,
                        Guid tipoEstagio,
                        bool ativo
                        )
        {
            //Id = id;
            Nome = nome;
            CNPJ = cnpj;
            DataInicio = dataInicio;
           // Trimestre = trimestre;
            Vagas = vagas;
            CEP = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            UF = uf;
            Bairro = bairro;
            TipoEstagio = tipoEstagio;
            Ativo = ativo;
           // Matriculados = new List<EstagioMatricula>();

        }
       // public int Id { get; private set; }
        public string Nome { get; private set; }
        public string CNPJ { get; private set; }
        public DateTime DataInicio { get; private set; }
        //public int Trimestre { get; private set; }
        public int Vagas { get; private set; }
        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public string Bairro { get; private set; }
        public bool Ativo { get; private set; }
        public Guid TipoEstagio { get; private set; }
        public Guid SupervisorId { get; private set; }
        //public List<EstagioMatricula> Matriculados { get; private set; }

        //public void SetSupervisorId(int supervisorId)
        //{
        //    SupervisorId = supervisorId;
        //}
    }
}

using Invictus.Domain.Pedagogico.EstagioAggregate;
using System.Collections.Generic;

namespace Invictus.Domain.Pedagogico.Models
{
    public class Estagio
    {
        public Estagio()
        {

        }
        public Estagio(int id,
                        string nome,
                        string dataInicio,
                        int trimestre,
                        int vagas,
                        string cep,
                        string logradouro,
                        string complemento,
                        string cidade,
                        string uf,
                        string bairro
                        )
        {
            Id = id;
            Nome = nome;
            DataInicio = dataInicio;
            Trimestre = trimestre;
            Vagas = vagas;
            CEP = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Cidade = cidade;
            UF = uf;
            Bairro = bairro;
            Matriculados = new List<EstagioMatricula>();

        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string DataInicio { get; private set; }
        public int Trimestre { get; private set; }
        public int Vagas { get; private set; }
        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public string Bairro { get; private set; }
        public List<EstagioMatricula> Matriculados { get; private set; }
    }
}

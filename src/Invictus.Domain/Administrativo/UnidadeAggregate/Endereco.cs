using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.UnidadeAggregate
{
    public class Endereco : ValueObject
    {
        public Endereco() { }
        public Endereco(string bairro,
                        string cep,
                        string complemento,
                        string logradouro,
                        string numero,
                        string cidade,
                        string uf
                        )
        {
            Bairro = bairro;
            CEP = cep;
            Complemento = complemento;
            Logradouro = logradouro;
            Numero = numero;
            Cidade = cidade;
            UF = uf;

        }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Complemento { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        //public Guid UnidadeId { get; private set; }
        //// EF
        //public Unidade Unidade { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Bairro;
            yield return CEP;
            yield return Complemento;
            yield return Logradouro;
            yield return Numero;
            yield return Cidade;
            yield return UF;

        }
    }
}

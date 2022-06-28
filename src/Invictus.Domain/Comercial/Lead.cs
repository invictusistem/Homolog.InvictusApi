using Invictus.Core;
using System;

namespace Invictus.Domain.Comercial
{
    public class Lead : Entity
    {

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime Data { get; private set; }
        public string Telefone { get; private set; }
        public string Bairro { get; private set; }
        public string CursoPretendido { get; private set; }
        public Guid UnidadeId { get; private set; }
        public DateTime DataInclusaoSistema { get; private set; }
        public Guid ResponsavelLead { get; private set; }
        public bool Efetivada { get; private set; }
        public Guid? MatriculaId { get; private set; }

        public static Lead LeadFactory(string nome,
                    string email,
                    //string data,
                    string telefone,
                    string bairro,
                    string cursoPretendido,
                    Guid unidadeId,
                    DateTime dataInclusaoSistem,
                    Guid responsavelLead)
        {

            var lead = new Lead()
            {
                Nome = nome,
                Email = email,
                Data = DateTime.Now,
                Telefone = telefone,
                Bairro = bairro,
                CursoPretendido = cursoPretendido,
                UnidadeId = unidadeId,
                DataInclusaoSistema = dataInclusaoSistem,
                ResponsavelLead = responsavelLead,
                Efetivada = false
            };

            return lead;
        }


        protected Lead()
        {

        }

    }
}

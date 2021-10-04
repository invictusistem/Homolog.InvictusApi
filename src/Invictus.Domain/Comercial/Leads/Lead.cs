using System;

namespace Invictus.Domain.Comercial.Leads
{
    public class Lead
    {
        public Lead(int id,
                    string nome,
                    string email,
                    string data,
                    string telefone,
                    string bairro,
                    string cursoPretendido,
                    string unidade,
                    DateTime dataInclusaoSistem,
                    string responsavelLead,
                    int colaboradorId)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Data = data;
            Telefone = telefone;
            Bairro = bairro;
            CursoPretendido = cursoPretendido;
            Unidade = unidade;
            DataInclusaoSistema = dataInclusaoSistem;
            ResponsavelLead = responsavelLead;
            ColaboradorId = colaboradorId;


        }

        public Lead()
        {

        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Data { get; private set; }
        public string Telefone { get; private set; }
        public string Bairro { get; private set; }
        public string CursoPretendido { get; private set; }
        public string Unidade { get; private set; }
        public DateTime DataInclusaoSistema { get; private set; }
        public string ResponsavelLead { get; private set; }
        public int ColaboradorId { get; private set; }

        public void SetDateAndResponsavelInLead(string nomeResponsavel)
        {
            ResponsavelLead = nomeResponsavel;
            DataInclusaoSistema = DateTime.Now;

        }
    }
}

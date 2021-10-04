using System;
using System.Collections.Generic;

namespace Invictus.Domain.Administrativo.Models
{
    public class Mensagem
    {
        public Mensagem() { }
        public Mensagem(int id,
                        string conteudo,
                        DateTime dataCriacao,
                        DateTime dataExpiracao,
                        string criador)
        {
            Id = id;
            Conteudo = conteudo;
            DataCriacao = dataCriacao;
            DataExpiracao = dataExpiracao;
            Criador = criador;
            Destinatarios = new List<Destinatario>();
        }

        public int Id { get; private set; }
        public string Conteudo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataExpiracao { get; private set; }
        public string Criador { get; private set; }
        public List<Destinatario> Destinatarios { get; private set; }
    }

    public class Destinatario
    {
        public Destinatario() { }
        public Destinatario(int id,
                            string perfil)
        {
            Id = id;
            Perfil = perfil;
        }

        public int Id { get; private set; }
        public string Perfil { get; private set; }
    }
}

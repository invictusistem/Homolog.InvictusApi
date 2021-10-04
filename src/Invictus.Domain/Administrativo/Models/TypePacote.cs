using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Models
{
    public class TypePacote
    {
        public TypePacote(
            string nome,
            string observacao,
            string nivel,
            bool ativo
            )
        {
            Nome = nome;
            Observacao = observacao;
            Nivel = nivel;
            Ativo = ativo;

        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Nivel { get; private set; }
        public bool Ativo { get; private set; }
        public string Observacao { get; private set; }
    }
}

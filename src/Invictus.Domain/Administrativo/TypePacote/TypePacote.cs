
using Invictus.Core;

namespace Invictus.Domain.Administrativo.Models
{
    public class TypePacote : Entity
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
       // public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Nivel { get; private set; }
        public bool Ativo { get; private set; }
        public string Observacao { get; private set; }

        #region EF 

        public TypePacote()
        {

        }

        #endregion
    }
}
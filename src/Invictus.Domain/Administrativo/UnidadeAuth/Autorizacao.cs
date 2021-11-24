using Invictus.Core;
using System;

namespace Invictus.Domain.Administrativo.UnidadeAuth
{
    public class Autorizacao : Entity
    {
        public Autorizacao(Guid usuarioId,
                            string unidade,
                            Guid unidadeId
                            )
        {
            UsuarioId = usuarioId;
            Unidade = unidade;
            UnidadeId = unidadeId;

        }
        public Guid UsuarioId { get; private set; }
        public string Unidade { get; private set; }
        public Guid UnidadeId { get; private set; }

        #region EF

        public Autorizacao() { }    

        #endregion
    }
}

using Invictus.Core;
using System;

namespace Invictus.Domain.Administrativo.ColaboradorAggregate
{
    public class AnotacaoColaborador : Entity
    {
        public AnotacaoColaborador(string titulo,
                                string comentario,
                                DateTime dataRegistro,
                                Guid userId,
                                Guid matriculaId
                                )
        {
            Titulo = titulo;
            Comentario = comentario;
            DataRegistro = dataRegistro;
            UserId = userId;
            MatriculaId = matriculaId;

        }
        public string Titulo { get; private set; }
        public string Comentario { get; private set; }
        public DateTime DataRegistro { get; private set; }
        public Guid UserId { get; private set; }
        public Guid MatriculaId { get; private set; }

        #region EF
        public AnotacaoColaborador()
        {

        }

        #endregion
    }
}

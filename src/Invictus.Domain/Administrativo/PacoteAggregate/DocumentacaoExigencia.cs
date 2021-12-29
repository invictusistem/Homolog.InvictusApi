using Invictus.Core;
using Invictus.Core.Enums;
using System;

namespace Invictus.Domain.Administrativo.PacoteAggregate
{
    public class DocumentacaoExigencia : Entity
    {
        
        public DocumentacaoExigencia(//Guid documentoId,
                                     string descricao,
                                     string comentario,
                                     TitularDoc titular,
                                     int validadeDias,
                                     bool obrigatorioParaMatricula
                                    )
        {
           // DocumentoId = documentoId;
            Descricao = descricao;
            Comentario = comentario;
            Titular = titular.DisplayName;
            ValidadeDias = validadeDias;
            ObrigatorioParaMatricula = obrigatorioParaMatricula;
        }
        
       // public Guid DocumentoId { get; private set; }
        public string Descricao { get; private set; }
        public string Comentario { get; private set; }
        public string Titular { get; private set; }
        public int ValidadeDias { get; private set; }
        public bool ObrigatorioParaMatricula { get; private set; }

        public void SetPacoteId(Guid pacoteId)
        {
            PacoteId = pacoteId;
        }

        #region EF

        public DocumentacaoExigencia() { }
        public Guid PacoteId { get; private set; }
        public virtual Pacote Pacote { get; private set; }
        #endregion
    }
}

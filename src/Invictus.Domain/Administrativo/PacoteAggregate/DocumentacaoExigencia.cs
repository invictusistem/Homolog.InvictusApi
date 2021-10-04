using Invictus.Core.Enums;

namespace Invictus.Domain.Administrativo.PacoteAggregate
{
    public class DocumentacaoExigencia
    {
        public DocumentacaoExigencia()
        {

        }
        public DocumentacaoExigencia(//int id,
                                    string descricao,
                                    string comentario,
                                    //TitularDoc titular,
                                    int moduloId
                                    )
        {
           // Id = id;
            Descricao = descricao;
            Comentario = comentario;
            //Titular = titular.DisplayName;
            ModuloId = moduloId;

        }
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public string Comentario { get; private set; }
        public string Titular { get; private set; }
        public int ModuloId { get; private set; }
        public virtual Pacote Pacote { get; private set; }

        public void SetTitular(string titular)
        {
            Titular = TitularDoc.TryParse(titular).DisplayName;
        }
    }
}

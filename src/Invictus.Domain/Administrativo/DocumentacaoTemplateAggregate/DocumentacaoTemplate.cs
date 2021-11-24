using Invictus.Core;

namespace Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate
{
    public class DocumentacaoTemplate : Entity
    {
        public DocumentacaoTemplate(string nome,
                                    string descricao,
                                    int validadeDias
                                    )
        {
            Nome = nome;
            Descricao = descricao;
            ValidadeDias = validadeDias;

        }
        public string Nome { get; private set; }
        public  string Descricao { get; private set; }
        public int ValidadeDias { get; private set; }

        #region

        public DocumentacaoTemplate() { }

        #endregion
    }
}

using Invictus.Core;
using Invictus.Core.Enumerations.Logs;
using System;

namespace Invictus.Domain.Administrativo.Logs
{
    public class LogProduto : Entity
    {
        public LogProduto(DateTime dataEvento,
                        LogProdutoAcao acao,
                        Guid produtoId,
                        Guid userId,
                        string detalhesJson,
                        string observacaoUsuario,
                        Guid unidadeId
                        )
        {
            DataEvento = dataEvento;
            Acao = acao.DisplayName;
            ProdutoId = produtoId;
            UserId = userId;
            DetalhesJson = detalhesJson;
            ObservacaoUsuario = observacaoUsuario;
            UnidadeId = unidadeId;


        }
        // Data  produtoId Acao    Usuario    Detalhes  observacaoDoUsuario UnidadeId
        //public Guid Id { get; private set; }
        public DateTime DataEvento { get; private set; }
        public string Acao { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Guid UserId { get; private set; }
        public string DetalhesJson { get; private set; }
        public string ObservacaoUsuario { get; private set; }
        public Guid UnidadeId { get; private set; }

        protected LogProduto()
        {

        }
    }
}

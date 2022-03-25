using System;

namespace Invictus.Domain.Administrativo.Logs
{
    public class LogProduto
    {
        public LogProduto(Guid produtoId,
                            Guid colaboradorID,
                            string metodo,
                            DateTime dataCriacao,
                            string oldCommand,
                            string newCommand)
        {
            ProdutoId = produtoId;
            ColaboradorID = colaboradorID;
            Metodo = metodo;
            DataCriacao = dataCriacao;
            OldCommand = oldCommand;
            NewCommand = newCommand;

        }

        public Guid Id { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Guid ColaboradorID { get; private set; }
        public string Metodo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string OldCommand { get; private set; }
        public string NewCommand { get; private set; }

        public LogProduto()
        {

        }
    }
}

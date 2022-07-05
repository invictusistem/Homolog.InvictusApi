using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations.Logs
{
    public class LogProdutoAcao : Enumeration
    {
        public static LogProdutoAcao Criacao = new(1, "Criação");
        public static LogProdutoAcao Inativacao = new(2, "Inativação");
        public static LogProdutoAcao Edicao = new(3, "Edição");
        public static LogProdutoAcao AumentoEstoque = new(4, "Estoque aumentado");
        public static LogProdutoAcao DiminuicaoEstoque = new(5, "Diminuicao estoque");
        public static LogProdutoAcao Venda = new(6, "Venda");


        public LogProdutoAcao() { }
        public LogProdutoAcao(int id, string name) : base(id, name) { }


    }
}
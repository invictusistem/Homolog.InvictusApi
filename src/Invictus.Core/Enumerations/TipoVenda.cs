using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class TipoVenda : Enumeration
    {
        public static TipoVenda Produto = new(1, "Produto");
        public static TipoVenda Outros = new(2, "Outros");

        public TipoVenda()
        {

        }
        public TipoVenda(int id, string name) : base(id, name) { }
    }
}
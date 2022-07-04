using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro
{
    public class Juro
    {
        public int Juros { get; private set; }
        public int JurosFixo { get; private set; }
        public string Multa { get; private set; }
        public string MultaFixo { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Parametros
{
    public class ParametrosType
    {
        public ParametrosType(string nome)
        {
            Nome = nome;
            ParametrosValues = new List<ParametrosValue>();
        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public List<ParametrosValue> ParametrosValues { get; private set; }

    }
}

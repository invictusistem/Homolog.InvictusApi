using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Models
{
    public class Parametros
    {
        public Parametros()
        {

        }

        public int Id { get; private set; }
        public string Type { get; private set; }
        public string Value { get; private set; }
        public bool IsActive { get; private set; }

    }
}

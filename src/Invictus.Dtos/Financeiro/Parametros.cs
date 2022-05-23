using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro
{
    public class Parametros
    {
        public Guid? meioPgmId { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}

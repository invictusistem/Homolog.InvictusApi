using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Logs
{
    public class Log
    {
        public Log()
        {

        }
        public long Id { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Acao { get; set; }
        public string ConteudoJson { get; set; }

    }
}

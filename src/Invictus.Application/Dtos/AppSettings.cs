using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpireHours { get; set; }
        public string Emissor { get; set; }
        public string Validation { get; set; }
        public string SMTPAccount { get; set; }
        public string SMTPServer { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPPassword { get; set; }

    }
}

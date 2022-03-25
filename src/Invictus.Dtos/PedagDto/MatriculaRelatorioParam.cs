using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.PedagDto
{
    public class MatriculaRelatorioParam
    {
        public string opcao { get; set; }
        public DateTime? inicio { get; set; }
        public DateTime? fim { get; set; }
        public Guid? turmaId { get; set; }
    }
}

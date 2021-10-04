using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class SaveProfsCommand
    {
        public int turmaId { get; set; }
        public IEnumerable<int> listProfsIds { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class FormData
    {
        public AlunoDto alunoDto { get; set; }
        public RespFinancDto respAlunoDto { get; set; }
        public RespMenorDto respMenorDto { get; set; }
    }
}

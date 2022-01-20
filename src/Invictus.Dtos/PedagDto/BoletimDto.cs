using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.PedagDto
{
    public class BoletimDto
    {   
        public string avaliacaoUm { get; set; }
        public string segundaChamadaAvaliacaoUm { get; set; }
        public string avaliacaoDois { get; set; }
        public string segundaChamadaAvaliacaoDois { get; set; }
        public string avaliacaoTres { get; set; }
        public string segundaChamadaAvaliacaoTres { get; set; }
        
        public string materiaDescricao { get; set; }
        public string resultado { get; set; }
        public int faltas { get; set; }
        
    }
    
}

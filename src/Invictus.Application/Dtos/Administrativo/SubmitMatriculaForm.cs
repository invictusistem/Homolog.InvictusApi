using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Administrativo
{
    public class SubmitMatriculaForm
    {
        public int idAluno { get; set; }
        public int idTurma { get; set; }
        public string ciencia { get; set; }
        public string meioPagamento { get; set; }
        public string parcelas { get; set; }
        public int percentualDesconto { get; set; }
        public bool primeiraParceJaPaga { get; set; }
        public string diaVencimento { get; set; }
        
    }
}

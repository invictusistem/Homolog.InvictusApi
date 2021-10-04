using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enums
{
    public class HistoricoDivida : Enumeration
    {
        public static HistoricoDivida Original = new(1, "Original");
        public static HistoricoDivida Reparcelamento = new(2, "Reparcelamento");
       
        //public static Status Encerrada = new(4, "Encerrada");
        public HistoricoDivida()
        {

        }
        public HistoricoDivida(int id, string name) : base(id, name) { }

        public static HistoricoDivida TryParse(string compare)
        {
            if (compare == "original")
            {
                return Original;

            }
            else if (compare == "reparcelamento")
            {
                return Reparcelamento;

            }

            throw new NotImplementedException();
        }      
    }
}

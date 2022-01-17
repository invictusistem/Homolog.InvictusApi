using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class StatusMatricula : Enumeration
    {
        public static StatusMatricula AguardoConfirmacao = new(1, "Aguardando confirmação");
        public static StatusMatricula Suspensa = new(2, "Suspensa");
        public static StatusMatricula Regular = new(3, "Regular");
        public static StatusMatricula Encerrada = new(4, "Encerrada");
        

        //public static Status Encerrada = new(4, "Encerrada");
        public StatusMatricula()
        {

        }
        public StatusMatricula(int id, string name) : base(id, name) { }

        public static StatusMatricula TryParse(string compare)
        {
            if (compare == "Aguardando confirmação")
            {
                return AguardoConfirmacao;

            }
            else if (compare == "Suspensa")
            {
                return Suspensa;

            }
            else if (compare == "Regular")
            {
                return Regular;

            }
            else if (compare == "Encerrada")
            {
                return Encerrada;

            }

            throw new NotImplementedException();
        }

    }
}
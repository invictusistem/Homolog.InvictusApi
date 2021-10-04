using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enums
{
    public class TitularDoc : Enumeration
    {
        public static TitularDoc Aluno = new(1, "Aluno");
        public static TitularDoc RespMenor = new(2, "Responsável menor");
        public static TitularDoc RespFin = new(3, "Responsável financeiro");
        

        //public static Status Encerrada = new(4, "Encerrada");
        public TitularDoc()
        {

        }
        public TitularDoc(int id, string name) : base(id, name) { }

        public static TitularDoc TryParse(string resp)
        {
            if (resp == "Aluno")
            {
                return Aluno;

            }
            else if (resp == "Responsável menor")
            {
                return RespMenor;

            }
            else if (resp == "Responsável financeiro")
            {
                return RespFin;

            }
            

            throw new NotImplementedException();
        }


        //public static DayOfWeek TryParseToDayofWeek(string day)
        //{
        //    if (day == "Segunda-feira")
        //    {
        //        return DayOfWeek.Monday;

        //    }
        //    else if (day == "Terça-feira")
        //    {
        //        return DayOfWeek.Tuesday;

        //    }
        //    else if (day == "Quarta-feira")
        //    {
        //        return DayOfWeek.Wednesday;

        //    }
        //    else if (day == "Quinta-feira")
        //    {
        //        return DayOfWeek.Thursday;

        //    }
        //    else if (day == "Sexta-feira")
        //    {
        //        return DayOfWeek.Friday;

        //    }
        //    else if (day == "Sábado")
        //    {
        //        return DayOfWeek.Saturday;

        //    }
        //    else if (day == "Domingo")
        //    {
        //        return DayOfWeek.Sunday;

        //    }

        //    throw new NotImplementedException();
        //}
    }
}

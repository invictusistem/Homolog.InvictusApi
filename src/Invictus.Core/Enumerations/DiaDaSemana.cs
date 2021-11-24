using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class DiaDaSemana : Enumeration
    {
        public static DiaDaSemana Segunda = new(1, "Segunda-feira");
        public static DiaDaSemana Terca = new(2, "Terça-feira");
        public static DiaDaSemana Quarta = new(3, "Quarta-feira");
        public static DiaDaSemana Quinta = new(4, "Quinta-feira");
        public static DiaDaSemana Sexta = new(5, "Sexta-feira");
        public static DiaDaSemana Sabado = new(6, "Sábado");
        public static DiaDaSemana Domingo = new(7, "Domingo");

        //public static Status Encerrada = new(4, "Encerrada");
        public DiaDaSemana()
        {

        }
        public DiaDaSemana(int id, string name) : base(id, name) { }
        public static DiaDaSemana TryParseStringToString(string day)
        {
            if (day == "Segunda-feira")
            {
                return Segunda;

            }
            else if (day == "Terça-feira")
            {
                return Terca;

            }
            else if (day == "Quarta-feira")
            {
                return Quarta;

            }
            else if (day == "Quinta-feira")
            {
                return Quinta;

            }
            else if (day == "Sexta-feira")
            {
                return Sexta;

            }
            else if (day == "Sábado")
            {
                return Sabado;

            }
            else if (day == "Domingo")
            {
                return Domingo;

            }

            throw new NotImplementedException();
        }
        public static DiaDaSemana TryParse(DayOfWeek day)
        {
            if (day == DayOfWeek.Monday)
            {
                return Segunda;

            }
            else if (day == DayOfWeek.Tuesday)
            {
                return Terca;

            }
            else if (day == DayOfWeek.Wednesday)
            {
                return Quarta;

            }
            else if (day == DayOfWeek.Thursday)
            {
                return Quinta;

            }
            else if (day == DayOfWeek.Friday)
            {
                return Sexta;

            }
            else if (day == DayOfWeek.Saturday)
            {
                return Sabado;

            }
            else if (day == DayOfWeek.Sunday)
            {
                return Domingo;

            }

            throw new NotImplementedException();
        }


        public static DayOfWeek TryParseToDayofWeek(string day)
        {
            if (day == "Segunda-feira")
            {
                return DayOfWeek.Monday;

            }
            else if (day == "Terça-feira")
            {
                return DayOfWeek.Tuesday;

            }
            else if (day == "Quarta-feira")
            {
                return DayOfWeek.Wednesday;

            }
            else if (day == "Quinta-feira")
            {
                return DayOfWeek.Thursday;

            }
            else if (day == "Sexta-feira")
            {
                return DayOfWeek.Friday;

            }
            else if (day == "Sábado")
            {
                return DayOfWeek.Saturday;

            }
            else if (day == "Domingo")
            {
                return DayOfWeek.Sunday;

            }

            throw new NotImplementedException();
        }
    }
}

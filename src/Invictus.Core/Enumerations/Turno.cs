using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class Turno : Enumeration
    {
        public static Turno Manha = new(1, "Manhã");
        public static Turno Tarde = new(2, "Tarde");
        public static Turno Noite = new(3, "Noite");
        public static Turno Integral = new(4, "Integral");


        //public static Status Encerrada = new(4, "Encerrada");
        public Turno()
        {

        }
        public Turno(int id, string name) : base(id, name) { }
       
    }
}

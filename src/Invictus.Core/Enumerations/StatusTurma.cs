using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class StatusTurma : Enumeration
    {
        public static StatusTurma AguardandoInicio = new(1, "Aguardando início");
        public static StatusTurma EmAndamento = new(2, "Em andamento");
        public static StatusTurma Suspensa = new(3, "Suspensa");
        public static StatusTurma Encerrada = new(4, "Encerrada");
        public static StatusTurma Cancelada = new(5, "Cancelada");
        public StatusTurma()
        {

        }
        public StatusTurma(int id, string name) : base(id, name) { }
    }
}

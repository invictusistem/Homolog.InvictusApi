using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations.Logs
{
    public class LogTurmaAcao : Enumeration
    {
        public static LogTurmaAcao Criacao = new(1, "Criação");
        public static LogTurmaAcao Cancelamento = new(2, "Cancelamento");
        public static LogTurmaAcao Adiamento = new(2, "Adiamento");
        public static LogTurmaAcao Iniciada = new(2, "Iniciada");

        public LogTurmaAcao() { }
        public LogTurmaAcao(int id, string name) : base(id, name) { }

        
    }
}
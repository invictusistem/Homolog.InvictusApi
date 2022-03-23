using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Logs
{
    public class LogCalendario
    {
        public LogCalendario(Guid calendarioId,
                            Guid colaboradorID,
                            string metodo,
                            DateTime dataCriacao,
                            string oldCommand,
                            string newCommand)
        {
            CalendarioId = calendarioId;
            ColaboradorID = colaboradorID;
            Metodo = metodo;
            DataCriacao = dataCriacao;
            OldCommand = oldCommand;
            NewCommand = newCommand;

        }

        public Guid Id { get; private set; }
        public Guid CalendarioId { get; private set; }
        public Guid ColaboradorID { get; private set; }
        public string Metodo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string OldCommand { get; private set; }
        public string NewCommand { get; private set; }

        public LogCalendario()
        {

        }
    }
}

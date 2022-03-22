using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Logs
{
    public class LogTurmas
    {
        public LogTurmas(
                            Guid turmaId,
                            Guid guidColaborador,
                            DateTime dataCriacao,
                            string createTurmaCommandJson)
        {

            TurmaId = turmaId;
            GuidColaborador = guidColaborador;
            DataCriacao = dataCriacao;
            CreateTurmaCommandJson = createTurmaCommandJson;

        }

        public Guid Id { get; private set; }
        public Guid TurmaId { get; private set; }
        public Guid GuidColaborador { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string CreateTurmaCommandJson { get; private set; }

        public LogTurmas()
        {

        }
    }
}

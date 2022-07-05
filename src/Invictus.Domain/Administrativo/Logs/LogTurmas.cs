using Invictus.Core.Enumerations.Logs;
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
                            LogTurmaAcao acao,
                            Guid guidColaborador,
                            DateTime dataCriacao,
                            Guid unidadeId,
                            string createTurmaCommandJson)
        {

            TurmaId = turmaId;
            Acao = acao.DisplayName;
            GuidColaborador = guidColaborador;
            DataCriacao = dataCriacao;
            UnidadeId = unidadeId;
            CreateTurmaCommandJson = createTurmaCommandJson;

        }

        public Guid Id { get; private set; }
        public string Acao { get; set; }
        public Guid TurmaId { get; private set; }
        public Guid GuidColaborador { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Guid UnidadeId { get; private set; }
        public string CreateTurmaCommandJson { get; private set; }

        public LogTurmas()
        {

        }
    }
}

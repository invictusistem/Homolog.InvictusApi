using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Logs
{
    public class LogMatriculas
    {
        public LogMatriculas(
                            Guid matriculaId,
                            Guid guidColaborador,
                            DateTime dataCriacao,
                            string command)
        {
           
            MatriculaId = matriculaId;
            GuidColaborador = guidColaborador;
            DataCriacao = dataCriacao;
            Command = command;

        }

        public Guid Id { get; private set; }
        public Guid MatriculaId { get; private set; }
        public Guid GuidColaborador { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string Command { get; private set; }

        public LogMatriculas()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.ReportService.Interfaces
{
    public interface IReportServices
    {
        Task<byte[]> GenerateFichaMatricula(GenerateFichaMatriculaDTO infos);
        Task<byte[]> GenerateContrato(GenerateContratoDTO info, Guid typePacoteId);
        Task<byte[]> GenerateContratoExemplo(GenerateContratoDTO info, Guid contratoId);
        Task<byte[]> GeneratePendenciaDocs(Guid matriculaId);
    }
}

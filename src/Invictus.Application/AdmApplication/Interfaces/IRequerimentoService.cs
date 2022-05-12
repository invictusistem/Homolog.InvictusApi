using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IRequerimentoService
    {
        Task SaveRequerimento(RequerimentoDto requerimentoDto);
        Task EditRequerimento(RequerimentoDto requerimentoDto);
        Task SaveTipoRequerimento(TipoRequerimentoDto tipo);
        Task EditTipoRequerimento(TipoRequerimentoDto tipo);

    }
}

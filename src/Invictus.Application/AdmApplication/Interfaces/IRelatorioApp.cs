using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IRelatorioApp
    {
        void ReadAndSaveExcel();
        List<MatriculaCommand> MatriculaExcel();
        void DeleteExcel();
    }
}

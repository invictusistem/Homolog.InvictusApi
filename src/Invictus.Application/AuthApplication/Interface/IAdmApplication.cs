using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AuthApplication.Interface
{
    public interface IAdmApplication
    {
        void ReadAndSaveExcelAlunosCGI();
        void ReadAndSaveExcelAlunosSJM();
    }
}

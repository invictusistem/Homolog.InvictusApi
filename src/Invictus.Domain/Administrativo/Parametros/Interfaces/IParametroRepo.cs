using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Parametros.Interfaces
{
    public interface IParametroRepo : IDisposable
    {
        Task AddParamValue(ParametrosValue parametro);
        void Commit();
    }
}

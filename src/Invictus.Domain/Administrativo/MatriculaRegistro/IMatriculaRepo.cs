using Invictus.Domain.Administrativo.RegistroMatricula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.MatriculaRegistro
{
    public interface IMatriculaRepo : IDisposable
    {
        Task Save(Matricula newMatricula);
        void Commit();
    }
}

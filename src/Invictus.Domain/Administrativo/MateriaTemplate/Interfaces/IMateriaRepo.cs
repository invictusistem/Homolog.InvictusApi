using Invictus.Domain.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.MatTemplate.Interfaces
{
    public interface IMateriaRepo : IDisposable
    {
        Task Save(MateriaTemplate materia);
        Task Edit(MateriaTemplate materia);
        void Commit();
    }
}

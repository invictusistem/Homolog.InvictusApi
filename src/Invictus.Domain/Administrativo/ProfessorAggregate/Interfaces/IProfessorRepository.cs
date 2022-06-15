using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.ProfessorAggregate.Interfaces
{
    public interface IProfessorRepository : IDisposable
    {
        //Task AddProfessor(Professor professor);
        //Task EditProfessor(Professor professor);
        Task AddProfessorMateria(MateriaHabilitada profMateria);
        Task RemoveProfessorMateria(Guid profMateriaId);
        Task AddDisponibilidade(Disponibilidade disponibilidade);
        Task EditDisponibilidade(Disponibilidade disponibilidade);
        Task RemoveDisponibilidade(Disponibilidade disponibilidade);

        void Save();
    }
}

using Invictus.Domain.Administrativo.ProfessorAggregate;
using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IProfessorApplication
    {
        Task SaveProfessor(ProfessorDto newProfessor);
        Task EditProfessor(ProfessorDto editedProfessor);
        Task AddProfessorMateria(Guid profId, Guid materiaId);
        Task RemoveProfessorMateria(Guid profMateriaId);
        Task AddDisponibilidade(DisponibilidadeDto disponibilidade);
        Task EditDisponibilidade(DisponibilidadeDto disponibilidade);
    }
}

using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface ITurmaApplication
    {
        Task CreateTurma(CreateTurmaCommand command); //CreateTurmaCommand command
        Task IniciarTurma(Guid turmaId);
        Task AdiarInicio(Guid turmaId);
        Task AddProfessoresNaTurma(SaveProfsCommand command);
        Task SetMateriaProfessor(Guid turmaId, Guid professorId, IEnumerable<MateriaView> profsMatCommand);
        Task RemoverProfessorDaTurma(Guid professorId, Guid turmaId);
    }
}

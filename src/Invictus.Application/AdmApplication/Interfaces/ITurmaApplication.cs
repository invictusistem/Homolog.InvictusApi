using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface ITurmaApplication
    {
        Task CreateTurma(CreateTurmaCommand command); //CreateTurmaCommand command
        Task IniciarTurma(Guid turmaId);
        Task AdiarInicio(Guid turmaId);
    }
}

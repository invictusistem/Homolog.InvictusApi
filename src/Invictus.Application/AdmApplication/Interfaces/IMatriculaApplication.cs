using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IMatriculaApplication
    {
        Task<Guid> Matricular();
        void AddParams(Guid turmaId, Guid alunoId, MatriculaCommand command);
        Task SetAnotacao(AnotacaoDto anotacao);
    }
}

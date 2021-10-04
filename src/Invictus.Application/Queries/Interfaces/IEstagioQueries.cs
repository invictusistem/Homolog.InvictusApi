using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Pedagogico;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IEstagioQueries
    {
        Task<List<AlunoDocViewModel>> GetDocsAnalise(int unidade);
        Task<List<EstagioMatriculaViewModel>> GetEstagios();
    }
}

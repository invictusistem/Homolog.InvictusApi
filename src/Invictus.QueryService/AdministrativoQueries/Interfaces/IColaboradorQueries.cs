using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using System;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IColaboradorQueries
    {
        Task<PaginatedItemsViewModel<PessoaDto>> GetColaboradoresByUnidadeId(int itemsPerPage, int currentPage, string paramsJson);
        Task<PessoaDto> GetColaboradoresByEmail(string email);
        Task<PessoaDto> GetColaboradoresById(Guid colaboradorId);
        Task<PessoaDto> GetColaboradoresByIdV2(Guid colaboradorId);
        Task<string> GetEmailFromColaboradorById(Guid colaboradorId);
    }
}

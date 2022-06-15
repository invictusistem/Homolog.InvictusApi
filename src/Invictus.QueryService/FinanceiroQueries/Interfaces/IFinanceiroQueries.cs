using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.Financeiro;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries.Interfaces
{
    public interface IFinanceiroQueries
    {
        Task<IEnumerable<BoletoDto>> GetAllBoletosVencidos();
        Task<IEnumerable<BoletoDto>> GetDebitoAlunos(Guid matriculaId);
        Task<IEnumerable<BoletoDto>> GetContasReceber(string meioPagamentoId,DateTime start, DateTime end);
        Task<BoletoDto> GetContaReceber(Guid id);
        Task<IEnumerable<BoletoDto>> GetContasPagar(string meioPagamentoId, DateTime start, DateTime end);
        Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetAlunosFinanceiro(int itemsPerPage, int currentPage, string paramsJson);
        Task<PaginatedItemsViewModel<BoletoDto>> GetProdutosVendaByRangeDate(int itemsPerPage, int currentPage, string paramsJson);
    }
}

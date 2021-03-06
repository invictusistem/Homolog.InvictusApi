using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IAlunoQueries
    {
        Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetMatriculadosView(int itemsPerPage, int currentPage, string paramsJson);
        Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetMatriculadosViewV2(int itemsPerPage, int currentPage, string paramsJson);
        Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetAllMatriculadosView(int itemsPerPage, int currentPage,string paramsJson);
        Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetSomenteMatriculadosView(int itemsPerPage, int currentPage, string paramsJson);
        Task<IEnumerable<PessoaDto>> FindAlunoByCPForEmailorRG(string cpf, string rg, string email);
        Task<IEnumerable<PessoaDto>> SearchPerCPF(string cpf);
        Task<IEnumerable<PessoaDto>> SearchPerCPFV2(string cpf);
        Task<DateTime> GetIdadeAluno(Guid alunoId);
        Task<PessoaDto> GetAlunoById(Guid alunoId);
        Task<PessoaDto> GetAlunoByMatriculaId(Guid matriculaId);
    }
}

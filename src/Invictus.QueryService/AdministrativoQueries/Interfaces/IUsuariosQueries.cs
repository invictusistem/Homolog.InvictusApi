using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IUsuariosQueries
    {
        Task<PaginatedItemsViewModel<UsuarioDto>> GetUsuarios(int itemsPerPage, int currentPage, string paramsJson);
        Task<UsuarioDto> GetUsuario(Guid colaboradorId);
        Task<CreateUsuarioView> GetCreateUsuarioViewModel(string email);
        Task<IEnumerable<string>> GetAllIdentityRoles();
        Task<IEnumerable<UsuarioAcessoViewModel>> GetUsuarioAcessoViewModel(Guid usuarioId);
    }
}

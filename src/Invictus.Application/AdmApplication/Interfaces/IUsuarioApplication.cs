using Invictus.Dtos.AdmDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IUsuarioApplication
    {
       string GenerateRandomPassword(PasswordOptions opts = null);
        Task CriarAcessoInicial(Guid colaboradorId, string siglaUnidade, Guid unidadeId);
        Task EditarAcesso(List<UsuarioAcessoViewModel> acessos, Guid colabId);
        IEnumerable<string> GetPerfisAutorizados();
    }
}

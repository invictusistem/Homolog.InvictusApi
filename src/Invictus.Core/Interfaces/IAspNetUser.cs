using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Interfaces
{
    public interface IAspNetUser
    {
        //string Name { get; }
        //Guid ObterUserId();
        string ObterUnidadeDoUsuario();
        //string ObterUserToken();
        //string ObterUserRefreshToken();
        Guid GetUnidadeIdDoUsuario();
        Guid ObterUsuarioId();
        bool EstaAutenticado();
        bool PossuiRole(string role);
        IEnumerable<Claim> ObterClaims();
        string ObterRole();
        HttpContext ObterHttpContext();
    }
}

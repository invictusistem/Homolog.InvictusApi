using Invictus.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        //public Guid ObterUserId()
        //{
        //    return EstaAutenticado() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        //}

        //public string ObterUserEmail()
        //{
        //    return EstaAutenticado() ? _accessor.HttpContext.User.GetUserEmail() : "";
        //}

        //public string ObterUserToken()
        //{
        //    return EstaAutenticado() ? _accessor.HttpContext.User.GetUserToken() : "";
        //}

        //public string ObterUserRefreshToken()
        //{
        //    return EstaAutenticado() ? _accessor.HttpContext.User.GetUserRefreshToken() : "";
        //}

        public bool EstaAutenticado()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool PossuiRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public HttpContext ObterHttpContext()
        {
            return _accessor.HttpContext;
        }

        public string ObterUnidadeDoUsuario()
        {
            return _accessor.HttpContext.User.FindFirst("Unidade").Value; //_userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            //throw new NotImplementedException();
        }

        public Guid ObterUsuarioId()
        {
            string guid = _accessor.HttpContext.User.FindFirst("usuarioId").Value;
            return new Guid(guid); // usuarioId
        }

        public string ObterRole()
        {
            var roles = _accessor.HttpContext.User.Claims;

            //var asd = roles.Select(r => r.
            var role = roles.ToList()[11];
            //string role = _accessor.HttpContext.User.FindFirst("role").Value;

            return role.Value;
        }
    }
}

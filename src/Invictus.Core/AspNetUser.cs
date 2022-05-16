using Dapper;
using Invictus.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _config;
        public AspNetUser(IHttpContextAccessor accessor, IConfiguration config)
        {
            _accessor = accessor;
            _config = config;
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

        public Guid GetUnidadeIdDoUsuario()
        {
            var unidadeSigla = _accessor.HttpContext.User.FindFirst("Unidade").Value;

            var query = @"select Unidades.Id from Unidades Where Unidades.Sigla = @sigla";

            using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var unidadeId = connection.QuerySingle<Guid>(query, new { sigla = unidadeSigla });

                connection.Close();

                return unidadeId;

            }
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
            var role = roles.ToList()[12];
            //string role = _accessor.HttpContext.User.FindFirst("role").Value;

            return role.Value;
        }
    }
}

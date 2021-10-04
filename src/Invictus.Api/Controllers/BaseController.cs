using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public abstract class BaseController : ControllerBase
    {
        //private readonly IHttpContextAccessor _userHttpContext;
        //public readonly string unidade;
        //public BaseController(IHttpContextAccessor userHttpContext)
        //{
        //    _userHttpContext = userHttpContext;
        //    unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;

        //}


        //var unidadeUsuario = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
        //var unidadeId = _context.Unidades.Where(u => u.Sigla == unidadeUsuario).Select(s => s.Id).SingleOrDefault();
    }
}

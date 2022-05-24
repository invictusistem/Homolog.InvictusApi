using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Administrativo
{
    [Route("api/acessos")]
    [Authorize]
    [ApiController]
    public class AcessoController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetAcessos()
        {
            var jsonAcessos = @"{""financeiro"":[""configuracoes"":[""bancos"":[""create"":true,""edit"": false]]],""formarecebimento"": null }";

            var acessos = JsonConvert.DeserializeObject<object>(jsonAcessos);

            return Ok(new { acessos = acessos });
        }

    }
}

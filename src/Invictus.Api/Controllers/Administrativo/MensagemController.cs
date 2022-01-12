using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Administrativo
{
    [Route("api/mensagem")]
    [Authorize]
    [ApiController]
    public class MensagemController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetMessage()
        {

            var mensagem = "Bem-vindo ao sistema Invictus!";
            return Ok(new { mensagem = mensagem });
        }
    }
}

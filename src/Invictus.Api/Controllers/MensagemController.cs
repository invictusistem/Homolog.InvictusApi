using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/mensagem")]
    public class MensagemController : ControllerBase
    {
        private readonly InvictusDbContext _db;
        public MensagemController(InvictusDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult SaveMessage([FromBody] Content content)
        {
            var msg = new Mensagem(0, content.content, DateTime.Now, DateTime.Now, "SuperAdm");
            _db.Mensagens.Add(msg);
            _db.SaveChanges();

            return Ok();
        }


        [HttpGet]
        public IActionResult GetMessage()
        {
            //var message = await _db.Mensagens.FindAsync(1);

            //return Ok(message.Conteudo);
            var message = "Tenha um bom dia.";
            return Ok(new { message = message });
        }
    }

    public class Content
    {
        public string content { get; set; }
    }
}

using Invictus.Data.Context;
using Invictus.QueryService.AlunoSia.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Aluno
{
    [ApiController]
    [Authorize]
    [Route("api/sia/estagio")]
    public class AlunoEstagioController : ControllerBase
    {
        private readonly IAlunoSiaQueries _alunoSiaQueries;
        private readonly InvictusDbContext _db;
        public AlunoEstagioController(IAlunoSiaQueries alunoSiaQueries, InvictusDbContext db)
        {
            _alunoSiaQueries = alunoSiaQueries;
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            // verificar se tem matricula liberada

            // verificar se os docs ja foram enviados ou nao

            // Ou mostrar estágios

            return Ok(new { status = "no aguardo" });

        }

        [HttpGet]
        [Route("documentacao")]
        public async Task<IActionResult> GetDocumentacao()
        {
            // verificar se tem matricula liberada

            // verificar se os docs ja foram enviados ou nao

            // Ou mostrar estágios

            var documentos = await _alunoSiaQueries.GetDocumentosEstagio(new Guid("D41BC26F-DC80-4F33-8EEC-3727B37846C0"));

            return Ok(new { documentos = documentos });

        }

        [HttpPost]        
        [Route("documentacao/{documentoId}")]
        public async Task<IActionResult> Index(Guid documentoId, IFormFile file)
        {
            // IMPORANTE VERIFICAR SE O ALUNO DO TOKEN É O MSM DO DOCUMENTID

            var fileName = Path.GetFileName(file.FileName);

            var fileExtension = Path.GetExtension(fileName);

            var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

            var documento = await _db.DocumentosEstagio.FindAsync(documentoId);            

            byte[] arquivo = null;

            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                arquivo = target.ToArray();
            }

            documento.AddFileByAluno(arquivo, file.ContentType, fileName);

            await _db.DocumentosEstagio.SingleUpdateAsync(documento);
            
            _db.SaveChanges();

            return Ok();
        }
    }
}

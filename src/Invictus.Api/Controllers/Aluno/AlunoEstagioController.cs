using Invictus.Core.Enumerations;
using Invictus.Data.Context;
using Invictus.QueryService.AlunoSia.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
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
            var status = "";

            var estagio = await _db.MatriculasEstagios.Include(e => e.Documentos).Where(e => e.MatriculaId == new Guid("D41BC26F-DC80-4F33-8EEC-3727B37846C0")).FirstOrDefaultAsync();
            if (estagio == null) return Ok(new { status = "sem liberação" });

            var docsLiberados = estagio.Documentos.Where(d => d.Status == StatusDocumento.Aprovado.DisplayName);

            if(docsLiberados.Count() == estagio.Documentos.Count())
            {
                //var natriculas = pegar os estagios em que está matriculado
                //var estagios = pegar os liberados para se matricular
                return Ok(new { status = "liberado" });
            }
            else
            {
                return Ok(new { status = "no aguardo" });
            }            

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

        [HttpGet]
        [Route("tipo/{matriculaId}")]
        public async Task<IActionResult> GetEstagiosLiberados(Guid matriculaId)
        {
            var tipos = await _alunoSiaQueries.GetEstagiosTiposLiberadosDoAluno(matriculaId);

            return Ok(new { tipos = tipos });

        }

        [HttpGet]
        [Route("liberados/{tipoEstagioId}")]
        public async Task<IActionResult> GetEstagios(Guid tipoEstagioId)
        {
            var estagios = await _alunoSiaQueries.GetEstagiosLiberados(tipoEstagioId);

            return Ok(new { estagios = estagios });
        }

        [HttpGet]
        [Route("busca/{estagioId}")]
        public async Task<IActionResult> GetEstagio(Guid estagioId)
        {
            var estagio = await _alunoSiaQueries.GetEstagio(estagioId);

            return Ok(new { estagio = estagio });
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

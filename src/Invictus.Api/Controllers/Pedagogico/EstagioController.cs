using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Pedagogico
{
    [Route("api/estagio")]
    [Authorize]
    [ApiController]
    public class EstagioController : ControllerBase
    {
        private readonly IEstagioApplication _estagioApp;
        private readonly IEstagioQueries _estagioQueries;
        private readonly IAlunoQueries _alunoQueries;
        private readonly InvictusDbContext _db;
        private readonly IAspNetUser _aspUser;
        public EstagioController(IEstagioApplication estagioApp, IEstagioQueries estagioQueries, IAlunoQueries alunoQueries, InvictusDbContext db,
            IAspNetUser aspUser)
        {
            _estagioApp = estagioApp;
            _estagioQueries = estagioQueries;
            _alunoQueries = alunoQueries;
            _db = db;
            _aspUser = aspUser;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstagios()
        {
            var estagios = await _estagioQueries.GetEstagios();

            if (!estagios.Any()) return NotFound();

            return Ok(new { estagios = estagios });
        }

        [HttpGet]
        [Route("{estagioId}")]
        public async Task<IActionResult> GetEstagio(Guid estagioId)
        {
            var estagio = await _estagioQueries.GetEstagioById(estagioId);

            var tiposEstagios = await _estagioQueries.GetTiposDeEstagios();

            return Ok(new { estagio = estagio, tipos = tiposEstagios });
        }

        [HttpGet]
        [Route("alunos")]
        public async Task<IActionResult> GetAlunoByFilter([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            //var results = await _alunoQueries.GetMatriculadosView(itemsPerPage, currentPage, paramsJson);

            var estagioarios = await _estagioQueries.GetMatriculadosView(itemsPerPage, currentPage, paramsJson);

            if (estagioarios.Data.Count() == 0) return NotFound();

            return Ok(estagioarios);
        }        

        [HttpGet]
        [Route("tipos")]
        public async Task<IActionResult> GetTypeEstagios()
        {

            var tiposEstagios = await _estagioQueries.GetTiposDeEstagios();

            if (!tiposEstagios.Any()) return NotFound();

            return Ok(new { tipos = tiposEstagios });
        }

        [HttpGet]
        [Route("aluno/tipos-liberados/{matriculaId}")]
        public async Task<IActionResult> GetTypeEstagios(Guid matriculaId)
        {
            var tiposEstagios = await _estagioQueries.GetTiposDeEstagiosLiberadorParaAluno(matriculaId);

            if (!tiposEstagios.Any()) return NoContent();

            return Ok(new { tipos = tiposEstagios });
        }

        [HttpGet]
        [Route("aluno/{matriculaId}/documentos-estagio")]
        public async Task<IActionResult> GetDocumentos(Guid matriculaId)
        {
            var documentos = await _estagioQueries.GetDocumentosDoEstagio(matriculaId);

            return Ok(new { docs = documentos });
        }

        [HttpGet]
        [Route("document/{estagioDocId}")]
        public async Task<ActionResult> GetFile(Guid estagioDocId)
        {   
            var doc = await _estagioQueries.GetDocumentById(estagioDocId);

            var memory = new MemoryStream(doc.dataFile);

            return File(memory, doc.contentArquivo, doc.nomeArquivo);
        }

        [HttpPost]
        [Route("matricular")]
        public async Task<IActionResult> CreateEstagio([FromBody] LiberarEstagioCommand command)
        {
            await _estagioApp.LiberarMatricula(command);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SaveEstagio([FromBody] EstagioDto estagio)
        {
            await _estagioApp.CreateEstagio(estagio);

            return Ok();
        }

        [HttpPost]
        [Route("tipos")]
        public async Task<IActionResult> EstagioTypeCreate([FromBody] TypeEstagioDto typeEstagio)
        {
            await _estagioApp.CreateTypeEstagio(typeEstagio);

            return Ok();
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

            var userId = _aspUser.ObterUsuarioId();

            documento.AddFileByUsuario(arquivo, file.ContentType, fileName, userId);

            await _db.DocumentosEstagio.SingleUpdateAsync(documento);

            _db.SaveChanges();

            // verificar status matricula
            var docs = await _db.DocumentosEstagio.Where(d => d.MatriculaEstagioId == documento.MatriculaEstagioId).ToListAsync();
            var docsAprovados = docs.Where(d => d.Status == "Aprovado");
            if(docsAprovados.Count() == docs.Count())
            {
                var estagioMatricula = await _db.MatriculasEstagios.FindAsync(documento.MatriculaEstagioId);

                estagioMatricula.ChangeStatusEstagioMatricula(StatusMatricula.AguardoEscolha);

                await _db.MatriculasEstagios.SingleUpdateAsync(estagioMatricula);

                _db.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        //[Authorizarion]
        [Route("arquivos")]
        public IActionResult Index(List<IFormFile> file)
        {
            /*
            if (file != null)
            {
                if (file.Count() > 0)
                {
                    foreach (var item in file)
                    {


                        var fileName = Path.GetFileName(item.FileName);
                        var desc = fileName.Substring(0, 2);

                        //var testarSemanaPort = "sábado";
                        DocumentoDesc doscDesc;
                        DocumentoDesc.TryParse(desc, out doscDesc);

                        var fileExtension = Path.GetExtension(fileName);

                        var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                        var token = HttpContext.GetTokenAsync("Bearer", "access_token");
                        var x = HttpContext.User.Identity.Name;
                        var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
                        //var aluno = _db.Alunos.Where(t => t.Email == email).SingleOrDefault();

                        var aluno = _db.Alunos.Where(a => a.Email == email).FirstOrDefault();//   .OrderBy(c => c.Id).LastOrDefault();

                        var documento = new Documento(0, aluno.Id, doscDesc.GetDescription(), fileName.Remove(0, 2), false, false, fileExtension, item.ContentType, null, DateTime.Now);

                        byte[] arquivo = null;

                        using (var target = new MemoryStream())
                        {
                            item.CopyTo(target);
                            arquivo = target.ToArray();
                        }

                        documento.AddDataByte(arquivo);

                        _db.DocumentosEStagio.Add(documento);
                        _db.SaveChanges();

                        //public IActionResult SalvarLeads([FromQuery] string userEmail, IFormFile file)
                        //{
                        //List<UserModel> users = new List<UserModel>();
                        //List<LeadDto> leadsDto = new List<LeadDto>();
                        //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //using (var stream = new MemoryStream())
                        //{
                        //    item.CopyTo(stream);
                        //    stream.Position = 0;
                        //    using (var reader = ExcelReaderFactory.CreateReader(stream))
                        //    {
                        //        while (reader.Read())
                        //        {
                        //            if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;
                        //            leadsDto.Add(new LeadDto()
                        //            {
                        //                nome = reader.GetValue(0).ToString(),
                        //                email = reader.GetValue(1).ToString(),
                        //                data = reader.GetValue(2).ToString(),
                        //                telefone = reader.GetValue(3).ToString(),
                        //                bairro = reader.GetValue(4).ToString(),
                        //                cursoPretendido = reader.GetValue(5).ToString(),
                        //                unidade = reader.GetValue(6).ToString()
                        //            });
                        //        }
                        //    }
                        //}

                        //leadsDto.RemoveAt(0);
                    }

                }
            }

            //CreateUser();
            */
            return Ok();
        }

        [HttpPut]
        [Route("tipos")]
        public async Task<IActionResult> EstagioTypeEdit([FromBody] TypeEstagioDto typeEstagio)
        {
            await _estagioApp.EditTypeEstagio(typeEstagio);

            return Ok();
        }

        [HttpPut]
        [Route("aluno/{documentoId}/documentos-estagio/{aprovar}")]
        public async Task<IActionResult> EstagioDocumentoAprovar(Guid documentoId, bool aprovar)
        {
            await _estagioApp.AprovarDocumento(documentoId, aprovar);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditEstagio([FromBody] EstagioDto estagio)
        {
            await _estagioApp.EditEstagio(estagio);

            return Ok();
        }

        [HttpDelete]
        [Route("tipo/{estagioTipoId}")]
        public async Task<IActionResult> EstagioTypeDelete(Guid estagioTipoId)
        {
            await _estagioApp.DeleteTypeEstagio(estagioTipoId);

            return Ok();
        }
    }
}

using AutoMapper;
using ExcelDataReader;
using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using Invictus.Core;
using Invictus.Data.Context;
using Invictus.Domain.Pedagogico.EstagioAggregate;
using Invictus.Domain.Pedagogico.Models;
using Invictus.Domain.Pedagogico.Models.IPedagModelRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    
    [ApiController]
    [Route("api/estagios")]
    public class EstagioController : ControllerBase
    {
        private readonly IPedagModelsRepository _pedagModelRepo;
        private readonly IPedagModelsQueries _pedagModelQueries;
        private readonly IMapper _mapper;
        private readonly IEstagioQueries _estagioQuery;
        private readonly IHttpContextAccessor _userHttpContext;
        private readonly InvictusDbContext _db;
        public EstagioController(IPedagModelsRepository pedagModelRepo, IMapper mapper, IPedagModelsQueries pedagModelQueries,
            InvictusDbContext db, IEstagioQueries estagioQuery, IHttpContextAccessor userHttpContext)
        {
            _pedagModelRepo = pedagModelRepo;
            _mapper = mapper;
            _pedagModelQueries = pedagModelQueries;
            _db = db;
            _estagioQuery = estagioQuery;
            _userHttpContext = userHttpContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetEstagios()
        {
            var alunoId = GetUserId();
            // verificar se está se está no terceiro trimestre
            // (ver calendário da turma e ver se está no terceiro, analisando notas
            // nao ? msg: não é possível se matricular
            // sim continua
            // se ja está matriculado em alguma vaga (junto com docs)
            var id = _db.EstagioMatriculas.Where(e => e.AlunoId == alunoId).Select(e => e.AlunoId).FirstOrDefault();

            if (id == 0)
            {
                var docs = _db.DocumentosEStagio.Where(e => e.AlunoId == alunoId).Select(e => e.AlunoId);

                if (docs.Count() == 0)
                {
                    return Ok(new { documentacaoEnviada = false, message = "Envie a documentação para poder se candidatar em uma vaga de estágio" });
                }
                else
                {
                    var documentacoes = _db.DocumentosEStagio.Where(e => e.AlunoId == alunoId);

                    var analisados = documentacoes.Where(doc => doc.Analisado == false).Select(doc => doc.Analisado).ToList();
                    // validados
                    var find = analisados.Contains(false);

                    if (find) return Ok(new { documentacaoEnviada = true, documentosAnalisados = false, message = "A documentação enviada está em análise. Aguarde!" });

                    var validados = documentacoes.Where(doc => doc.Validado == false).Select(doc => doc.Validado).ToList();

                    var findRecusados = validados.Contains(false);

                    if(findRecusados) return Ok(new { documentacaoEnviada = true, documentosAnalisados = false, message = "O(s) seguinte(s) documento(s) foi/foram rejeitado(s), favor reenviar:" });

                    var estagios = await _pedagModelQueries.GetListEstagios();
                    return Ok(new { documentacaoEnviada = true, documentosValidados = true, data = estagios });
                }




            }


            // sim 
            //avisar se já foi analisada a doc ou nao
            // nao
            // mostrar lista de vagas

            //var estagios = await _pedagModelQueries.GetListEstagios();

            return Ok();
        }

        [HttpGet]
        [Route("documentos")]
        public async Task<ActionResult> GetDocs()
        {
            //string unidade = "CGI";

            var unidadeUsuario = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            var unidadeId = _db.Unidades.Where(u => u.Sigla == unidadeUsuario).Select(s => s.Id).SingleOrDefault();

            var documentos = await _estagioQuery.GetDocsAnalise(unidadeId);

            var estagios = await _estagioQuery.GetEstagios();// await _db.Estagios.ToListAsync();

            return Ok(new { documentos = documentos, estagios = estagios });
        }



        [HttpPost]
        public IActionResult SaveEstagio(EstagioDto estagio)
        {
            var newEstagio = _mapper.Map<EstagioDto, Estagio>(estagio);
            _pedagModelRepo.CreateEstagio(newEstagio);

            return Ok();
        }

        [HttpPut]
        public IActionResult EditDocs([FromBody] ValidarDocumentCommand command)
        {
            var doc = _db.DocumentosEStagio.Where(doc => doc.Id == command.docId & doc.AlunoId == command.alunoId).SingleOrDefault();

            doc.ValidarDoc(command.validado);

            _db.DocumentosEStagio.Update(doc);

            _db.SaveChanges();

            // if(command.validado) // domain event notificarAluno

            return Ok();
        }

        [HttpPost]
        //[Authorizarion]
        [Route("arquivos")]
        public IActionResult Index(List<IFormFile> file)
        {
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
            return Ok();
        }







        [HttpGet]
        [Route("file")]
        public async Task<ActionResult> GetFile([FromQuery] int alunoid, [FromQuery] int docid)
        {

            var doc = new Documento();

            await Task.Run(() =>
            {
                doc = _db.DocumentosEStagio.Where(doc => doc.AlunoId == alunoid & doc.Id == docid).SingleOrDefault();
            });
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl);
            //if (!System.IO.File.Exists((filePath))
            //    return NotFound();
            var memory = new MemoryStream(doc.DataFile);


            //await using (var stream = new FileStream(filePath, FileMode.Open))
            //{
            //    await stream.CopyToAsync(memory);
            //}
            //memory.Position = 0;
            //return new FileStreamResult(stream, result.FileType);
            return File(memory, doc.ContentArquivo, doc.Nome);
        }

        //connection.Open();
        //    DynamicParameters dynamicParameters = new DynamicParameters();
        //dynamicParameters.Add(@"ID", FileID);
        //    FileModel result = connection.Query<FileModel>("GetFile", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //Stream stream = new MemoryStream(result.FileData);


        [HttpPost]
        [Route("matriculas")]
        public async Task<IActionResult> MatriculaEstagio([FromQuery] int estagioId)
        {
            var alunoId = GetUserId();
            var aluno = await _db.Alunos.FindAsync(alunoId);

            var newEstagioMatricula = new EstagioMatricula(alunoId, aluno.Nome, aluno.Email, aluno.CPF, estagioId);
            await _db.EstagioMatriculas.AddAsync(newEstagioMatricula);
            await _db.SaveChangesAsync();

            return Ok();
        }



        private int GetUserId()
        {
            //var token = HttpContext.GetTokenAsync("Bearer", "access_token");
            //var x = HttpContext.User.Identity.Name;
            //var token = HttpContext.GetTokenAsync("Bearer", "access_token").GetAwaiter().GetResult();
            //var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            //var aluno = _db.Alunos.Where(t => t.Email == email).SingleOrDefault();
            var userEmail = _userHttpContext.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var alunoId = _db.Alunos.Where(a => a.Email == userEmail).Select(a => a.Id).FirstOrDefault();
           

            return alunoId;// alunoId.Id;

        }


        private void CreateUser()
        {

            var token = HttpContext.GetTokenAsync("Bearer", "access_token");
            var x = HttpContext.User.Identity.Name;
            //var x = UserHttpContext.HttpContext.User.Identity.
            //var currentUser = _userHttpContext.HttpContext.User.GetCurrentUserDetails();
            //var userId = _userHttpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var name = UserHttpContext.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            //var name2 = _userHttpContext.HttpContext.User.FindFirst("Name").Value;


            // find user identity by email 
            var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            // com email, resgatar o ID na tabela Aluno
            var alunoDto = _db.Alunos.Where(t => t.Email == email).SingleOrDefault();
            // com setar id na entidade documentaocap

            // pegar o Id do estágio para colocar na FK entidade estagioMatricula

        }
    }

}

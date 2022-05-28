using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Invictus.Api.Helpers;
using Invictus.Api.HuSignalR;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.ReportService.Interfaces;
using Invictus.BackgroundTasks;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Calendarios;
using Invictus.Domain.Administrativo.FuncionarioAggregate;
using Invictus.Domain.Financeiro.Fornecedores;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/teste")]
    public class TestController : ControllerBase
    {
        public UserManager<IdentityUser> UserManager { get; set; }
        private IHubContext<ChartHub> _hub;
        private IMapper _mapper;
        private readonly IRelatorioApp _relatorioApp;
        private readonly IPDFDesigns _pdfDesign;
        private readonly IAspNetUser _aspNetUser;
        //readonly IBus _bus;
        public RoleManager<IdentityRole> RoleManager { get; set; }
        private IConverter _converter;
        private readonly ITemplate _template;
        private readonly InvictusDbContext _db;
        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TestController> _logger;
        private readonly IMatriculaApplication _matriculaApplication;
        private readonly ITurmaQueries _turmaQueries;

        public TestController(
            ITurmaQueries turmaQueries,
            IRelatorioApp relatorioApp,
            IHubContext<ChartHub> hub,
            BackgroundWorkerQueue backgroundWorkerQueue,
            ILogger<TestController> logger,
            IServiceScopeFactory serviceScopeFactory,
            InvictusDbContext db,
            ITemplate template,
            UserManager<IdentityUser> userMgr,
            IConverter converter,
            IMapper mapper,
            RoleManager<IdentityRole> roleMgr,
            IPDFDesigns pdfDesign,
            IAspNetUser aspNetUser,
            IMatriculaApplication matriculaApplication)
        {
            //_bus = bus;
            _turmaQueries = turmaQueries;
            _relatorioApp = relatorioApp;
            _hub = hub;
            _mapper = mapper;
            UserManager = userMgr;
            RoleManager = roleMgr;
            _converter = converter;
            _template = template;
            _db = db;
            _backgroundWorkerQueue = backgroundWorkerQueue;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _pdfDesign = pdfDesign;
            _matriculaApplication = matriculaApplication;
            _aspNetUser = aspNetUser;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("mass-transit")]
        public async Task<IActionResult> MassTransit()
        {
            //await _bus.Publish(new GettingStarted { Value = $"The time is {DateTimeOffset.Now}" });

            return Ok();
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("calendario/{calendarioId}")]
        public IActionResult UpdateCalendario(Guid calendarioId)
        {
            var calend = _db.Calendarios.Find(calendarioId);


            return Ok(new { calend = calend });

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("criar-funcionario")]
        public IActionResult CriarFuncionario()
        {
            var colabs = _db.Colaboradores.ToList();

            var funcionarios = new List<Funcionario>();

            var userId = new Guid("e94d7dd8-8fef-4c14-8907-88ed8dc934c8");
           
            foreach (var colab in colabs)
            {
                var func = Funcionario.ColaboradorFactory(colab.Id, colab.Nome, colab.Email, colab.CPF, colab.Celular, colab.CargoId, colab.UnidadeId,
                    colab.Ativo, DateTime.Now, colab.Endereco.Bairro, colab.Endereco.CEP, colab.Endereco.Complemento, colab.Endereco.Logradouro,
                    colab.Endereco.Numero, colab.Endereco.Cidade, colab.Endereco.UF, userId);

                funcionarios.Add(func);
            }


            var prof = _db.Professores.ToList();

            
            foreach (var colab in prof)
            {
                var func = Funcionario.ProfessorFactory(colab.Id, colab.Nome, colab.Email, colab.CPF, colab.Celular, colab.UnidadeId,
                    colab.Ativo, DateTime.Now, colab.Endereco.Bairro, colab.Endereco.CEP, colab.Endereco.Complemento, colab.Endereco.Logradouro,
                    colab.Endereco.Numero, colab.Endereco.Cidade, colab.Endereco.UF, userId, colab.CNPJ,colab.DataEntrada,colab.DataSaida,colab.NomeContato,
                    colab.TelefoneContato);

                funcionarios.Add(func);
            }

            var fornec = _db.Fornecedors.ToList();            
            

            foreach (var colab in fornec)
            {
                var func = Funcionario.FornecedorFactory(colab.Id,colab.RazaoSocial, colab.Email,colab.CNPJ_CPF,
                    colab.IE_RG, colab.NomeContato, colab.TelContato,colab.WhatsApp, DateTime.Now, colab.Ativo,
                    colab.Bairro,colab.CEP, colab.Complemento, colab.Logradouro,
                    colab.Numero, colab.Cidade, colab.UF, userId,colab.UnidadeId);

                funcionarios.Add(func);
            }



            _db.Funcionarios.AddRange(funcionarios);

            _db.SaveChanges();

            return Ok();

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("atualizar-boletos-pessoa")]
        public IActionResult AtualizarBoleto()
        {
            var boletos = _db.Boletos.ToList();

            foreach (var boleto in boletos)
            {
                var forne =  _db.Fornecedors.Find(boleto.PessoaId);

                if(forne != null)
                {
                    boleto.SetTipoPessoa(TipoPessoa.Fornecedor.DisplayName);
                }
            }

            foreach (var boleto in boletos)
            {
                var prof = _db.Professores.Find(boleto.PessoaId);

                if (prof != null)
                {
                    boleto.SetTipoPessoa(TipoPessoa.Professor.DisplayName);
                }
            }

            foreach (var boleto in boletos)
            {
                var colab = _db.Colaboradores.Find(boleto.PessoaId);

                if (colab != null)
                {
                    boleto.SetTipoPessoa(TipoPessoa.Colaborador.DisplayName);
                }
            }

            foreach (var boleto in boletos)
            {
                var aluno = _db.Alunos.Find(boleto.PessoaId);

                if (aluno != null)
                {
                    boleto.SetTipoPessoa(TipoPessoa.Aluno.DisplayName);
                }
            }

            foreach (var boleto in boletos)
            {
                var matri = _db.Matriculas.Find(boleto.PessoaId);

                if (matri != null)
                {
                    //var aluno = await _db.Alunos.FindAsync(boleto.PessoaId);

                    boleto.SetTipoPessoa(TipoPessoa.Matriculado.DisplayName);
                }
            }

            _db.Boletos.UpdateRange(boletos);

            _db.SaveChanges();
            /*
             ehFornecedor: fornecedor

            aluno/colaborador/matriculado/professor
             */


            return Ok();

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("calendario")]
        public IActionResult UpdateCalendario([FromBody] TurmaCalendarioViwModel calendDto)
        {
            var calend = _mapper.Map<Calendario>(calendDto);

            _db.Calendarios.Update(calend);
            _db.SaveChanges();
            return Ok();

        }


        [HttpGet]
        [Route("pendencia")]
        public IActionResult GetPendencia()
        {
            //var globalSettings = new GlobalSettings
            //{
            //    ColorMode = ColorMode.Color,
            //    Orientation = Orientation.Portrait,
            //    PaperSize = PaperKind.A4,
            //    Margins = new MarginSettings { Top = 10 },
            //    DocumentTitle = "PDF Report"
            //};
            //var objectSettings = new ObjectSettings
            //{
            //    PagesCount = true,
            //    HtmlContent = _pdfDesign.GetPendenciaDocs(""),//
            //    WebSettings = { DefaultEncoding = "utf-8" },
            //    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "", Line = false },
            //    FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
            //};


            //var pdf = new HtmlToPdfDocument()
            //{
            //    GlobalSettings = globalSettings,
            //    Objects = { objectSettings }
            //};
            //var file = _converter.Convert(pdf);


            //return File(file, "application/pdf");

            return Ok();

        }

        [HttpGet]
        [Route("export-contrato")]
        public IActionResult GetContrato()
        {
            var conteudos = _db.Conteudos.Where(c => c.ContratoId == new Guid("3419e3ed-9d11-45f2-9a91-8017b012e80f")).ToList();

            try
            {
                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report"
                };
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = _template.GetContratoHTMLString(conteudos),// TemplateGenerator.GetHTMLString(),//@"<div><div style=""color: red""> PDF </div></div>", //TemplateGenerator.GetHTMLString(),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "", Line = false },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                var file = _converter.Convert(pdf);



                return File(file, "application/pdf");




            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("export-dk-pdf")]
        public IActionResult ExportPDF()
        {
            try
            {
                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report"
                };
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = _template.GetHTMLString(),// TemplateGenerator.GetHTMLString(),//@"<div><div style=""color: red""> PDF </div></div>", //TemplateGenerator.GetHTMLString(),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "", Line = false },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                var file = _converter.Convert(pdf);
                ///////////////////////////
                ///

                //var docDto = await _pedagDocQueries.GetDocumentById(documentId);
                //var doc = _mapper.Map<AlunoDocumento>(docDto);

                //var fileName = Path.GetFileName(file.FileName);

                //var fileExtension = Path.GetExtension(fileName);

                //var newFileName = String.Concat(System.Convert.ToString(Guid.NewGuid()), fileExtension);

                byte[] arquivo = file;

                //using (var target = new MemoryStream())
                //{
                //    file.CopyTo(target);
                //    arquivo = target.ToArray();
                //}

                var tamanho = arquivo.Length / 1024;

                //var document = new AlunoDocumento()

                //doc.AddDocumento(arquivo, fileName, fileExtension, file.ContentType, System.Convert.ToInt32(tamanho));


                //await _alunoRepo.EditAlunoDoc(doc);




                return File(file, "application/pdf");




            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        //[HttpGet]
        //[Route("boleto-resp")]
        //public async Task<IActionResult> GetAsync()
        //{
        //    var boletos = await _db.Boletos.ToListAsync();
        //    var date = new DateTime(2022, 1, 10, 12, 0, 0);
        //    foreach (var item in boletos)
        //    {
        //        item.SetBoletoDateCadastro(date);
        //    }

        //    _db.Boletos.UpdateRange(boletos);

        //    _db.SaveChanges();

        //    return Ok();
        //}


        [HttpGet]
        [Route("hubtest")]
        public IActionResult Get()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));

            return Ok(new { Message = "Request Completed" });
        }

        [HttpGet]
        [Route("codes")]
        public IActionResult TesteCodes()
        {
            var boletos = _db.Boletos.ToList();

            foreach (var item in boletos)
            {
                item.SetSubConta("MENSALIDADE");
            }

            _db.Boletos.UpdateRange(boletos);

            _db.SaveChanges();
            //  decimal valor1 = 0;
            //  decimal valor2 = 45;
            //  decimal valor3 = 35.5m;

            //  decimal valor4 = 35.5m;
            //  NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
            //  nfi.NumberDecimalSeparator = ".";
            ////  Console.Write(a.ToString(nfi));
            //  string a = valor1.ToString(nfi);
            //  string b = valor2.ToString(nfi);
            //  string c = valor3.ToString(nfi);
            //  string d = valor4.ToString(nfi);



            return Ok();
        }


        [HttpGet]
        [Route("addrole")]
        public async Task<IActionResult> AddRole()
        {
            //var unidade = await _unidadeQueries.GetUnidadeById(newuser.UnidadeId);
            //var usuario = await UserManager.FindByEmailAsync("invictusalc@master.com");

            //await UserManager.AddToRoleAsync(usuario, "");
            //await UserManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", newuser.IsActive.ToString()));
            //await UserManager.AddClaimAsync(user, new System.Security.Claims.Claim("Unidade", unidade.sigla));

            return Ok();
        }

        [HttpGet]
        [Route("senmsg")]
        public IActionResult send()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));

            return Ok(new { Message = "Request Completed" });
        }

        [HttpGet]
        [Route("bck")]
        public async Task<IActionResult> StartBackGround()
        {
            //check user account
            //(bool isStarted, string data) result = backgroundService.Start();
            //work.ExecuteTask();
            await CallSlowApi();


            return Ok("Process start! Plz WAIT");
        }

        private async Task CallSlowApi()
        {
            _backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {
                await Task.Delay(20000);
                _logger.LogInformation($"Done at {DateTime.UtcNow.TimeOfDay}");
            });
        }

        [HttpGet]
        [Route("teste-get")]
        public async Task<IActionResult> TesteGet()
        {

            return Ok();
        }

        [HttpPost]
        [Route("teste-post")]
        public async Task<IActionResult> TestePost()
        {

            return Ok();
        }

        [HttpGet]
        [Route("readexcelalunos")]
        public string ReadExcel()
        {
            _relatorioApp.ReadAndSaveExcel();
            return "invictus Ok";
        }

        [HttpGet]
        [Route("add-fornecedores")]
        public string ReadEFornecedores()
        {
            _relatorioApp.SaveFornecedores();
            return "invictus Ok";
        }

        [HttpGet]
        [Route("atualizar-subcontas")]
        public string Atualizarsubcontas()
        {
            var subcontas = _db.SubContas.ToList();

            foreach (var subs in subcontas)
            {
                subs.Atualizar(subs.Tipo);
            }

            _db.SubContas.UpdateRange(subcontas);

            _db.SaveChanges();

            return "invictus Ok";
        }

        [HttpGet]
        [Route("removerusuario")]
        public async Task<IActionResult> RemoverUsuario()
        {
            var email = "andrietograce@gmail.com";
            var usuario = await UserManager.FindByEmailAsync(email);

            if (usuario != null) await UserManager.DeleteAsync(usuario);


            return Ok();
        }

        [HttpGet]
        [Route("delete-registros")]
        public string DeleteExcel()
        {
            _relatorioApp.DeleteExcel();
            return "invictus Ok";
        }

        [HttpGet]
        [Route("matricula-lotes-dados")]
        public async Task<IActionResult> GetDados()
        {
            var turmas = await _turmaQueries.GetTurmas();

            return Ok(new { turmas = turmas });
        }

        [HttpPost]
        [Route("matricular-registros")]
        public IActionResult MatriculaExcel([FromBody] MatriculaPlanilha matricula)
        {
            var commands = _relatorioApp.MatriculaExcel(matricula);
            return Ok(new { commands = commands });
        }

        [HttpPost]
        [Route("matricular-final-registros/{turmaId}/{alunoId}")]
        public async Task<IActionResult> MatriculaFinalExcel(Guid turmaId, Guid alunoId, [FromBody] MatriculaCommand command)
        {

            //Thread.Sleep(1000);
            //Console.WriteLine(alunoId);
            _matriculaApplication.AddParams(turmaId, alunoId, command);
            var matriculaId = await _matriculaApplication.Matricular();
            //var ids = _relatorioApp.MatriculaExcel();
            return Ok(new { alunoId = alunoId });
        }



        [HttpGet]
        public IActionResult GetInfo()
        {

            /*
            var turmasMaterias = _db.TurmasMaterias.Where(t => t.TurmaId == new Guid("3237628F-F130-459E-85F9-664FE75306CE")).ToList();
            var turma = _db.Turmas.Where(t => t.Id == new Guid("3237628F-F130-459E-85F9-664FE75306CE")).FirstOrDefault();

            var materiasdDoPacote = _db.Materias.Where(p => p.PacoteId == new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a")).ToList();

            var turmasParaAddNaTurma = new List<TurmaMaterias>();

            foreach (var mat in materiasdDoPacote)
            {
                var contem = turmasMaterias.Where(t => t.MateriaId == mat.Id).FirstOrDefault();

                if (contem == null)
                {
                    var mats = new TurmaMaterias(
                        mat.Nome, null, ModalidadeCurso.TryParse(mat.Modalidade), mat.CargaHoraria,  new Guid("8E7B9DA4-783A-4EBA-B56E-A84C60AEADE1"),
                        turma.Id, mat.Ordem, true);

                    turmasParaAddNaTurma.Add(mats);
                }
            }

            _db.TurmasMaterias.AddRange(turmasMaterias);

            _db.SaveChanges();
           
                      
       

            _db.TurmasMaterias.AddRange(turmasMaterias);


           
            */
            return Ok();
        }

        //[HttpGet]
        //[Route("add-role")]
        //public async Task<ActionResult> AddRole()
        //{
        //    //var email = "invictus@teste.com";
        //    var role = "Aluno";
        //    var role1 = "Administrador";
        //    var role2 = "Professor";
        //    var role3 = "SuperAdm";
        //    var role4 = "MasterAdm";
        //    //if (ModelState.IsValid)
        //    //{
        //    IdentityResult result = IdentityResult.Success;
        //    //if (result.Process(ModelState))
        //    //{
        //    //IdentityUser user = await UserManager.FindByEmailAsync(email);

        //    await RoleManager.CreateAsync(new IdentityRole(role));
        //    await RoleManager.CreateAsync(new IdentityRole(role1));
        //    //var role2 = "Professor";
        //    await RoleManager.CreateAsync(new IdentityRole(role2));
        //    //var role3 = "MasterAdm";
        //    await RoleManager.CreateAsync(new IdentityRole(role3));
        //    await RoleManager.CreateAsync(new IdentityRole(role4));
        //    //if (!await UserManager.IsInRoleAsync(user, role))
        //    //{
        //    //    result = await UserManager.AddToRoleAsync(user, role);
        //    //}

        //    //}
        //    //}

        //    return Ok();
        //}

        [HttpPost]
        [Route("salvarteste")]
        public async Task<IActionResult> Salver([FromBody] Testando testa)
        {

            return Ok();
        }

        [HttpPost]
        [Route("salvarteste")]
        public IActionResult EditTurmaMaterias([FromBody] Testando testa)
        {
            var pacoteMaterias = _db.Materias.Where(p => p.PacoteId == new Guid("3B1F2590-897F-4107-80FA-08D9AF7EA64A"));

            var pacote = _db.Pacotes.Find(new Guid("3B1F2590-897F-4107-80FA-08D9AF7EA64A"));

            var horas = 0;

            foreach (var mats in pacoteMaterias)
            {
                horas += mats.CargaHoraria;
            }

            return Ok();
        }
    }

    

    public class Testando
    {
        public string nome { get; set; }
        public Child child { get; set; }
    }

    public class Child
    {
        public string nome { get; set; }
    }
}

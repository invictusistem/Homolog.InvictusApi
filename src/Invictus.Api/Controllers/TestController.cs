using DinkToPdf;
using DinkToPdf.Contracts;
using Invictus.Api.Helpers;
using Invictus.Api.HuSignalR;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.BackgroundTasks;
using Invictus.Core.Enums;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IRelatorioApp _relatorioApp;
        public RoleManager<IdentityRole> RoleManager { get; set; }
        private IConverter _converter;
        private readonly ITemplate _template;
        private readonly InvictusDbContext _db;
        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TestController> _logger;

        public TestController(
            IRelatorioApp relatorioApp,
            IHubContext<ChartHub> hub,
           BackgroundWorkerQueue backgroundWorkerQueue,
           ILogger<TestController> logger,
            IServiceScopeFactory serviceScopeFactory,
            // IHostedService demoService,
            //IBackgroundTaskQueue backgroundTaskQueue,

            InvictusDbContext db,
            ITemplate template,
            UserManager<IdentityUser> userMgr,
            IConverter converter,
            RoleManager<IdentityRole> roleMgr)
        {
            _relatorioApp = relatorioApp;
            _hub = hub;
            UserManager = userMgr;
            RoleManager = roleMgr;
            _converter = converter;
            _template = template;
            _db = db;
            _backgroundWorkerQueue = backgroundWorkerQueue;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            // _demoService = demoService;
            //_backgroundTaskQueue = backgroundTaskQueue ?? throw new ArgumentNullException(nameof(backgroundTaskQueue));

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
                return File(file, "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


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
        [Route("readexcelalunos")]
        public string ReadExcel()
        {
            _relatorioApp.ReadAndSaveExcel();
            return "invictus Ok";
        }



        [HttpGet]
        public IActionResult GetInfo()
        {
            var pacoteDocs = _db.DocumentacoesExigencias.Where(d => d.PacoteId == new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));

            var aphEnfermagem = new Guid("08f55f62-cab5-4008-c364-08d9b478a235");

            var pacDocs = new List<DocumentacaoExigencia>();

            foreach (var item in pacoteDocs)
            {
                var doc = new DocumentacaoExigencia(item.Descricao, item.Comentario, TitularDoc.TryParse(item.Titular), item.ValidadeDias, item.ObrigatorioParaMatricula);
                doc.SetPacoteId(aphEnfermagem);

                pacDocs.Add(doc);

            }

            _db.DocumentacoesExigencias.AddRange(pacDocs);

            _db.SaveChanges();

            var x = pacoteDocs;

            /*
            var colabList = new List<Colaborador>();

            var colab1 = new Colaborador("TESTE COLABORADOR 2", "testecolab2@gmail.com", "12345678914", "21999999999", new Guid("331ba75a-8bbf-41d9-851f-895addb491aa"),
                new Guid("3ded5a63-cc31-4e96-981e-4a7aacc4c76c"), true, new ColaboradorEndereco("BAIRRO TAL", "23050000", "COMPLEMENTO TAL", "LOGRAD TAL", "TAL", "RIO DE JANEIRO", "RJ"));
            colab1.SetDataCriacao();

            var colab2 = new Colaborador("TESTE COLABORADOR 3", "testecolab3@gmail.com", "12345678915", "21999999999", new Guid("331ba75a-8bbf-41d9-851f-895addb491aa"),
                new Guid("3ded5a63-cc31-4e96-981e-4a7aacc4c76c"), true, new ColaboradorEndereco("BAIRRO TAL", "23050000", "COMPLEMENTO TAL", "LOGRAD TAL", "TAL", "RIO DE JANEIRO", "RJ"));
            colab2.SetDataCriacao();

            var colab3 = new Colaborador("TESTE COLABORADOR 4", "testecolab4@gmail.com", "12345678916", "21999999999", new Guid("331ba75a-8bbf-41d9-851f-895addb491aa"),
                new Guid("3ded5a63-cc31-4e96-981e-4a7aacc4c76c"), true, new ColaboradorEndereco("BAIRRO TAL", "23050000", "COMPLEMENTO TAL", "LOGRAD TAL", "TAL", "RIO DE JANEIRO", "RJ"));
            colab3.SetDataCriacao();

            var colab4 = new Colaborador("TESTE COLABORADOR 5", "testecolab5@gmail.com", "12345678917", "21999999999", new Guid("331ba75a-8bbf-41d9-851f-895addb491aa"),
                new Guid("3ded5a63-cc31-4e96-981e-4a7aacc4c76c"), true, new ColaboradorEndereco("BAIRRO TAL", "23050000", "COMPLEMENTO TAL", "LOGRAD TAL", "TAL", "RIO DE JANEIRO", "RJ"));
            colab4.SetDataCriacao();

            var colab5 = new Colaborador("TESTE COLABORADOR 6", "testecolab6@gmail.com", "12345678918", "21999999999", new Guid("331ba75a-8bbf-41d9-851f-895addb491aa"),
                new Guid("3ded5a63-cc31-4e96-981e-4a7aacc4c76c"), true, new ColaboradorEndereco("BAIRRO TAL", "23050000", "COMPLEMENTO TAL", "LOGRAD TAL", "TAL", "RIO DE JANEIRO", "RJ"));
            colab5.SetDataCriacao();

            var colab6 = new Colaborador("TESTE COLABORADOR 7", "testecolab7@gmail.com", "12345678919", "21999999999", new Guid("331ba75a-8bbf-41d9-851f-895addb491aa"),
                new Guid("3ded5a63-cc31-4e96-981e-4a7aacc4c76c"), true, new ColaboradorEndereco("BAIRRO TAL", "23050000", "COMPLEMENTO TAL", "LOGRAD TAL", "TAL", "RIO DE JANEIRO", "RJ"));
            colab6.SetDataCriacao();

            colabList.Add(colab1);
            colabList.Add(colab2);
            colabList.Add(colab3);
            colabList.Add(colab4);
            colabList.Add(colab5);
            colabList.Add(colab6);

            _db.Colaboradores.AddRange(colabList);

            _db.SaveChanges();
            */
            /*
            var pacoteDocs = _db.DocumentacoesExigencias;

            var alunosDocs = _db.AlunosDocs;

            var pacotesSeparadosUm = pacoteDocs.Where(p => p.Id != new Guid("f3045248-ac70-4a96-0e51-08d9af7ea64e"));
            var pacotesSeparadosDois = pacotesSeparadosUm.Where(p => p.Id !=
            new Guid("12745804-ad38-46db-0e52-08d9af7ea64e"));

            foreach (var alunoDoc in alunosDocs.DistinctBy(a => a.MatriculaId))
            {
                foreach (var doc in pacotesSeparadosDois)
                {
                    var docAluno = new AlunoDocumento(alunoDoc.MatriculaId, doc.Descricao, doc.Comentario
                        , false, false, false, doc.ValidadeDias, alunoDoc.TurmaId);

                    _db.AlunosDocs.Add(docAluno);
                }    
            }

            _db.SaveChanges();
            */
            /*
            var guid = new Guid("9ea84227-44fa-45f6-9867-419ce36590b9");

            var listaValues = new List<ParametrosValue>();

            listaValues.Add(new ParametrosValue("01/01", "Ano novo", null, guid));
            listaValues.Add(new ParametrosValue("21/04", "Tiradentes", null, guid));
            listaValues.Add(new ParametrosValue("01/05", "Dia Mundial do Trabalho", null, guid));
            listaValues.Add(new ParametrosValue("03/06", "Corpus Christi", null, guid));
            listaValues.Add(new ParametrosValue("07/11", "Independência do Brasil", null, guid));
            listaValues.Add(new ParametrosValue("12/10", "Nossa Senhora Aparecida", null, guid));
            listaValues.Add(new ParametrosValue("02/11", "Finados", null, guid));
            listaValues.Add(new ParametrosValue("15/11", "Proclamação da República", null, guid));
            listaValues.Add(new ParametrosValue("20/11", "Dia da Consciência Negra", null, guid));
            listaValues.Add(new ParametrosValue("25/12", "Natal", null, guid));

            _db.ParametrosValues.AddRange(listaValues);

            _db.SaveChanges();


            return Ok();
            */
            /*
            var guid = new Guid("D497D437-E7D4-4A7F-9018-7409A22B0128");
            var turmasMAterias = _db.TurmasMaterias.Where(t => t.TurmaId == guid).ToList();

            var guidTurma = new Guid("2A42C6F2-17B3-4727-96BD-35EDE505E445"); // Turma 06

            var turmasMaterias = new List<TurmaMaterias>();
            foreach (var item in turmasMAterias)
            {
                turmasMaterias.Add(new TurmaMaterias(item.Nome, null, ModalidadeCurso.TryParse(item.Modalidade), item.CargaHoraria, true,
                new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), guidTurma, item.MateriaId));
            }

            _db.TurmasMaterias.AddRange(turmasMaterias);

            _db.SaveChanges();


            return Ok();
            */
            //var pacoteMat1 = new PacoteMateria("Teoria e Prática da Enfermagem 1", new Guid("9ee49a46-017c-4522-9b82-08d9b45c5f79"), 3, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat2 = new PacoteMateria("Microbiologia e Parasitologia", new Guid("09a11a07-0b59-4dc7-9b83-08d9b45c5f79"), 4, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat3 = new PacoteMateria("Saúde Coletiva", new Guid("d2cee15e-35a0-420f-9b84-08d9b45c5f79"), 5, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat4 = new PacoteMateria("Teoria e Prática da enfermagem 2", new Guid("d69a0364-473c-4383-9b85-08d9b45c5f79"), 6, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat5 = new PacoteMateria("Saúde Mental", new Guid("bb020ea7-a833-4af2-9b86-08d9b45c5f79"), 7, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat6 = new PacoteMateria("Materno Infantil", new Guid("95bafe07-ba0d-4432-9b87-08d9b45c5f79"), 8, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat7 = new PacoteMateria("Teoria e Prática da Enfermagem 3", new Guid("f7df719a-03a2-4e4d-9b88-08d9b45c5f79"), 9, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat8 = new PacoteMateria("Fundamentos da Enfermagem e Semiologia Técnica", new Guid("4e89cb32-980e-438c-9b89-08d9b45c5f79"), 10, 96, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat9 = new PacoteMateria("Teoria e Prática da Enfermagem 4", new Guid("2410a0d1-a453-4f88-9b8a-08d9b45c5f79"), 11, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat10 = new PacoteMateria("Cuidados de Enfermagem na Clínica Médica", new Guid("fcf4b674-6457-4716-9b8b-08d9b45c5f79"), 12, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat11 = new PacoteMateria("Cuidados de Enfermagem na Clínica Cirúrgica", new Guid("a8692776-d182-4528-9b8c-08d9b45c5f79"), 13, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat12 = new PacoteMateria("Teoria e Prática da Enfermagem 5", new Guid("28e7bdd9-aa9b-4a4c-9b8d-08d9b45c5f79"), 14, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat13 = new PacoteMateria("Estágio 01", new Guid("507be3c4-a0cb-4f04-9b8e-08d9b45c5f79"), 15, 200, Core.Enums.ModalidadeCurso.Estagio, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat14 = new PacoteMateria("Enfermagem em Pacientes Críticos", new Guid("266b5631-0eba-4655-9b8f-08d9b45c5f79"), 16, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat15= new PacoteMateria("TCC em Enfermagem 01", new Guid("5f306b94-833d-4b34-9b90-08d9b45c5f79"), 17, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat16 = new PacoteMateria("Teoria e Prática da Enfermagem 6", new Guid("554d2b63-2aa2-4f48-9b91-08d9b45c5f79"), 18, 48, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat17 = new PacoteMateria("Estágio 02", new Guid("e00a650f-2c17-4406-9b92-08d9b45c5f79"), 19, 200, Core.Enums.ModalidadeCurso.Estagio, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat18 = new PacoteMateria("Teoria e Prática da Enfermagem 7", new Guid("8aea1726-0b82-496d-9b93-08d9b45c5f79"), 20, 32, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat19 = new PacoteMateria("Planejamento de Carreira e Sucesso Profissional", new Guid("0a355547-d960-468f-9b94-08d9b45c5f79"), 21, 32, Core.Enums.ModalidadeCurso.Presencial, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat20 = new PacoteMateria("Estágio 03", new Guid("52de85eb-7818-47ba-9b95-08d9b45c5f79"), 22, 200, Core.Enums.ModalidadeCurso.Estagio, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));
            //var pacoteMat21 = new PacoteMateria("Comunicação Interpessoal e suas Diferentes Linguagens na Enfermagem", new Guid("12b9c2a4-18fe-4a73-9b96-08d9b45c5f79"), 23, 240, Core.Enums.ModalidadeCurso.OnLine, new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a"));

            //_db.Materias.Add(pacoteMat1);
            //_db.Materias.Add(pacoteMat2);
            //_db.Materias.Add(pacoteMat3);
            //_db.Materias.Add(pacoteMat4);
            //_db.Materias.Add(pacoteMat5);
            //_db.Materias.Add(pacoteMat6);
            //_db.Materias.Add(pacoteMat7);
            //_db.Materias.Add(pacoteMat8);
            //_db.Materias.Add(pacoteMat9);
            //_db.Materias.Add(pacoteMat10);
            //_db.Materias.Add(pacoteMat11);
            //_db.Materias.Add(pacoteMat12);
            //_db.Materias.Add(pacoteMat13);
            //_db.Materias.Add(pacoteMat14);
            //_db.Materias.Add(pacoteMat15);
            //_db.Materias.Add(pacoteMat16);
            //_db.Materias.Add(pacoteMat17);
            //_db.Materias.Add(pacoteMat18);
            //_db.Materias.Add(pacoteMat19);
            //_db.Materias.Add(pacoteMat20);
            //_db.Materias.Add(pacoteMat21);

            //_db.SaveChanges();
            //// turmaId = d497d437-e7d4-4a7f-9018-7409a22b0128
            //// 8e7b9da4-783a-4eba-b56e-a84c60aeade1
            //var turmaMateria1 = new TurmaMaterias(pacoteMat1.Nome, null, ModalidadeCurso.TryParse(pacoteMat1.Modalidade), pacoteMat1.CargaHoraria,true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat1.MateriaId);

            //var turmaMateria2 = new TurmaMaterias(pacoteMat2.Nome, null, ModalidadeCurso.TryParse(pacoteMat2.Modalidade), pacoteMat2.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat2.MateriaId);

            //var turmaMateria3 = new TurmaMaterias(pacoteMat3.Nome, null, ModalidadeCurso.TryParse(pacoteMat3.Modalidade), pacoteMat3.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat3.MateriaId);

            //var turmaMateria4 = new TurmaMaterias(pacoteMat4.Nome, null, ModalidadeCurso.TryParse(pacoteMat4.Modalidade), pacoteMat4.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat4.MateriaId);

            //var turmaMateria5 = new TurmaMaterias(pacoteMat5.Nome, null, ModalidadeCurso.TryParse(pacoteMat5.Modalidade), pacoteMat5.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat5.MateriaId);

            //var turmaMateria6 = new TurmaMaterias(pacoteMat6.Nome, null, ModalidadeCurso.TryParse(pacoteMat6.Modalidade), pacoteMat6.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat6.MateriaId);

            //var turmaMateria7 = new TurmaMaterias(pacoteMat7.Nome, null, ModalidadeCurso.TryParse(pacoteMat7.Modalidade), pacoteMat7.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat7.MateriaId);

            //var turmaMateria8 = new TurmaMaterias(pacoteMat8.Nome, null, ModalidadeCurso.TryParse(pacoteMat8.Modalidade), pacoteMat8.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat8.MateriaId);

            //var turmaMateria9 = new TurmaMaterias(pacoteMat9.Nome, null, ModalidadeCurso.TryParse(pacoteMat9.Modalidade), pacoteMat9.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat9.MateriaId);

            //var turmaMateria10 = new TurmaMaterias(pacoteMat10.Nome, null, ModalidadeCurso.TryParse(pacoteMat10.Modalidade), pacoteMat10.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat10.MateriaId);

            //var turmaMateria11 = new TurmaMaterias(pacoteMat11.Nome, null, ModalidadeCurso.TryParse(pacoteMat11.Modalidade), pacoteMat11.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat11.MateriaId);

            //var turmaMateria12 = new TurmaMaterias(pacoteMat12.Nome, null, ModalidadeCurso.TryParse(pacoteMat12.Modalidade), pacoteMat12.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat12.MateriaId);

            //var turmaMateria13 = new TurmaMaterias(pacoteMat13.Nome, null, ModalidadeCurso.TryParse(pacoteMat13.Modalidade), pacoteMat13.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat13.MateriaId);

            //var turmaMateria14 = new TurmaMaterias(pacoteMat14.Nome, null, ModalidadeCurso.TryParse(pacoteMat14.Modalidade), pacoteMat14.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat14.MateriaId);

            //var turmaMateria15 = new TurmaMaterias(pacoteMat15.Nome, null, ModalidadeCurso.TryParse(pacoteMat15.Modalidade), pacoteMat15.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat15.MateriaId);

            //var turmaMateria16 = new TurmaMaterias(pacoteMat16.Nome, null, ModalidadeCurso.TryParse(pacoteMat16.Modalidade), pacoteMat16.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat16.MateriaId);

            //var turmaMateria17 = new TurmaMaterias(pacoteMat17.Nome, null, ModalidadeCurso.TryParse(pacoteMat17.Modalidade), pacoteMat17.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat17.MateriaId);

            //var turmaMateria18 = new TurmaMaterias(pacoteMat18.Nome, null, ModalidadeCurso.TryParse(pacoteMat18.Modalidade), pacoteMat18.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat18.MateriaId);

            //var turmaMateria19 = new TurmaMaterias(pacoteMat19.Nome, null, ModalidadeCurso.TryParse(pacoteMat19.Modalidade), pacoteMat19.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat19.MateriaId);

            //var turmaMateria20 = new TurmaMaterias(pacoteMat20.Nome, null, ModalidadeCurso.TryParse(pacoteMat20.Modalidade), pacoteMat20.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat20.MateriaId);

            //var turmaMateria21 = new TurmaMaterias(pacoteMat21.Nome, null, ModalidadeCurso.TryParse(pacoteMat21.Modalidade), pacoteMat21.CargaHoraria, true,
            //    new Guid("8e7b9da4-783a-4eba-b56e-a84c60aeade1"), new Guid("d497d437-e7d4-4a7f-9018-7409a22b0128"), pacoteMat21.MateriaId);
            ////_db.Materias.Add(pacoteMat22);

            //_db.TurmasMaterias.Add(turmaMateria1);
            //_db.TurmasMaterias.Add(turmaMateria2);
            //_db.TurmasMaterias.Add(turmaMateria3);
            //_db.TurmasMaterias.Add(turmaMateria4);
            //_db.TurmasMaterias.Add(turmaMateria5);
            //_db.TurmasMaterias.Add(turmaMateria6);
            //_db.TurmasMaterias.Add(turmaMateria7);
            //_db.TurmasMaterias.Add(turmaMateria8);
            //_db.TurmasMaterias.Add(turmaMateria9);
            //_db.TurmasMaterias.Add(turmaMateria10);
            //_db.TurmasMaterias.Add(turmaMateria11);
            //_db.TurmasMaterias.Add(turmaMateria12);
            //_db.TurmasMaterias.Add(turmaMateria13);
            //_db.TurmasMaterias.Add(turmaMateria14);
            //_db.TurmasMaterias.Add(turmaMateria15);
            //_db.TurmasMaterias.Add(turmaMateria16);
            //_db.TurmasMaterias.Add(turmaMateria17);
            //_db.TurmasMaterias.Add(turmaMateria18);
            //_db.TurmasMaterias.Add(turmaMateria19);
            //_db.TurmasMaterias.Add(turmaMateria20);
            //_db.TurmasMaterias.Add(turmaMateria21);

            //_db.SaveChanges();

            return Ok();
        }

        //[HttpGet]
        //[Route("addrole")]
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

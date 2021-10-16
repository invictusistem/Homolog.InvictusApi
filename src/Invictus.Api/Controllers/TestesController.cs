using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static Invictus.Application.Services.EmailMessage;
using System.Reflection;
using Invictus.Domain;
using Invictus.Data.Context;
using Invictus.Api.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using Invictus.Core;
using Invictus.Domain.Pedagogico.Models;
using Invictus.Application.Dtos;
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using ExcelDataReader;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Wkhtmltopdf.NetCore;
using Invictus.Application.AuthApplication.Interface;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html.simpleparser;
using System.Collections;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Pedagogico.EstagioAggregate;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/testando")]
    public class TestesController : ControllerBase
    {
        private IConverter _converter;
     
        private IConfiguration _config { get; }
        public UserManager<IdentityUser> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        private readonly IEmailSender _email;
        private readonly InvictusDbContext _db;
        private readonly IMapper _mapper;
        private readonly IAdmApplication _admApp;
        private readonly IHttpContextAccessor _userHttpContext;
        private readonly string unidade;
        //readonly IGeneratePdf _generatePdf;
        public TestesController(IConfiguration config,
                                UserManager<IdentityUser> userMgr,
                                RoleManager<IdentityRole> roleMgr,
                                IEmailSender email,
                                InvictusDbContext db,
                                IMapper mapper,
                                IAdmApplication admApp,
                                IHttpContextAccessor userHttpContext,
                                IConverter converter
                                // IGeneratePdf generatePdf
                                )
        {
            _config = config;
            UserManager = userMgr;
            RoleManager = roleMgr;
            _email = email;
            _db = db;
            _mapper = mapper;
            _admApp = admApp;
            _userHttpContext = userHttpContext;
            unidade = "CGI";// _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            _converter = converter;
        }
        //public static MemoryStream Pdf(string html)
        //{
        //    StringReader sr = new StringReader(html);

        //    Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

        //    MemoryStream stream = new MemoryStream();

        //    PdfWriter.GetInstance(document, stream);

        //    document.Open();

        //    StyleSheet styles = new StyleSheet();

        //    var unicodeFontProvider = FontFactoryImp.Instance;
        //    unicodeFontProvider.DefaultEmbedding = BaseFont.EMBEDDED;
        //    unicodeFontProvider.DefaultEncoding = BaseFont.IDENTITY_H;

        //    var props = new Hashtable
        //    {

        //        { "font_factory", unicodeFontProvider }
        //    };
        //    var objects = HtmlWorker.ParseToList(sr, styles);
        //    foreach (IElement element in objects)
        //    {
        //        document.Add(element);
        //    }

        //    document.Close();
        //    return stream;
        //}

        [HttpGet]
        [Route("export-dk-pdf")]
        public IActionResult ExportPDF()
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
                HtmlContent = @"<div> PDF </div>", //TemplateGenerator.GetHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf");
        }

        [HttpGet]
        [Route("export-pdf")]
        public IActionResult Export()
        {
            //var html = "";
            //var conteudo = _db.Conteudos.Where(c => c.ContratoId == 1).Select(c => c.Content).ToList();
            //foreach (var item in conteudo)
            //{
            //    html += item;
            //}
            //byte[] arquivo = Pdf(html).ToArray();// await ExportFile();
            //string contentType = "application/pdf";
            ////return Ok(Pdf(html));
            //return File(arquivo, contentType);
            return Ok();
        }

        [HttpGet]
        [Route("testparsedate")]
        public IActionResult testDate()
        {
            //var dataString = "04/10";
            //var dataArray = dataString.Split('/');
            //var data = new Data();
            //data.dia = Convert.ToInt32(dataArray[0]);
            //data.mes = Convert.ToInt32(dataArray[1]);

            //var date1 = new DateTime(2021, 10, 5, 0, 0, 0);
            //int dia = 4;
            //int mes = 10;

            //bool tem1 = date1.Month == data.mes;
            //bool tem2 = date1.Day == data.dia;

            //return Ok(Pdf(html));
            var data = new DateTime(2021, 09, 21, 0, 0, 0);
            var datavinteum = data.AddMonths(21);
            return Ok();
        }

        [HttpGet]
        [Route("readexcelalunos")]
        public string ReadExcel()
        {
            _admApp.ReadAndSaveExcelAlunosCGI();
            return "invictus Ok";
        }

        [HttpGet]

        [Route("testeinvictus")]
        [Authorize]
        public string testeAzure()
        {

            return "invictus Ok";
        }

        [HttpGet]
        [Route("getboleto")]
        public string GetBoleto()
        {
            EnviaRequisicaoGET();
            return "invictus Ok";
        }

        [HttpGet]
        [Route("gerardoscspedencia")]
        public IActionResult GerarDocs()
        {
            //var docsexig = new List<DocumentacaoExigencia>();

            //docsexig.Add(new DocumentacaoExigencia(0, "RG", "RG aluno", 5));
            //docsexig.Add(new DocumentacaoExigencia(0, "CPF", "CPF aluno", 5));
            //docsexig.Add(new DocumentacaoExigencia(0, "Residência", "Comp de residência - aluno", 5));

            //_db.DocsExigencias.AddRange(docsexig);

            //_db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("getboleto")]
        public string POSTBoleto()
        {
            EnviaRequisicaoPOST();
            return "invictus Ok";
        }

        //[HttpGet]
        //[Route("getboletopdf")]
        //public async Task<IActionResult> GetBoletoPDF()
        //{
        //    //EnviaRequisicaoGET();
        //    return await _generatePdf.GetPdf("Views/Testes/Index.cshtml");
        //}

        public static void EnviaRequisicaoPOST()
        {
            var parametross = new Dictionary<string, string>();
            var url = "https://sandbox.pjbank.com.br/recebimentos/27f8b64b8168ee9d9775d3b529e985fd8e3698aa/transacoes";
            //parametross.Add("key1", "value1");
            //parametross.Add("key2", "value2");


            parametross.Add("vencimento", "12/30/2021");
            parametross.Add("valor", "50.75");
            parametross.Add("juros", "0");
            parametross.Add("juros_fixo", "0");
            parametross.Add("multa", "0");
            parametross.Add("multa_fixo", "0");
            parametross.Add("desconto", "20.00");
            parametross.Add("diasdesconto1", "");
            parametross.Add("desconto2", "");
            parametross.Add("diasdesconto2", "");
            parametross.Add("desconto3", "");
            parametross.Add("diasdesconto3", "");
            parametross.Add("nunca_atualizar_boleto", "0");
            parametross.Add("nome_cliente", "Cliente de Exemplo");
            //parametross.Add("email_cliente", "value1");
            parametross.Add("telefone_cliente", "21987333442");
            parametross.Add("cpf_cliente", "Rua Joaquim Vilac");
            parametross.Add("endereco_cliente", "509");
            parametross.Add("complemento_cliente", "");
            parametross.Add("bairro_cliente", "Vila Teixeira");
            parametross.Add("cidade_cliente", "Campinas");
            parametross.Add("estado_cliente", "SP");
            parametross.Add("cep_cliente", "13301510");
            parametross.Add("logo_url", "https://pjbank.com.br/assets/images/logo-pjbank.png");
            parametross.Add("texto", "Texto opcional");
            parametross.Add("instrucoes", "Este é um boleto de exemplo");
            parametross.Add("instrucao_adicional", "Este boleto não deve ser pago pois é um exemplo");
            parametross.Add("grupo", "Boletos001");
            parametross.Add("webhook", "http://example.com.br");
            parametross.Add("pedido_numero", "89724");
            parametross.Add("especie_documento", "DS");
            parametross.Add("pix", "pix-e-boleto");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                HttpResponseMessage response = client.PostAsync(url, new FormUrlEncodedContent(parametross)).Result;
                var tokne = response.Content.ReadAsStringAsync().Result;
            }
        }

        public static void EnviaRequisicaoGET() // ok
        {
            var parametross = new Dictionary<string, string>();
            var url = "https://sandbox.pjbank.com.br/recebimentos/27f8b64b8168ee9d9775d3b529e985fd8e3698aa/transacoes/70355632";
            //parametross.Add("key1", "value1");
            //parametross.Add("key2", "value2");

            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                HttpResponseMessage response = client.GetAsync(url).Result;
                var tokne = response.Content.ReadAsStringAsync().Result;
            }
        }

        [HttpPost]
        [Route("decimalnota")]
        public IActionResult DecimalNotaSave(DecimalNota nota)
        {
            var notaNew = nota.nota;
            return Ok();
        }

        [HttpGet]
        [Route("getdatas")]
        public IActionResult testeDatas()
        {

            var query = @"select 
                          convert(varchar, AvaliacaoUm, 103) AvaliacaoUm, 
                          convert(varchar, SegundachamadaAvaliacaoUm, 103) SegundachamadaAvaliacaoUm  
                          from provasagenda";

            using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = connection.Query<AgendasProvasDto>(query);

                return Ok(results);
            }

            // var datas = _db.ProvasAgenda.ToList();

            //return Ok(datas);
        }

        [HttpGet]
        [Route("createagendas")]
        public IActionResult agendasCreate()
        {
            var materias = _db.Materias.Where(m => m.ModuloId == 1).ToList();
            var agendas = new List<ProvasAgenda>();

            foreach (var mat in materias)
            {
                var agenda = new ProvasAgenda();
                agenda.Factory(5, mat.Id, mat.Descricao);
                agendas.Add(agenda);
            }


            _db.ProvasAgenda.AddRange(agendas);

            _db.SaveChanges();

            return Ok(agendas);


        }


        // SeedData.EnsurePopulated(app, Configuration);

        //[HttpPost]
        //[Route("create-age")]
        //public async Task<ActionResult> SeedB([FromBody] List<AgendasProvasDto> agendas )
        //{


        //    return Ok();
        //}

        [HttpGet]
        [AllowAnonymous]
        [Route("savealunos/{CGIunidadeId}/{SJMunidadeId}")]
        public IActionResult SaveAlunos(int CGIunidadeId, int SJMunidadeId)
        {
            var alunos = new List<Aluno>();
            var aluno1 = new Domain.Administrativo.AlunoAggregate.Aluno(0, "Jade Morão Barrocas", "202000001", null, "64550166029", "239257042", new DateTime(2006, 8, 15, 0, 0, 0),
                "Rio de Janeiro", "RJ", "jade.morao@gmail.com", "21999999999", "Maria", "21999995555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
                "Rio de Janeiro", "RJ", "Campo Grande", null, "observaçoes etc...", CGIunidadeId);

            aluno1.AtivarAluno();

            var aluno2 = new Domain.Administrativo.AlunoAggregate.Aluno(0, "Vitória Carneiro Álvares", "202000002", null, "96595951070", "505449158", new DateTime(2000, 1, 1, 0, 0, 0),
            "Rio de Janeiro", "RJ", "vitoria.carneiro.morao@gmail.com", "21999999999", "Maria", "21999995555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
            "Rio de Janeiro", "RJ", "Campo Grande", null, "observaçoes etc...", SJMunidadeId);
            aluno2.AtivarAluno();
            var aluno3 = new Domain.Administrativo.AlunoAggregate.Aluno(0, "Azael Godoi Santana", "202000003", null, "47410950021", "209151973", new DateTime(2005, 1, 1, 0, 0, 0),
            "Rio de Janeiro", "RJ", "azael.godoi@gmail.com", "21999999999", "Maria", "21999995555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
            "Rio de Janeiro", "RJ", "Campo Grande", null, "observaçoes etc...", SJMunidadeId);
            aluno3.AtivarAluno();
            //var aluno4 = new Domain.Administrativo.AlunoAggregate.Aluno(0, "Vitória da Silva", "202000004", null, "69173153036", "120239759", new DateTime(1999, 6, 10, 0, 0, 0),
            //    "Rio de Janeiro", "RJ", "vitoria.silva@gmail.com", "(21)99999-9999", "Maria", "(21)99999-5555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
            //    "Rio de Janeiro", "RJ", "Campo Grande", null, "observaçoes etc...", unidade3.Id);

            var aluno5 = new Domain.Administrativo.AlunoAggregate.Aluno(0, "João Carlos Machado", "202000005", null, "12345678912", "123456789", new DateTime(1999, 6, 10, 0, 0, 0),
                "Rio de Janeiro", "RJ", "alvaro@teste.com", "21999999999", "Maria", "21999995555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
                "Rio de Janeiro", "RJ", "Campo Grande", null, "observaçoes etc...", SJMunidadeId);
            aluno5.AtivarAluno();

            alunos.Add(aluno1);
            alunos.Add(aluno2);
            alunos.Add(aluno3);
            alunos.Add(aluno5);
            _db.Alunos.AddRange(alunos);

            _db.SaveChanges();

            return Ok();
        }
        [HttpGet]
        [Route("addrole")]
        public async Task<ActionResult> AddRole()
        {
            //var email = "invictus@teste.com";
            var role = "Aluno";
            var role1 = "Administrador";
            var role2 = "Professor";
            var role3 = "SuperAdm";
            var role4 = "MasterAdm";
            //if (ModelState.IsValid)
            //{
            IdentityResult result = IdentityResult.Success;
            //if (result.Process(ModelState))
            //{
            //IdentityUser user = await UserManager.FindByEmailAsync(email);

            await RoleManager.CreateAsync(new IdentityRole(role));
            await RoleManager.CreateAsync(new IdentityRole(role1));
            //var role2 = "Professor";
            await RoleManager.CreateAsync(new IdentityRole(role2));
            //var role3 = "MasterAdm";
            await RoleManager.CreateAsync(new IdentityRole(role3));
            await RoleManager.CreateAsync(new IdentityRole(role4));
            //if (!await UserManager.IsInRoleAsync(user, role))
            //{
            //    result = await UserManager.AddToRoleAsync(user, role);
            //}

            //}
            //}

            return Ok();
        }

        [HttpGet]
        //[Authorize(Roles = "SuperAdm")]
        [Route("seed")] // MasterAdm
        public IActionResult Seed(IApplicationBuilder app)
        {
            SeedData.EnsurePopulated(app, _config);

            return Ok();
        }

        [HttpPost]
        //[Authorize(Roles = "SuperAdm")]
        [Route("date")] // MasterAdm
        public IActionResult Date(AceitandoData data)
        {
            //var date = Convert.ToDateTime(data);

            return Ok(data);
        }

        [HttpGet]
        [Route("email")]
        public async Task<ActionResult> Email()
        {
            await _email.SendEmailAsync("alvaroximenesrj@gmail.com", "Invictus test", "usuário registrado com sucesso");

            return Ok();
        }

        [HttpGet]
        [Route("testeenum")]
        public ActionResult Enum()
        {
            /*
            var novoEnum = new TestandoEnum("descTeste", CardType.Amex);
            _db.Testando.Add(novoEnum);
            _db.SaveChanges();
            */

            /*
            string query = @"select * from Testando where Testando.Descricao = 'descTeste' ";

            using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                
                connection.Open();

                var list = connection.Query<TestandoEnum>(query).FirstOrDefault();

                var x = list;
            }
            */
            return Ok();
        }

        [HttpPost]
        [Route("notas")]
        public IActionResult GetNotas([FromBody] List<NotasDisciplinasDto> model)
        {
            var teste = model;

            return Ok();
        }


        [HttpPost]
        public IActionResult Get([FromBody] TesteModel model)
        {
            var teste = model;

            return Ok();
        }

        [HttpPost]
        [Route("savecargobulk")]
        public IActionResult SaveCargo([FromBody] List<CargoDto> cargosDto)
        {
            var cargos = _mapper.Map<List<Cargo>>(cargosDto);

            foreach (var cargo in cargos)
            {
                cargo.SetDataCriacao();
            }


            _db.Cargos.AddRange(cargos);

            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("salvarform")]
        public IActionResult porForm([FromBody] ModelForm model)
        {
            var teste = model;

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetModel()
        {
            //var teste = await GetDatabase();
            var teste = await GetDatabase2();

            return Ok(teste);
        }

        [HttpPost]
        [Route("salvar-alunos")]
        public IActionResult SalvarAlunos(IFormFile file)
        {
            //List<UserModel> users = new List<UserModel>();
            List<AlunoExcelDto> alunosDto = new List<AlunoExcelDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;


                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;
                        alunosDto.Add(new AlunoExcelDto()
                        {
                            nome = reader.GetValue(1).ToString(),
                            dataCadastro = Convert.ToDateTime(reader.GetValue(2).ToString()),
                            rg = reader.GetValue(4).ToString(),
                            cpf = reader.GetValue(5).ToString(),
                            nascimento = Convert.ToDateTime(reader.GetValue(6).ToString()),
                            logradouro = reader.GetValue(7).ToString(),
                            bairro = reader.GetValue(8).ToString(),
                            cidade = reader.GetValue(9).ToString(),
                            uf = reader.GetValue(10).ToString(),
                            cep = reader.GetValue(11).ToString(),
                            telCelular = reader.GetValue(12).ToString(),
                            telWhatsapp = reader.GetValue(13).ToString(),
                            email = reader.GetValue(14).ToString(),
                            naturalidade = "Rio de Janeiro",
                            naturalidadeUF = "RJ",
                        });
                    }
                }
            }

            alunosDto.RemoveAt(0);

            //var user = _userManager.Users.FirstOrDefault(c => c.Email == userEmail);
            // or, if you have an async action, something like:


            //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            //var usuario = UserHttpContext.HttpContext.User.GetCurrentUserDetails();

            List<Aluno> alunos = new List<Aluno>();
            foreach (var item in alunosDto)
            {
                var lead = _mapper.Map<Aluno>(item);
                /// lead.SetDateAndResponsavelInLead(user.Email + "/" + user.UserName);
                alunos.Add(lead);
            }

            _db.Alunos.AddRange(alunos);



            return Ok();
        }

        [HttpPost]
        [Route("upload-arqaluno/{docId}")]
        public IActionResult SalvarDoc(IFormFile file, int docId)
        {

            var document = _db.DocumentosAlunos.Find(docId);

            


            var fileName = Path.GetFileName(file.FileName);
           // var desc = fileName.Substring(0, 2);


           // DocumentoDesc doscDesc;
           // DocumentoDesc.TryParse(desc, out doscDesc);

            var fileExtension = Path.GetExtension(fileName);

            var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);


            // var aluno = _db.Alunos.OrderBy(c => c.Id).LastOrDefault();
            ///var documento = new Documento(0, aluno.Id, doscDesc.GetDescription(), fileName.Remove(0, 2), false, false, fileExtension, item.ContentType, null, DateTime.Now);
            /// var documento = new Documento(0, aluno.Id, doscDesc.GetDescription(), fileName.Remove(0, 2), false, false, fileExtension, file.ContentType, null, DateTime.Now);

            byte[] arquivo = null;

            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                arquivo = target.ToArray();
            }

            var tamanho = file.Length / 1024;
            document.AddDocumento(arquivo,fileName,fileExtension,file.ContentType, Convert.ToInt32(tamanho));
            document.SetDataCriacao();
            // document.
            //  documento.AddDataByte(arquivo);

            _db.DocumentosAlunos.Update(document);
            _db.SaveChanges();




            return Ok();
        }

        [HttpGet]
        [Route("seedcolaboradores")]
        public IActionResult SeedColaboradores()
        {
            var unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefault();
            _db.Colaboradores.AddRange(
                new Colaborador(
                    0, "Desenolvedor", "invictus@master.com", "12345678912",
                    "21555555555", "Desenvolvedor", 0, unidadeId, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "João da Silva", "joao@gmail.com", "58795248975",
                    "21548957898", "Professor", 0, unidadeId, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "João Teixeira", "joaoteix@gmail.com", "42961742072",
                    "21548957898", "Professor", 0, unidadeId, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Joaquim José", "joaquim@gmail.com", "04605705015",
                    "21548957898", "Professor", 0, unidadeId, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Andre Marques", "andre@gmail.com", "20511464037",
                    "21548957898", "Professor", 0, unidadeId, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Silvio Santos", "silvio@gmail.com", "75448137032",
                    "21548957898", "Professor", 0, unidadeId, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Fausto Silva", "fausto@gmail.com", "53272733000",
                    "21548957898", "Professor", 0, unidadeId, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "João Almeida", "joao2@gmail.com", "50409942065",
                    "21548957898", "Administrador", 0, unidadeId, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
            new Colaborador(
                0, "Mévio Tício", "mevio@gmail.com", "26902480001",
                "21548957898", "Professor", 0, unidadeId, null,
                false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                "Campo Grande", "Rio de Janeiro", "RJ"),
            new Colaborador(
                0, "Antonio Carlos", "antonio@gmail.com", "37490462045",
                "21548957898", "Administrador", 0, unidadeId, null,
                false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                "Campo Grande", "Rio de Janeiro", "RJ"),
            new Colaborador(
                0, "Mario Silva", "mario@gmai.com", "52368455051",
                "21548957898", "Administrador", 0, unidadeId, null,
                false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                "Campo Grande", "Rio de Janeiro", "RJ"),
            new Colaborador(
                0, "Luciano Huck", "luciano@gmai.com", "14269876093",
                "21548957898", "Administrador", 0, unidadeId, null,
                false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                "Campo Grande", "Rio de Janeiro", "RJ")
            );
            _db.SaveChanges();

            return Ok();
        }
        public async Task<TesteModel> GetDatabase()
        {
            var query = @"select nome, cpf from Aluno where aluno.id = 2";
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                var result = await connection.QuerySingleAsync<TesteModel>(query);

                //result.SetCPFMask();
                result.SetCPFBind();

                return result;
            }
        }

        public async Task<dynamic> GetDatabase2()
        {
            var query = @"select nome, cpf from Aluno where aluno.id = 2";
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                var result = await connection.QuerySingleAsync<dynamic>(query);

                //result.SetCPFMask();
                //result.SetCPFBind();

                return result;
            }
        }

        public class AceitandoData
        {
            // [JsonConverter(typeof(DateTimeOffsetConverter))]
            //[DataType(DataType.Date)]
            //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy}")]
            // [JsonConverter(typeof(DateTimeOffsetConverter))]
            public DateTime data { get; set; }

        }
        public class ModelForm
        {

            public FilhoUm _filhosum { get; set; }
            public FilhoDois _filhodois { get; set; }
        }

        public class FilhoUm
        {
            public string nome { get; set; }
            public string sobrenome { get; set; }

        }

        public class DecimalNota
        {
            public decimal nota { get; set; }
        }
        public class FilhoDois
        {
            public string endereco { get; set; }
            public string bairro { get; set; }

        }


        public class TesteModel
        {
            public TesteModel()
            {

            }
            public TesteModel(string cpf)
            {
                //Nome = nome;
                CPF = GetDesc(cpf);
            }
            public string Nome { get; set; }
            public string CPF { get; set; }

            private string GetDesc(string cpf)
            {
                // var cleanCPF = cpf.Trim(new Char[] { '-', '.' });
                var cleanCPF = cpf.Replace(".", "").Replace("-", "");
                //var x = Descricao;
                return cleanCPF;
            }

            public void SetCPFMask()
            {
                var newValue = CPF.Substring(0, 3) + "." +
                CPF.Substring(3, 3) + "." +
                CPF.Substring(6, 3) + "-" +
                CPF.Substring(9, 2);

                CPF = newValue;
            }

            public void SetCPFBind()
            {
                var newValue = "***.***." + CPF.Substring(6, 3) + "-**";

                CPF = newValue;
            }
        }


    }




}

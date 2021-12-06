using DinkToPdf;
using DinkToPdf.Contracts;
using Invictus.Api.Helpers;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/teste")]
    public class TestController : ControllerBase
    {       
        public UserManager<IdentityUser> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        private IConverter _converter;
        private readonly ITemplate _template;
        public TestController(
            ITemplate template,
        UserManager<IdentityUser> userMgr,
            IConverter converter,
            RoleManager<IdentityRole> roleMgr)
        {            
            UserManager = userMgr;
            RoleManager = roleMgr;
            _converter = converter;
            _template = template;
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
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> GetInfo()
        //{
        //    //var obj = new { paramOne = "um", unidadeSigla = "dois" };
        //    //var obj2 = new { obj.paramOne, obj.unidadeSigla };
        //    ////
        //    //var result = await _admQueries.GenericSearch<ColaboradorDto>("Colaborador", "Nome", "João");

        //    //return Ok(new { resultado = result });
        //    return Ok();
        //}

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
    }
}

using ClosedXML.Excel;
using Invictus.Api.Model;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
        private readonly IHttpContextAccessor UserHttpContext;
        public HomeController(IHttpContextAccessor userHttpContext)
        {
            UserHttpContext = userHttpContext;
        }

        [HttpGet, Route("get")]
        public IActionResult Get()
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Admin",
                "alvarocamargorj2018@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(
                "user", "alvarocamargorj2018@gmail.com"));

            emailMessage.Subject = "This is email subject";

            BodyBuilder bodyBuilder = new BodyBuilder();
            //bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
            bodyBuilder.TextBody = "Hello World!";

            emailMessage.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("alvarocamargorj2018@gmail.com", "j24935251");

            client.Send(emailMessage);
            client.Disconnect(true);
            client.Dispose();


            return Ok(new { message = "Hello from Invictus" });
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("download")]
        public IActionResult Download()
        {
            byte[] content;

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Leads");

                var headerTitulo = worksheet.Range(1, 1, 1, 7);
                headerTitulo.Style.Font.FontColor = XLColor.Black;
                headerTitulo.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                headerTitulo.Style.Fill.BackgroundColor = XLColor.LightGreen;
                headerTitulo.Style.Font.FontSize = 14;
                worksheet.Row(1).Height = 15;

                worksheet.Column(1).Width = 25;
                worksheet.Cell(1,1).Value = "NOME";
                worksheet.Column(2).Width = 22;
                worksheet.Cell(1, 2).Value = "EMAIL";
                worksheet.Column(3).Width = 20;
                worksheet.Cell(1, 3).Value = "DATA";
                worksheet.Column(4).Width = 20;
                worksheet.Cell(1, 4).Value = "TELEFONE";
                worksheet.Column(5).Width = 18;
                worksheet.Cell(1, 5).Value = "BAIRRO";
                worksheet.Column(6).Width = 25;
                worksheet.Cell(1, 6).Value = "CURSO PRETENDIDO";
                worksheet.Column(7).Width = 18;
                worksheet.Cell(1, 7).Value = "UNIDADE";

                

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    content = stream.ToArray();
                }
            }

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(content, contentType, "Modelo LEAD.xlsx");

            //Response.ContentType = "application/vnd.ms-excel";
            ////Response.AppendHeader("content-disposition", "attachment; filename=myfile.xls");
            //var folderName = Path.Combine("Resources", "Modelo","Modelo LEAD.xlsx");
            //    var path = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //    var memory = new MemoryStream();
            //    using (var stream = new FileStream(path, FileMode.Open))
            //    {
            //        stream.CopyTo(memory);
            //    }
            //    memory.Position = 0;
            //return File(memory, Response.ContentType = "application/vnd.ms-excel", Path.GetFileName(path));
            
        }

        //[HttpPost, Route("teste")]
        //public IActionResult Getenconding([FromBody] Teste teste)
        //{
        //    var x = UserHttpContext.HttpContext.User.Identity.Name;
        //    //            var x = UserHttpContext.HttpContext.User.Identity.
        //    //var currentUser = UserHttpContext.HttpContext.User.GetCurrentUserDetails();
        //    var userId = UserHttpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    //var name = UserHttpContext.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        //    var name2 = UserHttpContext.HttpContext.User.FindFirst("Name").Value;
        //    var email = UserHttpContext.HttpContext.User.FindFirst(ClaimTypes.Email).Value;


        //    return Ok(new { message = "Hello from Invictus" });
        //}


        //private bool IsAPhotoFile(string fileName)
        //{
        //    return fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)
        //        || fileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase);
        //}

        //[HttpPost, DisableRequestSizeLimit]
        //public async Task<IActionResult> Upload()
        //{
        //    try
        //    {
        //        var formCollection = await Request.ReadFormAsync();
        //        var file = formCollection.Files.First();
        //        var folderName = Path.Combine("Resources", "Images");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);
        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //            return Ok(new { dbPath });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}

        //[HttpPost, Route("login")]
        //public IActionResult Login([FromBody] LoginModel user)
        //{
        //    if (user == null)
        //    {
        //        return BadRequest("Invalid client request");
        //    }
        //    if (user.UserName == "johndoe" && user.Password == "def@123")
        //    {
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
        //        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        //        var claims = new List<Claim>
        //    {
        //    new Claim(ClaimTypes.Name, user.UserName),
        //    new Claim(ClaimTypes.Role, "User")
        //    };
        //        var tokeOptions = new JwtSecurityToken(
        //           issuer: "http://localhost:5000",
        //           audience: "http://localhost:5000",
        //           claims: claims,
        //           expires: DateTime.Now.AddMinutes(5),
        //           signingCredentials: signinCredentials
        //       );
        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        //        return Ok(new { Token = tokenString });
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}
    }

    //public class Teste
    //{
    //    public string job_desc { get; set; }
    //}
    public class CurrentUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string[] Roles { get; set; }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static CurrentUser GetCurrentUserDetails(this ClaimsPrincipal principal)
        {
            if (!principal.Claims.Any())
                return null;

            return new CurrentUser
            {
                Name = principal.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(),
                Email = principal.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault(),
                Roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray(),
                IsActive = Boolean.Parse(principal.Claims.Where(c => c.Type == "IsActive").Select(c => c.Value).SingleOrDefault()),
            };
        }
    }


}

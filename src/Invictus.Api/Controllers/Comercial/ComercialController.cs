using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invictus.Api.Controllers.Comercial
{
    [ApiController]
    [Authorize]
    [Route("api/comercial")]
    public class ComercialController : ControllerBase
    {
        /*

        [HttpPost]
        public IActionResult SalvarLeads([FromQuery] string userEmail, IFormFile file)
        {
            //List<UserModel> users = new List<UserModel>();
            List<LeadDto> leadsDto = new List<LeadDto>();
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
                        leadsDto.Add(new LeadDto()
                        {
                            nome = reader.GetValue(0).ToString(),
                            email = reader.GetValue(1).ToString(),
                            data = reader.GetValue(2).ToString(),
                            telefone = reader.GetValue(3).ToString(),
                            bairro = reader.GetValue(4).ToString(),
                            cursoPretendido = reader.GetValue(5).ToString(),
                            unidade = reader.GetValue(6).ToString()
                        });
                    }
                }
            }

            leadsDto.RemoveAt(0);

            var user = _userManager.Users.FirstOrDefault(c => c.Email == userEmail);
            // or, if you have an async action, something like:


            //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            var usuario = UserHttpContext.HttpContext.User.GetCurrentUserDetails();

            List<Lead> leads = new List<Lead>();
            foreach (var item in leadsDto)
            {
                var lead = _mapper.Map<LeadDto, Lead>(item);
                lead.SetDateAndResponsavelInLead(user.Email + "/" + user.UserName);
                leads.Add(lead);
            }

            _leadRepository.AddLead(leads);

            return Ok();
        }
        */
    }
}
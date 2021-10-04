using AutoMapper;
using ExcelDataReader;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Comercial;
using Invictus.Application.Queries.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Comercial.Leads;
using Invictus.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/comercial")]
    public class ComercialController : BaseController
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor UserHttpContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IComercialQueries _comercialQueries;
        private readonly InvictusDbContext _context;
        public ComercialController(IMapper mapper, IHttpContextAccessor userHttpContext, UserManager<IdentityUser> userManager, ILeadRepository leadRepository,
            IComercialQueries comercialQueries, InvictusDbContext context)
        {
            _mapper = mapper;
            UserHttpContext = userHttpContext;
            _userManager = userManager;
            _leadRepository = leadRepository;
            _context = context;
            _comercialQueries = comercialQueries;
        }

        [HttpPost]
        [Route("teste")]
        public IActionResult SalvarLeadsTEste()
        {
            //HttpContext hfc = Request..Files;

            var modelData = JsonConvert.DeserializeObject<dynamic>(Request.Form["data"]);
            //IFormFile modelData2 = Request.Form["file"];

            List<LeadDto> leadsDto = new List<LeadDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                //file[0].CopyTo(stream);
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
            return Ok();

        }

        [HttpGet]
        [Route("leads")]
        public async Task<ActionResult> LeadsTotal()
        {
            var today = DateTime.Now;
            var leads = await _context.Leads.Where(
                l => l.DataInclusaoSistema.Year == today.Year &
                l.DataInclusaoSistema.Month == today.Month &
                l.DataInclusaoSistema.Day == today.Day).ToListAsync();

            var totalLeads = await _context.Leads.ToListAsync();

            return Ok(new { totalLeadsHoje = leads.Count(), totalLeads = totalLeads.Count() });
        }

        [HttpGet]
        [Route("metrica")]
        public async Task<ActionResult> Metrica()
        {
            var alunos = await _comercialQueries.GetAlunosMatriculados();

            var leads = await _context.Leads.ToListAsync();

            var listMetricaDto = new List<MetricaDto>();

            foreach (var alun in alunos)
            {
                foreach (var lead in leads)
                {
                    if (alun.email.ToLower() == lead.Email.ToLower())
                    {
                        var col = await _context.Colaboradores.Where(l => l.Id == lead.ColaboradorId).FirstOrDefaultAsync();
                        var metrica = new MetricaDto();
                        metrica.DiaMatricula = alun.diaMatricula;
                        metrica.DiaLead = lead.DataInclusaoSistema;
                        metrica.ColaboradorId = col.Id;
                        metrica.NomeColaborador = col.Nome;
                        metrica.EmailColaborador = col.Nome;
                        listMetricaDto.Add(metrica);
                    }
                }
            }


            return Ok();

        }

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

        private void GetLeadList(string userEmail, string file)
        {
            List<LeadDto> leadsDto = new List<LeadDto>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\Resources\Upload"}" + "\\" + file;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
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
                            unidade = reader.GetValue(6).ToString(),
                            dataInclusaoSistema = DateTime.Now,
                            responsavelLead = userEmail
                        });
                    }
                }

                System.IO.File.Delete(fileName);
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

            //_leadRepository.AddLead(leads);



        }


    }


}
/*
DataInclusaoSistema
ResponsavelLead
*/


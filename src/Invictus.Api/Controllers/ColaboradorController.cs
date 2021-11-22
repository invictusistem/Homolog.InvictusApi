using AutoMapper;
using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using Invictus.Core;
using Invictus.Data.Context;
using Invictus.Domain;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Invictus.Application.Services.EmailMessage;

namespace Invictus.Api.Controllers
{
    [ApiController]
    // [Authorize(Roles = "SuperAdm")]// SuperAdm
    [Route("api/colaboradores")]
    public class ColaboradorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly IColaboradorQueries _queries;
        private readonly InvictusDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _email;
        private readonly IHttpContextAccessor _userHttpContext;
        private readonly string unidade;
        public ColaboradorController(IMapper mapper, IColaboradorRepository colaboradorRepository,
            IColaboradorQueries queries,
            IEmailSender email,
            InvictusDbContext context,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor userHttpContext)
        {
            _mapper = mapper;
            _colaboradorRepository = colaboradorRepository;
            _queries = queries;
            _context = context;
            _userManager = userManager;
            _email = email;
            _userHttpContext = userHttpContext;
            unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
        }

        [HttpPost]
        //[Route("")]
        //[Authorize(Roles = "SuperAdm")]
        public IActionResult SaveColaborador([FromBody] ColaboradorDto newColaborador)
        {
            // var unidadeUsuario = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            var unidadeId = _context.Unidades.Where(u => u.Sigla == unidade).Select(s => s.Id).SingleOrDefault();
            newColaborador.unidadeId = unidadeId;
            var cargoString = _context.Cargos.Where(c => c.Id == newColaborador.cargoId).Select(c => c.Nome).SingleOrDefault();
            newColaborador.cargo = cargoString;
            try
            {
                var novoEmail = RemoveDiacritics(newColaborador.email).ToLower();
                //string accentedStr;
                //byte[] tempBytes;
                //tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(newColaborador.email);
                //string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
                newColaborador.email = novoEmail;

                var procurar = _queries.SearhColaborador(newColaborador.email).GetAwaiter().GetResult();
                if (procurar != null)
                {
                    return BadRequest(new { mensagem = "Já existe colaborador cadastrado com o e-mail informado." });
                }
                newColaborador.perfilAtivo = false;
                newColaborador.ativo = true;
                var colaborador = _mapper.Map<ColaboradorDto, Colaborador>(newColaborador);
                _colaboradorRepository.AddColaborador(colaborador);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers([FromQuery] string query, [FromQuery] int itemsPerPage, [FromQuery] int currentPage)
        {
            var param = JsonConvert.DeserializeObject<QueryDto>(query);
            var results = await _queries.GetUsuarios(param, itemsPerPage, currentPage);

            //return BadRequest();

            return Ok(results);
        }

        //[HttpGet]
        //[Route("alunos")]
        ////public async Task<IActionResult> BuscarCadastroAluno([FromQuery] string email, [FromQuery] string cpf, [FromQuery] string nome)
        ////public async Task<IActionResult> BuscarCadastroAluno(string email, int cpf, string nome)
        //public async Task<IActionResult> BuscarCadastroAluno([FromQuery] string query)
        //{
        //    var param = JsonConvert.DeserializeObject<QueryDto>(query);
        //    var pessoas = await _matriculaQueries.BuscaAlunos(param.email, param.cpf, param.nome);

        //    BindCPF(ref pessoas);

        //    return Ok(pessoas);
        //}

        //[HttpGet]
        //[Route("professores")]
        ////public async Task<IActionResult> GetProfessores([FromQuery] string unidade, [FromQuery] int itemsPerPage, [FromQuery] int currentPage)
        //public async Task<ActionResult<IEnumerable<ColaboradorDto>>> GetProfessores([FromQuery] string unidade, [FromQuery] int itemsPerPage, [FromQuery] int currentPage)
        //{
        //    //var results = await _queries.GetProfessores(unidade, itemsPerPage, currentPage);
        //    var results = await _queries.GetProfessores(unidade);

        //    return Ok(results);
        //}

        [HttpGet]
        [Route("professores/{turmaId}")]
        //public async Task<IActionResult> GetProfessores([FromQuery] string unidade, [FromQuery] int itemsPerPage, [FromQuery] int currentPage)
        public async Task<ActionResult<IEnumerable<ColaboradorDto>>> GetProfessoresTurma(int turmaId)
        {
            //string unidade = "Campo Grande";
            var unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            //var results = await _queries.GetProfessores(unidade, itemsPerPage, currentPage);
            var results = await _queries.GetProfessores(unidade, turmaId);

            return Ok(results);
        }

        [HttpPut]
        [Route("users")]
        [Authorize(Roles = "SuperAdm")]
        public IActionResult EditUsers([FromQuery] int id, [FromQuery] string perfil, [FromQuery] bool perfilAtivo)
        {

            _colaboradorRepository.EditColaborador(id, perfil, perfilAtivo);

            //return BadRequest();

            return Ok();
        }

        //        [Authorize(AuthenticationSchemes = "Bearer")]
        // [Authorize(Roles = "MasterAdm,SuperAdm")]


        [HttpGet]
        [Route("pesquisar")]
        public async Task<ActionResult<PaginatedItemsViewModel<ColaboradorDto>>> GetColaboradorV2([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var parametros = JsonConvert.DeserializeObject<ParametrosDTO>(paramsJson);
            var unidadeId = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();
            var results = await _queries.GetColaboradores(itemsPerPage, currentPage, parametros, unidadeId);

            var cargos = await _context.Cargos.ToListAsync();

            //return BadRequest();

            return Ok(results);
        }

        [HttpGet]
        [Route("procurar")]
        public async Task<IActionResult> SearchColaborador([FromQuery] string email)
        {
            //Thread.Sleep(2000);

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null) return BadRequest(new { mensagem = "Já existe usuário autorizado com o e-mail informado." });

            var result = await _queries.SearhColaborador(email);

            if (result == null)
                return BadRequest(new { mensagem = "Não foi encontrado nenhum colaborador com o e-mail cadastrado nesta unidade." });

            if (result != null)
                return Ok(new { data = result });

            //if (result.perfil != null)
            //    return BadRequest(new { mensagem = "Já existe usuário autorizado com o e-mail informado." });
            ////se retorna rnulo, n sei se é nule pq n existe ou pq ja tem perfil

            return BadRequest();
        }

        [HttpPut]
        public ActionResult UpdateColaborador([FromBody] ColaboradorDto editedColaborador)
        {
            ///* JSON PATCH
            //var colaborador = _context.Colaboradores.Find(id);
            var colaborador = _mapper.Map<Colaborador>(editedColaborador);
            //patchColaborador.ApplyTo(colDTO);
            //_mapper.Map(colDTO, colaborador);
            _colaboradorRepository.UpdateColaborador(colaborador);
            //_context.Colaboradores.Update(colaborador);
            //_context.SaveChanges();
            //*/
            //var colaborador = _context.Colaboradores.Find(id);
            //colaborador.SetPerfil(perfil, perfilAtivo);
            //_context.Colaboradores.Update(colaborador);
            //_context.SaveChanges();

            return NoContent();
            //_colaboradorRepository.UpdateColaborador(colaborador);
            //return NoContent();
        }

        [HttpPut]
        [Route("{id}/")]
        //[ProducesResponseType((int)HttpStatusCode.NoContent)]
        // [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Update(int id, [FromQuery] string perfil, [FromQuery] bool perfilAtivo)
        {
            /*
            
            buscar o colaborador pelo id
            mudar tabela colaborador atribuindo o perfil (adm, superadm etc...) 
            mudar tabela colaborador atribuindo o perfil Ativo ou inativo
            criar o usuário no identity com o msm email e pesquisar nome vindo da tabela colaborador
            e gerando senha automaticamente
            tudo ok?
            enviar email para o email informando a senha criada e o perfil
             */
            var colaborador = _context.Colaboradores.Find(id);
            colaborador.SetPerfil(perfil);
            colaborador.AtivarPerfil(perfilAtivo);
            var nome = colaborador.Nome.Split(" ");
            var user = new IdentityUser
            {
                UserName = RemoveDiacritics(nome[0]),
                Email = colaborador.Email,
                EmailConfirmed = true
            };

            var password = GenerateRandomPassword();

            var result = await _userManager.CreateAsync(user, password);


            //var result = await _userManager.CreateAsync(user, newuser.Senha);

            //if (result.Succeeded)
            //{
            // await _signInManager.SignInAsync(user, false);
            // return Ok(await GerarJwt(newuser.Email));
            var uni = _context.Unidades.Where(u => u.Sigla == unidade).FirstOrDefault();
            await _userManager.AddToRoleAsync(user, perfil);
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", perfilAtivo.ToString()));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Unidade", uni.Sigla));
            //var usuario = await _userManager.FindByEmailAsync(user.Email);



            var mensagem = "Olá,<br>Segue seu login e senha para acesso ao sistema Invictus:<br>Login: " + colaborador.Email + "<br>Senha: " + password;
            await _email.SendEmailAsync(colaborador.Email, "Invictus Login", mensagem);
            //var usuario = await _userManager.FindByEmailAsync(colaborador.Email);
            /* JSON PATCH
            var colaborador = _context.Colaboradores.Find(id);
            var colDTO = _mapper.Map<ColaboradorDto>(colaborador);
            patchColaborador.ApplyTo(colDTO);
            _mapper.Map(colDTO, colaborador);

            _context.Colaboradores.Update(colaborador);
            _context.SaveChanges();
            */

            //if (!ModelState.IsValid) return CustomResponse(ModelState);



            //var result = _userManager.CreateAsync(user, "Abc*123456").GetAwaiter().GetResult();

            //if (result.Succeeded)
            //{
            //    // await _signInManager.SignInAsync(user, false);
            //    // return Ok(await GerarJwt(newuser.Email));
            //    var usuario = await _userManager.FindByEmailAsync(user.Email);
            //    return CustomResponse(await GerarJwt(usuario));
            //}

            //foreach (var error in result.Errors)
            //{
            //    AdicionarErroProcessamento(error.Description);
            //}

            //colaborador.SetPerfil(perfil, perfilAtivo);
            //_context.Colaboradores.Update(colaborador);
            _context.SaveChanges();

            return NoContent();
            //_colaboradorRepository.UpdateColaborador(colaborador);
            //return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        //[ProducesResponseType((int)HttpStatusCode.NoContent)]
        // [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult Delete(int id)
        {
            //_adminAppService.RemoveGenericTask(genericTaskId);
            _colaboradorRepository.DeleteColaborador(id);
            return NoContent();
        }

        public string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                //RequiredUniqueChars = 4,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonAlphanumeric = true,

            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }


}

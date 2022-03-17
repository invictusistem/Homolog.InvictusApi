using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.Services;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/usuario")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IColaboradorQueries _colaboradorQueries;
        private readonly IProfessorQueries _profQueries;
        private readonly IUsuariosQueries _userQueries;
        private readonly IUsuarioApplication _userApplication;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAspNetUser _aspUser;
        private readonly IEmailSender _email;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UsuarioController(IColaboradorQueries colaboradorQueries, UserManager<IdentityUser> userManager, IAspNetUser aspUser,
            IEmailSender email, IUsuariosQueries userQueries, IProfessorQueries profQueries,
            IUsuarioApplication userApplication, IUnidadeQueries unidadeQueries, SignInManager<IdentityUser> signInManager)
        {
            _colaboradorQueries = colaboradorQueries;
            _userManager = userManager;
            _aspUser = aspUser;
            _email = email;
            _userQueries = userQueries;
            _userApplication = userApplication;
            _unidadeQueries = unidadeQueries;
            _signInManager = signInManager;
            _profQueries = profQueries;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedItemsViewModel<UsuarioDto>>> GetUsuarios([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var usuarios = await _userQueries.GetUsuarios(itemsPerPage, currentPage, paramsJson);

            if (usuarios.Data.Count() == 0) return NotFound();

            return Ok(usuarios);
        }

        [HttpGet]
        [Route("{colaboradorId}")]
        public async Task<IActionResult> GetUsuario(Guid colaboradorId)
        {
            var usuario = await _userQueries.GetUsuario(colaboradorId);

            return Ok(new { usuario = usuario });
        }

        [HttpGet]
        [Route("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _userQueries.GetAllIdentityRoles();

            return Ok(new { roles = roles });
        }

        [HttpGet]
        [Route("acessos/{colaboradorId}")]
        public async Task<IActionResult> GetAcessos(Guid colaboradorId)
        {
            var acessos = await _userQueries.GetUsuarioAcessoViewModel(colaboradorId);

            return Ok(new { acessos = acessos.OrderBy(a => a.podeAlterar) });
        }

        [HttpGet]
        [Route("procurar")]
        public async Task<IActionResult> SearchColaborador([FromQuery] string email)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null) return BadRequest(new { mensagem = "Já existe usuário autorizado com o e-mail informado." });

            //var result = await _colaboradorQueries.GetColaboradoresByEmail(email);
            var result = await _userQueries.GetCreateUsuarioViewModel(email);
            // get CreateUserViewModel = id, Nome, email e cargo

            if (result == null)
                return BadRequest(new { mensagem = "Não foi encontrado nenhum colaborador com o e-mail cadastrado nesta unidade." });

            if (result != null)
            {
                // pegar perfiz permitidos

                var perfis = _userApplication.GetPerfisAutorizados();

                if (!perfis.Any()) return Unauthorized(new { mensagem = "Você não possui autorização para conceder acessos." });

                return Ok(new { result = result, perfis = perfis });
            }

            return BadRequest();
        }



        [HttpPost]
        [Route("{colaboradorId}/")]
        public async Task<ActionResult> ConcederAcesso(Guid colaboradorId, [FromQuery] string perfil, [FromQuery] bool perfilAtivo)
        {

            var colaborador = await _colaboradorQueries.GetColaboradoresById(colaboradorId);// _context.Colaboradores.Find(id);
            
            if(colaborador == null)
            {
                var prof = await _profQueries.GetProfessorById(colaboradorId);
                colaborador.email = prof.email;
                colaborador.nome = prof.nome;
            }
            

            var primeiroNome = colaborador.nome.Split(" ");

            var user = new IdentityUser
            {
                UserName = colaborador.email,
                Email = colaborador.email,
                EmailConfirmed = true
            };

            var senha = GenerateRandomPassword();// GenerateRandomPassword();

            var result = await _userManager.CreateAsync(user, senha);

            if (result.Succeeded)
            {
                var unidadeSigla = _aspUser.ObterUnidadeDoUsuario();
                //var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);
                await _userManager.AddToRoleAsync(user, perfil);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", perfilAtivo.ToString()));
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Unidade", unidadeSigla));

                //await _userApplication.CriarAcessoInicial(colaboradorId, unidadeSigla, unidade.id);

            }
            else
            {
                return BadRequest(new { msg = "Ocorreu um erro ao conceder o acesso. Procure o administrador do sistema." });
            }



            var mensagem = "Olá,<br>Segue seu login e senha para acesso ao sistema Invictus:<br>Login: " + colaborador.email + "<br>Senha: " + senha + "<br> :)";
            try
            {
                await _email.SendEmailAsync(colaborador.email, "Invictus Login", mensagem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "O acesso foi concedido mas não foi possível enviar o e-mail para o usuário."});
            }

            return Ok();
        }

        [HttpPut]
        [Route("aluno-acesso/{email}/{acesso}")]
        public async Task<ActionResult> EditarAcessoAluno(string email, bool acesso)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var claims = await _userManager.GetClaimsAsync(user);

            var claim = claims.Where(c => c.Type == "IsActive").FirstOrDefault();

            await _userManager.RemoveClaimAsync(user, claim);

            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", acesso.ToString()));

            return Ok();
        }

        [HttpPut]
        [Route("envio-acesso/{email}")]
        public async Task<IActionResult> changePassword(string email)
        {  

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            var senha = GenerateRandomPassword();

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, senha);

            var resultado = await _userManager.UpdateAsync(user);
            if (!resultado.Succeeded)
            {
                return BadRequest();
            }

            var mensagem = "Olá,<br>Segue seu login e senha para acesso ao sistema Invictus:<br>Login: " + user.UserName + "<br>Senha: " + senha + "<br> :)";

            await _email.SendEmailAsync(user.Email, "Invictus Login", mensagem);

            return Ok();

        }

        [HttpPut]
        [Route("acessos")]
        public async Task<ActionResult> EditarAcesso(List<UsuarioAcessoViewModel> acessos)
        {
            await _userApplication.EditarAcesso(acessos);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EditarUsuario(UsuarioDto usuario)
        {
            var user = await _userManager.FindByEmailAsync(usuario.email);

            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var claim = claims.Where(c => c.Type == "IsActive").FirstOrDefault();

            // Claim
            await _userManager.RemoveClaimAsync(user, claim);

            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", usuario.ativo.ToString()));

            // Role
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            await _userManager.AddToRoleAsync(user, usuario.roleName);

            //await _userManager.RemoveClaimAsync

            //var siglaUnidade = await _unidadeQueries.GetUnidadeById(unidadeId);   //"ALC";
            ///*
            // trazer a lista de claims, excluindo a que foi SLELECIONADA
            //entao pegar, dar um FORECH CASO o resultado seja maior que 0
            // */
            //var result = claims.Where(c => c.Type == "Unidade" & c.Value != siglaUnidade.sigla);


            //var claim = usuario.
            //await _userManager.RemoveClaimAsync()


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

        public string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                //RequiredUniqueChars = 4,
                //  RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
                //  RequireNonAlphanumeric = true,

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

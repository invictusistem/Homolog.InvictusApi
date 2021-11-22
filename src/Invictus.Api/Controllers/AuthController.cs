using Invictus.Api.Configuration;
using Invictus.Api.Model;
using Invictus.Application.AuthApplication.Interface;
using Invictus.Application.Dtos;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/identity")]
    public class AuthController : MainController//Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly InvictusDbContext _db;
        //private readonly IHttpContextAccessor _userHttpContext;
        private readonly IAuthApplication _authApplication;
        //private readonly string unidade;
        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings, InvictusDbContext db, /*IHttpContextAccessor userHttpContext,*/
            IAuthApplication authApplication)
        {
            _authApplication = authApplication;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            //unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            _db = db;
        }

        [HttpPost("registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar(UserRegister newuser)
        {
            // if (!ModelState.IsValid) return BadRequest();
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = newuser.Nome,
                Email = newuser.Email,
                EmailConfirmed = true

            };

            //string activeClaimValue = "";
            //if(newuser.IsActive == true)
            //{
            //    activeClaimValue = "True";
            //}
            //else
            //{
            //    activeClaimValue = "False";
            //}

            var result = await _userManager.CreateAsync(user, newuser.Senha);

            if (result.Succeeded)
            {
                // await _signInManager.SignInAsync(user, false);
                // return Ok(await GerarJwt(newuser.Email));
                var unidade = _db.Unidades.Where(u => u.Sigla == "ALC").FirstOrDefault();
                await _userManager.AddToRoleAsync(user, newuser.Role);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", newuser.IsActive.ToString()));
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Unidade", unidade.Sigla));
                var usuario = await _userManager.FindByEmailAsync(user.Email);
                // return CustomResponse(await GerarJwt(usuario));
                return CustomResponse();
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.Description);
            }

            // return BadRequest();
            return CustomResponse();
        }

        [ClaimsAuthorize("Administrativo", "Gravar")]
        [HttpGet("teste")]
        public IActionResult Teste()
        {

            return Ok(new { message = "logado!!!!" });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin user)
        {

            // if (!ModelState.IsValid) return BadRequest();
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var usuario = await _userManager.FindByEmailAsync(user.Email);
            var result = await _signInManager.PasswordSignInAsync(usuario, user.Senha, false, true);
            var aluno = await _db.Alunos.Where(a => a.Email == user.Email).FirstOrDefaultAsync();

            if (aluno != null) return CustomResponse(await GerarJwtAluno(usuario, aluno));

            if (result.Succeeded) return CustomResponse(await GerarJwt(usuario));

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou senha incorretos");
            // return BadRequest();
            return CustomResponse();
        }

        private async Task<UserResponseLogin> GerarJwt(IdentityUser user)
        {
            //var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var colaborador = new Colaborador();
            //if (user.Email.ToLower() != "invictus@master.com")
            //{
            colaborador = await _db.Colaboradores.Where(c => c.Email == user.Email).FirstOrDefaultAsync();
            //}
            //var userId = _db.Colaboradores.Wh

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));


            //if (user.Email.ToLower() != "invictus@master.com")
            //{
            var trimName = colaborador.Nome.Split(' ');
            claims.Add(new Claim("Name", trimName[0]));
            //}
            //else
            //{
            //    claims.Add(new Claim("Name", "Desenvolvedor"));
            //}




            //if (user.Email.ToLower() != "invictus@master.com")
            //{

            claims.Add(new Claim("ColaboradorId", Convert.ToString(colaborador.Id)));
            //}
            //else
            //{
            //    claims.Add(new Claim("ColaboradorId", Convert.ToString(1)));
            //}
            //claims.Add(new Claim("Unidade", "Campo Grande"));
            //claims.Add(new Claim("Codigo", "CGI"));
            //claims.Add(new Claim(ClaimTypes.Role, "SuperAdm"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            //var userUnidade = claims?.FirstOrDefault(x => x.Type.Equals("Unidade", StringComparison.OrdinalIgnoreCase))?.Value;
            //var nomeUnidade = await _db.Unidades.Where(u => u.Sigla == userUnidade).Select(u => u.Descricao).FirstOrDefaultAsync();
            //claims.Add(new Claim("NomeUnidade", nomeUnidade));


            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            //validar
            var claimUnidade = identityClaims.FindFirst("Unidade").Value;
            var unidadeBairro = "";
            //if (user.Email.ToLower() != "invictus@master.com")
            //{
            unidadeBairro = await _db.Unidades.Where(u => u.Sigla == claimUnidade).Select(u => u.Bairro).FirstOrDefaultAsync();
            //}
            //else
            //{
            //    unidadeBairro = "CGI";
            //}
            claims.Add(new Claim("UnidadeBairro", unidadeBairro));

            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.Validation,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpireHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key),
                SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new UserResponseLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpireHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Nome = user.UserName,
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }


        private async Task<UserResponseLogin> GerarJwtAluno(IdentityUser user, Aluno aluno)
        {
            //var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            // var aluno = new Aluno();
            //if (user.Email.ToLower() != "invictus@master.com")
            //{
            //    colaborador = await _db.Colaboradores.Where(c => c.Email == user.Email).FirstOrDefaultAsync();
            //}
            //var userId = _db.Colaboradores.Wh

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));


            //if (user.Email.ToLower() != "invictus@master.com")
            //{
            var trimName = aluno.Nome.Split(' ');
            claims.Add(new Claim("Name", trimName[0]));
            //}
            //else
            //{
            //    claims.Add(new Claim("Name", "Desenvolvedor"));
            //}




            //if (user.Email.ToLower() != "invictus@master.com")
            //{

            claims.Add(new Claim("ColaboradorId", Convert.ToString(aluno.Id)));
            //}
            //else
            //{
            //    claims.Add(new Claim("ColaboradorId", Convert.ToString(1)));
            //}
            //claims.Add(new Claim("Unidade", "Campo Grande"));
            //claims.Add(new Claim("Codigo", "CGI"));
            //claims.Add(new Claim(ClaimTypes.Role, "SuperAdm"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            //var userUnidade = claims?.FirstOrDefault(x => x.Type.Equals("Unidade", StringComparison.OrdinalIgnoreCase))?.Value;
            //var nomeUnidade = await _db.Unidades.Where(u => u.Sigla == userUnidade).Select(u => u.Descricao).FirstOrDefaultAsync();
            //claims.Add(new Claim("NomeUnidade", nomeUnidade));


            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            //validar
            var claimUnidade = identityClaims.FindFirst("Unidade").Value;
            var unidadeBairro = "";
            //if (user.Email.ToLower() != "invictus@master.com")
            //{
            unidadeBairro = await _db.Unidades.Where(u => u.Sigla == claimUnidade).Select(u => u.Bairro).FirstOrDefaultAsync();
            // }
            //else
            //{
            //    unidadeBairro = "CGI";
            //}
            claims.Add(new Claim("UnidadeBairro", unidadeBairro));

            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.Validation,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpireHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key),
                SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new UserResponseLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpireHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Nome = user.UserName,
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);//
    }
}

using Invictus.Api.Configurations;
using Invictus.Api.Helpers;
using Invictus.Dtos;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.Identity;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    //[ApiController]
    //[Authorize]
    [Route("api/identity")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IColaboradorQueries _colaboradorQueries;
        private readonly IAutorizacaoQueries _autorizacoesQueries;
        public AuthController(SignInManager<IdentityUser> signInManager, 
                            UserManager<IdentityUser> userManager,
                            IOptions<AppSettings> appSettings,
                            IUnidadeQueries unidadeQueries,
                            IAutorizacaoQueries autorizacoesQueries,
                            IColaboradorQueries colaboradorQueries)
        {
            _colaboradorQueries = colaboradorQueries;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _unidadeQueries = unidadeQueries;
            _autorizacoesQueries = autorizacoesQueries;
        }

        [HttpPost("registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar(UserRegister newuser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = newuser.Nome,
                Email = newuser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, newuser.Senha);

            if (result.Succeeded)
            {
                var unidade = await _unidadeQueries.GetUnidadeById(newuser.UnidadeId);
                await _userManager.AddToRoleAsync(user, newuser.Role);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", newuser.IsActive.ToString()));
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Unidade", unidade.sigla));

                return CustomResponse();
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.Description);
            }

            // return BadRequest();
            return CustomResponse();
        }

        //private void AddUserClaims(IdentityUser user, Guid unidadeId, bool isActive)
        //{
        //    var unidade = await _admQueries.GetUnidadeById(newuser.UnidadeId);
        //    await _userManager.AddToRoleAsync(user, newuser.Role);
        //    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", newuser.IsActive.ToString()));
        //    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Unidade", unidade.sigla));
        //}

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

            // if (result.Succeeded) return Ok(await GerarJwt(user.Email));
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

        private async Task<IEnumerable<AutorizacaoDto>> GetAutorizacoes(string email)
        {
            var colaborador = await _colaboradorQueries.GetColaboradoresByEmail(email);
            var autorizacoes = await _autorizacoesQueries.GetUnidadesAutorizadas(colaborador.id);

            return autorizacoes;
        }

        private async Task<UserResponseLogin> GerarJwt(IdentityUser user)
        {

            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);


            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim("Nome", user.UserName));

            var autorizacoes = await GetAutorizacoes(user.Email);

            var autorizacoesSerialize = JsonSerializer.Serialize(autorizacoes);

            claims.Add(new Claim("UnidadesAutorizadas", autorizacoesSerialize));

            var colaborador = await _colaboradorQueries.GetColaboradoresByEmail(user.Email);

            claims.Add(new Claim("usuarioId", colaborador.id.ToString()));

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            //validar
            var claimUnidade = identityClaims.FindFirst("Unidade").Value;
           

            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor // CreateToken(Key)
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.Validation,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpireHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key),
                SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new UserResponseLogin //GenerateResponse(encodedToken,user,claims)
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

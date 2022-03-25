using Invictus.Api.Configurations;
using Invictus.Api.Helpers;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Logs;
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
        private readonly InvictusDbContext _db;
        public AuthController(SignInManager<IdentityUser> signInManager,
                            UserManager<IdentityUser> userManager,
                            IOptions<AppSettings> appSettings,
                            IUnidadeQueries unidadeQueries,
                            IAutorizacaoQueries autorizacoesQueries,
                            IColaboradorQueries colaboradorQueries,
                            InvictusDbContext db)
        {
            _colaboradorQueries = colaboradorQueries;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _unidadeQueries = unidadeQueries;
            _autorizacoesQueries = autorizacoesQueries;
            _db = db;
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

        [HttpPost("pre-login")]
        [AllowAnonymous]
        public async Task<IActionResult> PreLogin(UserLogin user)
        {
            // if (!ModelState.IsValid) return BadRequest();
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var usuario = await _userManager.FindByEmailAsync(user.Email);
            var result = await _signInManager.PasswordSignInAsync(usuario, user.Senha, false, true);


            // if (result.Succeeded) return Ok(await GerarJwt(user.Email));
            if (result.Succeeded)
            {

                var claims = await _userManager.GetClaimsAsync(usuario);
                var userRoles = await _userManager.GetRolesAsync(usuario);

                //var siglaUnidade = "ALC";
                /*
                 trazer a lista de claims, excluindo a que foi SLELECIONADA
                entao pegar, dar um FORECH CASO o resultado seja maior que 0
                 */

                //var role = userRoles.Where()

                if (userRoles[0] == "Aluno")
                {
                    return BadRequest();
                }

                var ativo = claims.Where(c => c.Type == "IsActive").FirstOrDefault();

                if (Convert.ToBoolean(ativo.Value) == false) return Unauthorized();
                //var x = ativo.Value;


                var unidades = claims.Where(c => c.Type == "Unidade");

                var listaUnidades = new List<string>();

                listaUnidades.AddRange(unidades.Select(u => u.Value));

                var unidadeSelect = new List<UnidadeSelect>();

                foreach (var uni in listaUnidades)
                {
                    var desc = await _unidadeQueries.GetUnidadeBySigla(uni);
                    unidadeSelect.Add(new UnidadeSelect() { unidadeId = desc.id, sigla = uni, value = desc.descricao });
                }



                return Ok(new { unidades = unidadeSelect });    //return CustomResponse(await GerarJwt(usuario));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado. Tente novamente em alguns minutos.");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou senha incorretos.");
            // return BadRequest();
            return CustomResponse();
        }

        [HttpPost("login/{unidadeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin user, Guid unidadeId)
        {
            // if (!ModelState.IsValid) return BadRequest();
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var usuario = await _userManager.FindByEmailAsync(user.Email);
            var result = await _signInManager.PasswordSignInAsync(usuario, user.Senha, false, true);

            // if (result.Succeeded) return Ok(await GerarJwt(user.Email));
            if (result.Succeeded) return CustomResponse(await GerarJwt(usuario, unidadeId));

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado. Tente novamente em alguns minutos.");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou senha incorretos.");
            // return BadRequest();
            return CustomResponse();
        }

        [HttpPut]
        [Route("troca-senha")]
        public async Task<IActionResult> changePassword(TrocaSenha usermodel)
        {


            var user = await _userManager.FindByEmailAsync(usermodel.Email);

            //temp
            //user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, "Abc*123456");
            //var resultado123 = await _userManager.UpdateAsync(user);
            //return Ok();
            //
            if (user == null)
            {
                return NotFound();
            }

            var result = await _signInManager.PasswordSignInAsync(user, usermodel.Senha, false, true);

            if (!result.Succeeded)
            {
                AdicionarErroProcessamento("Usuário ou senha incorretos.");
                return CustomResponse();
            }

            //var passwordValidator = new PasswordValidator<IdentityUser>();
            //var validateSenha = await passwordValidator.ValidateAsync(_userManager, user, usermodel.SenhaConfirmacao);

            //if (!validateSenha.Succeeded)
            //{
            //    AdicionarErroProcessamento("A senha deve conter letras maiúsculas e minúsculas.");
            //    return CustomResponse();
            //}




            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, usermodel.SenhaConfirmacao);

            var resultado = await _userManager.UpdateAsync(user);
            if (!resultado.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        private async Task<IEnumerable<AutorizacaoDto>> GetAutorizacoes(string email)
        {
            var colaborador = await _colaboradorQueries.GetColaboradoresByEmail(email);
            var autorizacoes = await _autorizacoesQueries.GetUnidadesAutorizadas(colaborador.id);

            return autorizacoes;
        }

        private async Task<UserResponseLogin> GerarJwt(IdentityUser user, Guid unidadeId)
        {

            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles[0] == "Aluno")
            {
                throw new NotImplementedException();
            }

            var siglaUnidade = await _unidadeQueries.GetUnidadeById(unidadeId);   //"ALC";
            /*
             trazer a lista de claims, excluindo a que foi SLELECIONADA
            entao pegar, dar um FORECH CASO o resultado seja maior que 0
             */
            var result = claims.Where(c => c.Type == "Unidade" & c.Value != siglaUnidade.sigla);
            //try
            //{
            if (result.Count() > 0)
            {
                foreach (var item in result.ToList())
                {
                    claims.Remove(item);
                }
            }
            //}catch(Exception ex)
            //{

            //}

            var colaborador = await _colaboradorQueries.GetColaboradoresByEmail(user.Email);

            var primeiroNome = colaborador.nome.Split(" ");

            claims.Add(new Claim("UnidadeId", siglaUnidade.id.ToString()));
            claims.Add(new Claim("Nome", primeiroNome[0]));

            var autorizacoes = await GetAutorizacoes(user.Email);

            var autorizacoesSerialize = JsonSerializer.Serialize(autorizacoes);

            claims.Add(new Claim("UnidadesAutorizadas", autorizacoesSerialize));

            // TESTE ACESSOS TELA
            var telas = new List<TelasAcess>();
            telas.Add(new TelasAcess() { path = "./adm", title = "Administrativo", @class = "" });
            telas.Add(new TelasAcess() { path = "./newmat", title = "Matrícula", @class = "" });

            var telasJson = JsonSerializer.Serialize(telas);

            claims.Add(new Claim("Telas", telasJson));

            claims.Add(new Claim("usuarioId", colaborador.id.ToString()));

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));


            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            //identityClaims.RemoveClaim(claims.Where(c => c.Type == "Unidade").FirstOrDefault());

            //validar
            //var claimUnidade = identityClaims.FindFirst("Unidade").Value;
            var claimUnidade = identityClaims.FindAll("Unidade").Select(c => c.Value);


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
            // log login

            var logLogin = new LogLogin(colaborador.id, colaborador.email, DateTime.Now, siglaUnidade.sigla);
            await _db.AddAsync(logLogin);
            try
            {
                _db.SaveChanges();

            }
            catch (Exception ex)
            {

            }

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);//
    }

    public class TelasAcess
    {
        public string path { get; set; }
        public string title { get; set; }
        public string @class { get; set;}

    }


    public class UnidadeSelect
    {
        public Guid unidadeId { get; set; }
        public string sigla { get; set; }
        public string value { get; set; }
    }
}

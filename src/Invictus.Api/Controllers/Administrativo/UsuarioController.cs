﻿using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.Services;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
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
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IColaboradorQueries _colaboradorQueries;
        private readonly IUsuariosQueries _userQueries;
        private readonly IUsuarioApplication _userApplication;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAspNetUser _aspUser;
        private readonly IEmailSender _email;
        private readonly UserManager<IdentityUser> _userManager;
        public UsuarioController(IColaboradorQueries colaboradorQueries, UserManager<IdentityUser> userManager, IAspNetUser aspUser,
            IEmailSender email, IUsuariosQueries userQueries,
            IUsuarioApplication userApplication, IUnidadeQueries unidadeQueries)
        {
            _colaboradorQueries = colaboradorQueries;
            _userManager = userManager;
            _aspUser = aspUser;
            _email = email;
            _userQueries = userQueries;
            _userApplication = userApplication;
            _unidadeQueries = unidadeQueries;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedItemsViewModel<UsuarioDto>>> GetUsuarios([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var usuarios = await _userQueries.GetUsuarios(itemsPerPage, currentPage, paramsJson);

            if (usuarios.Data.Count() == 0) return NotFound();

            return Ok(new { usuarios = usuarios });
        }

        [HttpGet]
        [Route("{colaboradorId}")]
        public async Task<IActionResult> GetUsuario(Guid colaboradorId)
        {
            var usuario = await _userQueries.GetUsuario(colaboradorId);

            return Ok(new { usuario = usuario });
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
                return Ok(new { result = result });

            return BadRequest();
        }



        [HttpPost]
        [Route("{colaboradorId}/")]
        public async Task<ActionResult> ConcederAcesso(Guid colaboradorId, [FromQuery] string perfil, [FromQuery] bool perfilAtivo)
        {
            var colaborador = await _colaboradorQueries.GetColaboradoresById(colaboradorId);// _context.Colaboradores.Find(id);

            var primeiroNome = colaborador.nome.Split(" ");

            var user = new IdentityUser
            {
                UserName = RemoveDiacritics(primeiroNome[0]),
                Email = colaborador.email,
                EmailConfirmed = true
            };

            var senha = GenerateRandomPassword();// GenerateRandomPassword();

            var result = await _userManager.CreateAsync(user, senha);

            if (result.Succeeded)
            {
                var unidadeSigla = _aspUser.ObterUnidadeDoUsuario();
                var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);
                await _userManager.AddToRoleAsync(user, perfil);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", perfilAtivo.ToString()));
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Unidade", unidadeSigla));

                await _userApplication.CriarAcessoInicial(colaboradorId, unidadeSigla, unidade.id);

            }
            else
            {
                return BadRequest();
            }

            

            var mensagem = "Olá,<br>Segue seu login e senha para acesso ao sistema Invictus:<br>Login: " + colaborador.email + "<br>Senha: " + senha+"<br> :)";
            await _email.SendEmailAsync(colaborador.email, "Invictus Login", mensagem);

            return NoContent();
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
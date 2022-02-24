using AutoMapper;
using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Financeiro.Bolsas;
using Invictus.Domain.Financeiro.Bolsas.Interfaces;
using Invictus.Dtos.Financeiro;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.FinancApplication
{
    public class BolsasApp : IBolsasApp
    {
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;
        private readonly IBolsaRepo _bolsaRepo;
        //private readonly IAlunoRepo _alunoRepo;
        private readonly IUnidadeQueries _unidadeQueries;
        public BolsasApp(IMapper mapper, IAspNetUser aspNetUser,
            //, IAlunoRepo alunoRepo,
            IBolsaRepo bolsaRepo,
            IUnidadeQueries unidadeQueries
            )
        {
            _mapper = mapper;
            _aspNetUser = aspNetUser;
            _bolsaRepo = bolsaRepo;
            //  _alunoRepo = alunoRepo;
            _unidadeQueries = unidadeQueries;
        }
        public async Task<string> SaveBolsa(BolsaDto bolsaDto)
        {
            var bolsa = _mapper.Map<Bolsa>(bolsaDto);

            var dataCriacao = DateTime.Now;
            bolsa.SetDataCriacao(dataCriacao);
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);
            bolsa.SetUnidadeId(unidade.id);

            var usuarioId = _aspNetUser.ObterUsuarioId();
            bolsa.SetColaboradorId(usuarioId);

            var senha = GenerateRandomPassword();
            bolsa.SetSenha(senha.ToUpper());

            await _bolsaRepo.SaveBolsa(bolsa);

            _bolsaRepo.Commit();

            return bolsa.Senha;
        }

        public async Task EditBolsa(BolsaDto editedBolsa)
        {
            var bolsa = _mapper.Map<Bolsa>(editedBolsa);

            await _bolsaRepo.EditBolsa(bolsa);

            _bolsaRepo.Commit();

        }

        public string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 6,
                //RequiredUniqueChars = 4,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonAlphanumeric = false,

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

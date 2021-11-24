using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.UnidadeAuth;
using Invictus.Domain.Administrativo.UnidadeAuth.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IAutorizacaoRepo _autoRepo;
        public UsuarioApplication(IAutorizacaoRepo autoRepo)
        {
            _autoRepo = autoRepo;
        }

        private List<char> chars = new List<char>();

        public async Task CriarAcessoInicial(Guid colaboradorId, string siglaUnidade, Guid unidadeId)
        {
            var autorizacao = new Autorizacao(colaboradorId, siglaUnidade, unidadeId);

            await _autoRepo.SaveAutorizacao(autorizacao);

            _autoRepo.Commit();

        }

        public string GenerateRandomPassword(PasswordOptions opts = null)
        {
            Random rand = new Random(Environment.TickCount);
            //List<char> chars = new List<char>();

            Task.Run(() =>
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

                
            });
            return new string(chars.ToArray());
        }
    }
}

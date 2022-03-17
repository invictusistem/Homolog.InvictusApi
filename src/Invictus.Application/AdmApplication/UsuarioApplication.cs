using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.UnidadeAuth;
using Invictus.Domain.Administrativo.UnidadeAuth.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IAutorizacaoRepo _autoRepo;
        private readonly IAspNetUser _aspNetUser;
        private readonly IColaboradorQueries _colabQueries;
        private readonly UserManager<IdentityUser> _userManager;
        public UsuarioApplication(IAutorizacaoRepo autoRepo, UserManager<IdentityUser> userManager, IColaboradorQueries colabQueries,
            IAspNetUser aspNetUser)
        {
            _autoRepo = autoRepo;
            _userManager = userManager;
            _colabQueries = colabQueries;
            _aspNetUser = aspNetUser;
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

        public async Task EditarAcesso(List<UsuarioAcessoViewModel> acessos)
        {
            var acessosSemDefault = acessos.Where(a => a.podeAlterar != false & a.acesso == true);
            var acessoDefault = acessos.Where(a => a.podeAlterar == false).FirstOrDefault();
            // remover claims com exceção da default
            var colab = await _colabQueries.GetColaboradoresById(acessos[0].id);
            
            var usuario = await _userManager.FindByEmailAsync(colab.email);
            var claims = await _userManager.GetClaimsAsync(usuario);

            var unidadesClaims = claims.Where(c => c.Type == "Unidade");
            // remove default
            var unidadesSemDefault = unidadesClaims.Where(c => c.Value != acessoDefault.sigla);
            if (unidadesClaims.Count() > 1) {
                IdentityResult result = await _userManager.RemoveClaimsAsync(usuario, unidadesSemDefault);

                if (!result.Succeeded) throw new NotImplementedException();
            }
            


            if (acessosSemDefault.Count() > 0)
            {
                foreach (var item in acessosSemDefault)
                {
                    await _userManager.AddClaimAsync(usuario, new Claim("Unidade", item.sigla));
                }
            }
           
        }

        public IEnumerable<string> GetPerfisAutorizados()
        {
            var perfis = new List<string>();
            var role = _aspNetUser.ObterRole();
            /*
            MasterAdm
 Professor
Aluno
Administrador
SuperAdm
            */

            if (role == "SuperAdm") // apenas para Dev = acesso total
            {
                perfis.Add("SuperAdm");
                perfis.Add("MasterAdm");
                perfis.Add("Administrador");
                perfis.Add("Professor");
            }

            if (role == "MasterAdm")
            {
               // perfis.Add("SuperAdm");
                perfis.Add("MasterAdm");
                perfis.Add("Administrador");
                perfis.Add("Professor");
            }            

            if (role == "Administrador")
            {
                //perfis.Add("SuperAdm");
                //perfis.Add("MasterAdm");
                perfis.Add("Administrador");
                perfis.Add("Professor");
            }

            if (role == "Professor")
            {
                //perfis.Add("SuperAdm");
                //perfis.Add("MasterAdm");
                //perfis.Add("Administrador");
                //perfis.Add("Professor");
            }

            

            
            return perfis;
        }
    }
}

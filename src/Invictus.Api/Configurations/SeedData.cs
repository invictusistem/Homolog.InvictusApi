using Invictus.Data.Context;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.Parametros;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Administrativo.UnidadeAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Invictus.Api.Configurations
{
    public static class SeedData
    {

        public static void EnsurePopulated(IApplicationBuilder app, IConfiguration Configuration)
        {
            InvictusDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<InvictusDbContext>();

            var unidade = context.Unidades.Where(u => u.Sigla == "DEV");

            if(unidade.Count() == 0)
            {
                var endereco = new Endereco("Campo Grande", "23093122", "Sala 205", "Estrada da Posse", "3700", "Rio de Janeiro", "RJ");
                var newUnidade = new Unidade("DEV","12345678912345","Invictus Dev", true, endereco);
                context.Unidades.Add(newUnidade);
                context.SaveChanges();
                var colab = context.Colaboradores.Where(c => c.Email == "invictus@master.com");

                

                if(colab.Count() == 0)
                {
                    var parmKey = context.ParametrosKeys.Where(p => p.Key == "Cargo").SingleOrDefault();
                    var paramValue = context.ParametrosValues.Where(p => p.Value == "Desenvolvedor").SingleOrDefault();

                    var tpePacote1 = new TypePacote("APH/ENFERMAGEM", null, "Avançado", true);
                    var tpePacote2 = new TypePacote("CUIDADOR", null, "Intermediário", true);
                    context.TypePacotes.Add(tpePacote1);
                    context.TypePacotes.Add(tpePacote2);

                    context.SaveChanges();


                    if (parmKey == null)
                    {
                        parmKey = new ParametrosKey("Cargo", null, true);
                        context.ParametrosKeys.Add(parmKey);

                        paramValue = new ParametrosValue("Desenvolvedor", null, null, true, parmKey.Id);
                        context.ParametrosValues.Add(paramValue);

                        context.SaveChanges();
                    }

                    var colabEndereco = new ColaboradorEndereco("Campo Grande", "23093122", "Sala 205", "Estrada da Posse", "3700", "Rio de Janeiro", "RJ");
                    var newColaborador = new Colaborador("Desenvolvedor", "invictus@master.com", "12345678912", "21999999999", paramValue.Id, newUnidade.Id, true, colabEndereco);
                    var autorize = new Autorizacao(newColaborador.Id, "DEV", newUnidade.Id);
                    context.Colaboradores.Add(newColaborador);
                    context.SaveChanges();

                    context.Autorizacoes.Add(autorize);
                    context.SaveChanges();
                }
            }
           
        }

    }
}



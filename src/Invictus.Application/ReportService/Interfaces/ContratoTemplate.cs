using Invictus.Data.Context;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.ReportService.Interfaces
{
    public class ContratoTemplate
    {
        private readonly IContratoQueries _contratoQueries;
        
        public ContratoTemplate(IContratoQueries contratoQueries)
        {
            _contratoQueries = contratoQueries;
            
        }
        public static string Generate(GenerateContratoDTO info, ContratoDto contrato)
        {

            var conteudo = "";

            foreach (var item in contrato.conteudos.OrderBy(c => c.order))
            {
                conteudo += item.content;
            }

            conteudo = conteudo.Remove(0, 5);

            var conteudoDois = "";//" @"<div style='text-align: justify; text-justify: inter-word;padding: 30px;' >";
            conteudoDois += conteudo;
            //var employees = DataStorage.GetAllEmployess();

            //var htmlDoc = new StringBuilder();
            // htmlDoc.AppendLine(@"<html><head></head><body>");
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo4a.png");//_webHostEnvironment.WebRootPath + "\\logo4a.png";
            //htmlDoc.AppendLine($"<img src=\"{path}\" />");
            //htmlDoc.AppendLine("</td>");
            var sb = new StringBuilder();
            sb.AppendLine(@"<html><head></head><body>");
            sb.AppendLine(@"<div style='padding: 15px;'>Contrato que celebram entre si Sistema de Ensino Invictus, CNPJ: "+info.cnpj+ @", situada 
            à " + info.logradouro + @", " + info.numero + @" nesta cidade de " + info.cidade + @", Estado do Rio de Janeiro, 
            neste ato, doravante denominada CONTRATADA, e o aluno acima identificado, doravante denominado CONTRATANTE, 
            conforme as cláusulas seguintes.</div>");
            sb.AppendLine(@"<div style='text-align: justify; text-justify: inter-word; padding: 15px;' >");
            sb.AppendLine(conteudoDois);

            //sb.AppendLine(@"</div>");
            sb.AppendLine(@"<br><br><br><br>
            <div style='text-align: center;' > ___________________________________________________<br>
            <div style='font-weight: bold;' >" + info.nome + @"</div>
  
            <div style='font-weight: bold;' >" + info.cpf + @"</div>
            </div>");


            sb.AppendLine(@" </body></html>");


            return sb.ToString();
        }
    }
}
using Invictus.Domain.Administrativo.ContratoAggregate;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.ReportService
{
    public interface IPDFDesigns
    {
        string GetHTMLString();
        string GetContratoHTMLString(List<Conteudo> conteudos);
    }
    public class PDFDesigns : IPDFDesigns
    {
        private IWebHostEnvironment _webHostEnvironment;
        public PDFDesigns(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string GetHTMLString()
        {
            //var employees = DataStorage.GetAllEmployess();

            //var htmlDoc = new StringBuilder();
            //htmlDoc.AppendLine(@"<html><head></head><body>");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo4a.png");//_webHostEnvironment.WebRootPath + "\\logo4a.png";
            //htmlDoc.AppendLine($"<img src=\"{path}\" />");
            //htmlDoc.AppendLine("</td>");
            var sb = new StringBuilder();
            sb.AppendLine(@"<html><head></head><body>");
            sb.AppendLine(@"<div style='position: relative;'>

                            <div class='imagediv'>
                                    <img class='image' src='" + path + @"' height='110'>
                            </div>"
                          );


            sb.AppendLine(@"
                          <div style='text-align: center;font-size: 1.3em;margin-bottom: 15px;color: rgb(6, 6, 177);'>SISTEMA DE ENSINO<br>INVICTUS</div>

                          <div style='text-align: center;font-size: 1.3em;margin-bottom: 15px;color: rgb(6, 6, 177);'>FICHA DE MATRÍCULA<br>Ano 2021</div>");



            sb.AppendLine(@"
    <div class='titulo'>CURSO:</div>

    <div style='padding: 10px; font-size: 1em;color: rgb(6, 6, 177);border: 1px solid blue'>TÉCNICO EM ENFERMAGEM</div>");

            sb.AppendLine(@"
    <div class='titulo'>DATA DE INÍCIO:</div>");

            sb.AppendLine(@"
    <div style='padding: 10px; font-size: 1em;color: rgb(6, 6, 177);border: 1px solid blue'>10/01/2021</div>");

            sb.AppendLine(@"
    <div class='titulo'>DADOS PESSOAIS:</div>

    <div style='padding: 10px;color: rgb(6, 6, 177);border: 1px solid blue'>
       
        <div class='subtitulo'>Nome Completo:
            <span class='clean'>_________________________________________________________________________________</span>
        </div>
       
        <div class='subtitulo' >Data de Nascimento:
            <span class='clean'>________________________</span>
            Local de Nascimento:
            <span class='clean'>__________________________________</span>
        </div>");


            sb.AppendLine(@"
        <div class='subtitulo'>
            Etnia: 
            <span class='clean'>______________________</span>");
            sb.AppendLine(@"
            RG: 
            <span class='clean'>_________________________</span>");
            sb.AppendLine(@"
            Órgão Emissor: 
            <span class='clean'>________________________</span>
        </div>");

            sb.AppendLine(@"
        <div class='subtitulo'>
            Data de Expedição: 
            <span class='clean'>_______________</span>");

            sb.AppendLine(@"
            CPF: 
            <span class='clean'>____________________</span>
        </div>");

            sb.AppendLine(@"
        <div class='subtitulo'>
            Endereço:
            <span class='clean'>_______________________________________________________________________________________</span>");
            sb.AppendLine(@"
            <span class='clean'>________________________________________________________________________________________________</span>
        </div>
        
        <div class='subtitulo'>
            Telefones de Contato: Fixo: 
            <span class='clean'>_____________________</span>,
            Cel: 
            <span class='clean'>_____________________</span>           
        </div>
        
        <div class='subtitulo'>
            E-mail:
            <span class='clean'>__________________________________</span>
        </div>

        
        <div class='subtitulo'>
            Filiação: 
            <span class='clean'>___________________________________________</span>
            e 
            <span class='clean'>___________________________________________</span>
        </div>

    </div>

    

    <div class='titulo'>ENSINO MÉDIO:</div>

    <div style='padding: 10px;color: rgb(6, 6, 177);border: 1px solid blue'>
       
        <div class='subtitulo'>
            Público:
            <span class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp;)</span>
            Privada: 
            <span class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp;)</span>
            Escola: 
            <span class='clean'>___________________________________________________________________</span>
        </div>
       
        <div class='subtitulo'>
            Local: 
            <span class='clean'>____________________________________</span>
            Data de Conclusão do Ensino Médio: 
            <span class='clean'>______________________</span>
        </div>

    </div>


    <div class='titulo'>DOCUMENTOS APRESENTADOS NO ATO DA MATRÍCULA:</div>

    <div style='padding: 10px;color: rgb(6, 6, 177);border: 1px solid blue'>

        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Xerox RG</div>
        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Xerox CPF</div>
        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Xerox Comprovante de residência</div>
        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Xerox Certidão de Nascimento ou Casamento</div>
        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Foto 3x4</div>
        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Xerox Título de Eleitor</div>
        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Xerox do Histórico Escolar do Ensino Médio</div>
        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Certificado Autentivado</div>
        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Cópia do Diário Oficial</div>
        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Certificado de Reservista</div>
        <div class='doc'>(&nbsp;&nbsp;&nbsp;&nbsp; ) Xerox do COREN</div>
        <br>
        <br>

        <div style='text-align: center;'>______________________________________<br>
            <span style='font-weight: bold;' >ASSINATURA DO ALUNO (A)</span></div>

    </div>

    
    <br>

    <div style='padding: 10px;color: rgb(6, 6, 177);border: 1px solid blue'>
       
        <div class='subtitulo'>
            RESPONSÁVEL PELA MATRÍCULA:
            <span class='clean'>__________________________________________</span>

        </div>

    </div>

</div>");




            sb.AppendLine(@"</body></html>");
            return sb.ToString();
        }

        public string GetContratoHTMLString(List<Conteudo> conteudos)
        {
            var conteudo = "";

            foreach (var item in conteudos.OrderBy(c => c.Order))
            {
                conteudo += item.Content;
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
            sb.AppendLine(@"<div style='padding: 15px;'>Contrato que celebram entre si Sistema de Ensino Invictus, CNPJ: 41.586.795/0001-49, situada 
            à Rua Manoel João Gonçalves, Nº 456 nesta cidade de São Gonçalo, Estado do Rio de Janeiro, 
            neste ato, doravante denominada CONTRATADA, e o aluno acima identificado, doravante denominado CONTRATANTE, 
            conforme as cláusulas seguintes.</div>");
            sb.AppendLine(@"<div style='text-align: justify; text-justify: inter-word; padding: 15px;' >");
            sb.AppendLine(conteudoDois);

            //sb.AppendLine(@"</div>");
            sb.AppendLine(@"<br><br><br><br>
            <div style='text-align: center;' > ___________________________________________________<br>
            <div style='font-weight: bold;' >ALUNO</div>
  
            <div style='font-weight: bold;' >CPF</div>
            </div>");


            sb.AppendLine(@" </body></html>");


            return sb.ToString();
        }
    }
}

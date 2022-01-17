using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.ReportService.Interfaces
{
    public class FichaMatriculaTemplate
    {

        public static string Generate(GenerateFichaMatriculaDTO info)
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo4a.png");//_webHostEnvironment.WebRootPath + "\\logo4a.png";
            //htmlDoc.AppendLine($"<img src=\"{path}\" />");
            //htmlDoc.AppendLine("</td>");

            var enderecoCompleto = "";
            var sb = new StringBuilder();
            
            sb.AppendLine(@"<html><head></head><body>");
            sb.AppendLine(@"<div style='position: relative;'>

                            <div class='imagediv'>
                                    <img class='image' src='" + path + @"' height='110'>
                            </div>"
                          );


            sb.AppendLine(@"
                          <div style='text-align: center;font-size: 1.3em;margin-bottom: 15px;color: rgb(6, 6, 177);'>SISTEMA DE ENSINO<br>INVICTUS</div>

                          <div style='text-align: center;font-size: 1.3em;margin-bottom: 15px;color: rgb(6, 6, 177);'>FICHA DE MATRÍCULA<br>Ano "+DateTime.Now.Year+@"</div>");



            sb.AppendLine(@"
    <div class='titulo'>CURSO:</div>

    <div style='padding: 10px; font-size: 1em;color: rgb(6, 6, 177);border: 1px solid blue'>"+info.nomeCurso+@"</div>");

            sb.AppendLine(@"
    <div class='titulo'>DATA DE INÍCIO:</div>");

            sb.AppendLine(@"
    <div style='padding: 10px; font-size: 1em;color: rgb(6, 6, 177);border: 1px solid blue'>" + info.dataInicio.ToString("dd/MM/yyyy") + @"</div>");

            sb.AppendLine(@"
    <div class='titulo'>DADOS PESSOAIS:</div>

    <div style='padding: 10px;color: rgb(6, 6, 177);border: 1px solid blue'>
       
        <div class='subtitulo'>Nome Completo:
            <span class='clean'>" + info.nomeAluno + @"</span>
        </div>
       
        <div class='subtitulo' >Data de Nascimento:
            <span class='clean'>" + info.nascimento.ToString("dd/MM/yyyy") + @"</span>
            Local de Nascimento:
            <span class='clean'>" + info.naturalidade + @"</span>
        </div>");


            sb.AppendLine(@"
        <div class='subtitulo'>
            Etnia: 
            <span class='clean'>______________________</span>");
            sb.AppendLine(@"
            RG: 
            <span class='clean'>" + info.rg + @"</span>");
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
            <span class='clean'>" + info.cpf + @"</span>
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
            <span class='clean'>" + info.pai + @"</span>
            e 
            <span class='clean'>" + info.mae + @"</span>
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
            <span class='clean'>" + info.nomeResponsavelMatricula + @"</span>

        </div>

    </div>

</div>");




            sb.AppendLine(@"</body></html>");


            return sb.ToString();
        }
    }
}

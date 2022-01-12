using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Api.Helpers
{
    public interface ITemplate
    {
        string GetHTMLString();
    }
    public class TemplateGenerator : ITemplate
    {
        private IWebHostEnvironment _webHostEnvironment;
        public TemplateGenerator(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string GetHTMLString()
        {
            //var employees = DataStorage.GetAllEmployess();
            
            var htmlDoc = new StringBuilder();
            htmlDoc.AppendLine(@"<html><head></head><body>");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo4a.png");//_webHostEnvironment.WebRootPath + "\\logo4a.png";
            //htmlDoc.AppendLine($"<img src=\"{path}\" />");
            //htmlDoc.AppendLine("</td>");
            var sb = new StringBuilder();
            sb.AppendLine(@"<div style='position: relative;'>

                            <div class='imagediv'>
                                    <img class='image' src='" + path + @"' height='110'>
                            </div>"
                          );


            sb.AppendLine(@"
                          <div style='text-align: center;font-size: 1.3em;margin-bottom: 15px;color: rgb(6, 6, 177);'>SISTEMA DE ENSINO<br>INVICTUS</div>

                          <div style='text-align: center;font-size: 1.3em;margin-bottom: 15px;color: rgb(6, 6, 177);'>FICHA DE MATRÍCULA<br>Ano 2021</div>




    <div class='titulo'>CURSO:</div>

    <div style='padding: 10px; font-size: 1em;color: rgb(6, 6, 177);border: 1px solid blue'>TÉCNICO EM ENFERMAGEM</div>

    <div class='titulo'>DATA DE INÍCIO:</div>

    <div style='padding: 10px; font-size: 1em;color: rgb(6, 6, 177);border: 1px solid blue'>10/01/2021</div>
    
    <div class='titulo'>DADOS PESSOAIS:</div>

    <div style='padding: 10px;color: rgb(6, 6, 177);border: 1px solid blue'>
       
        <div class='subtitulo'>Nome Completo:
            <span class='clean'>___________________________________________________________________</span>
        </div>
       
        <div class='subtitulo' >Data de Nascimento:
            <span class='clean'>_____________</span>
            Local de Nascimento:
            <span class='clean'>_____________________</span>
        </div>

       
        <div class='subtitulo'>
            Etnia: 
            <span class='clean'>________</span>
            RG: 
            <span class='clean'>________________</span>
            Órgão Emissor: 
            <span class='clean'>________________</span>
        </div>
        
        <div class='subtitulo'>
            Data de Expedição: 
            <span class='clean'>______________</span>
            CPF: 
            <span class='clean'>____________________</span>
        </div>

        
        <div class='subtitulo'>
            Endereço:
            <span class='clean'>________________</span>
        </div>
        
        <div class='subtitulo'>
            Telefones de Contato: Fixo: 
            <span class='clean'>____________________</span>,
            Cel: 
            <span class='clean'>_____________________</span>           
        </div>
        
        <div class='subtitulo'>
            E-mail:
            <span class='clean'>__________________________________</span>
        </div>

        
        <div class='subtitulo'>
            Filiação: 
            <span class='clean'>__________________________________________</span>
            e 
            <span class='clean'>__________________________________________</span>
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
            <span class='clean'>______________________________________________________</span>
        </div>
       
        <div class='subtitulo'>
            Local: 
            <span class='clean'>_______________________________________</span>
            Data de Conclusão do Ensino Médio: 
            <span class='clean'>_______________________</span>
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

</div>





                                         </body>
                        </html>");
            return sb.ToString();
        }
    }
}


/*
 
 
select 
TypePacote.Nome as Curso,
Turmas.PrevisaoAtual as Inicio,
alunos.nome,
alunos.Nascimento,
alunos.Naturalidade,
alunos.RG,
alunos.CPF,
alunos.Logradouro,
alunos.Complemento,
alunos.Bairro,
alunos.CEP,
alunos.Cidade,
alunos.UF,
alunos.TelResidencial,
alunos.TelWhatsapp,
alunos.Email,
alunos.NomePai,
alunos.NomeMae,
AlunosDocumentos.Descricao,
AlunosDocumentos.Validado
from alunos
inner join Matriculas on Alunos.Id = Matriculas.AlunoId
inner join AlunosDocumentos on AlunosDocumentos.MatriculaId = Matriculas.Id
inner join Turmas on Matriculas.TurmaId = Turmas.Id
inner join TypePacote on turmas.TypePacoteId = TypePacote.Id
 
*/

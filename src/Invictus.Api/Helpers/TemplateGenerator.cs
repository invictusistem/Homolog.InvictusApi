using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
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
            
            //var htmlDoc = new StringBuilder();
            //htmlDoc.AppendLine("<td>");
            //var path = _webHostEnvironment.WebRootPath + "\\1.png";//It fetches files under wwwroot
            //htmlDoc.AppendLine($"<img src=\"{path}\" />");
            //htmlDoc.AppendLine("</td>");
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                
<div class='divOne' >

<div class='divTwo'>
         <span>SISTEMA DE ENSINO INVICTUS</span><br>
              FICHA DE MATRÍCULA
            </div>
            

              <div style='margin-top: 50px;border: 1px solid red;width:100px;height: 50px;'></div>
             

               <div style='position: absolute;top:0;position: absolute;border: 1px solid red; width: 100px; height: 50px;'>
            <img class='imageOne' alt='' width='32' height='32' class='rounded-circle me-2'></div>


</div>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}

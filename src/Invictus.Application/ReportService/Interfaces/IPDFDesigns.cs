using Invictus.Domain.Administrativo.ContratoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.ReportService.Interfaces
{
    public interface IPDFDesigns
    {
        string GetHTMLString();
        string GetContratoHTMLString(List<Conteudo> conteudos);
        string GetPendenciaDocs(string nomeCompleto);
    }
}

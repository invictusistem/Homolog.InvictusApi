using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IDocTemplateApplication
    {
        Task AddDoc(DocumentacaoTemplateDto doc);
        Task EditDoc(DocumentacaoTemplateDto doc);
        Task RemoveDoc(Guid documentoId);

    }
}

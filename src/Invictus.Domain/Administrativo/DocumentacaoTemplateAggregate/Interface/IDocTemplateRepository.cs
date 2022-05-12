using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate.Interface
{
    public interface IDocTemplateRepository : IDisposable
    {
        Task SaveDoc(DocumentacaoTemplate doc);
        Task EditDoc(DocumentacaoTemplate doc);
        Task Delete(Guid documentoId);
        void Commit();

    }
}

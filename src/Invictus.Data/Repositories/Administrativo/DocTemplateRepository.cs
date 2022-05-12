using Invictus.Data.Context;
using Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate;
using Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class DocTemplateRepository : IDocTemplateRepository
    {
        private readonly InvictusDbContext _db;
        public DocTemplateRepository(InvictusDbContext db)
        {
            _db = db;
        }
        public void Commit()
        {
            _db.SaveChanges();
        }

        public async Task Delete(Guid documentoId)
        {
            var doc = await _db.DocumentacoesTemplate.FindAsync(documentoId);
            await _db.DocumentacoesTemplate.SingleDeleteAsync(doc);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task EditDoc(DocumentacaoTemplate doc)
        {
            await _db.DocumentacoesTemplate.SingleUpdateAsync(doc);
        }

        public async Task SaveDoc(DocumentacaoTemplate doc)
        {
            await _db.DocumentacoesTemplate.AddAsync(doc);
        }
    }
}

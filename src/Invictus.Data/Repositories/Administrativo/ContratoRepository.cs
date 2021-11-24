using Invictus.Data.Context;
using Invictus.Domain.Administrativo.ContratoAggregate;
using Invictus.Domain.Administrativo.ContratoAggregate.Interfaces;
using Invictus.Domain.Administrativo.ContratosAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class ContratoRepository : IContratoRepository
    {
        private readonly InvictusDbContext _db;
        public ContratoRepository(InvictusDbContext db)
        {
            _db = db;
        }        

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveContrato(Contrato newContrato)
        {
            await _db.Contratos.AddAsync(newContrato);
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public async Task UpdateContrato(Contrato newContrato)
        {   

            await _db.Contratos.SingleUpdateAsync(newContrato);
        }

        public void RemoveConteudos(List<Conteudo> conteudos)
        {
            _db.Conteudos.RemoveRange(conteudos);
        }

        public async Task SaveConteudo(List<Conteudo> conteudos)
        {
            await _db.Conteudos.AddRangeAsync(conteudos);
        }
    }
}

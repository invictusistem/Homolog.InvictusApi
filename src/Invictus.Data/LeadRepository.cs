using Invictus.Data.Context;
using Invictus.Domain.Comercial.Leads;
using Invictus.Domain.Repository;
using System.Collections.Generic;

namespace Invictus.Data
{
    public class LeadRepository : ILeadRepository
    {
        private readonly InvictusDbContext _db;
        public LeadRepository(InvictusDbContext db)
        {
            _db = db;
        }
        public void AddLead(IEnumerable<Lead> lead)
        {

            _db.Leads.AddRange(lead);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}

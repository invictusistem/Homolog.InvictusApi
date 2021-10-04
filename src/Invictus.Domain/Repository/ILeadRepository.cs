using Invictus.Domain.Comercial.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Repository
{
    public interface ILeadRepository : IDisposable
    {
        void AddLead(IEnumerable<Lead> lead);
        
    }
}

using Invictus.Domain.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.PlanoPagamento
{
    public interface IPlanoPgmRepository : IDisposable
    {
        Task CreatePlano(PlanoPagamentoTemplate plano);
        Task EditPlano(PlanoPagamentoTemplate plano);
        void Commit();
        
    }
}

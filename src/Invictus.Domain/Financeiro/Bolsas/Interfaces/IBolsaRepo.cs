using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Bolsas.Interfaces
{
    public interface IBolsaRepo : IDisposable
    {
        Task SaveBolsa(Bolsa bolsa);
        void Commit();
    }
}

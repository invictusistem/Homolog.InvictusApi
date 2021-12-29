using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Interfaces
{
    public interface IDebitosRepos : IDisposable
    {
        Task SaveInfoFinanceira(InformacaoDebito infoDebito);
        void Commit();
    }
}

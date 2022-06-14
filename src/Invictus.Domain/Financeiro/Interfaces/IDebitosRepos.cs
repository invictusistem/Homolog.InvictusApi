using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Interfaces
{
    public interface IDebitosRepos : IDisposable
    {
        //Task SaveInfoFinanceira(InformacaoDebito infoDebito);
        Task SaveBoleto(Boleto boleto);
        Task SaveBoletos(IEnumerable<Boleto> boleto);
        Task EditBoleto(Boleto conta);
        void Commit();
    }
}

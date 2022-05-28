using Invictus.Dtos.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.FinancApplication.Interfaces
{
    public interface IFinanceiroApp
    {
        Task CadastrarContaReceber(CadastrarContaReceberCommand command);
        Task EditarContaReceber(BoletoDto boleto);
        Task CadastrarContaPagar(CadastrarContaReceberCommand command);
    }
}

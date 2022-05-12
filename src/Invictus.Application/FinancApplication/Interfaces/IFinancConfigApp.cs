using Invictus.Dtos.Financeiro.Configuracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.FinancApplication.Interfaces
{
    public interface IFinancConfigApp
    {
        Task SaveBanco(BancoDto banco);
        Task SaveCentroDeCusto(CentroCustoDto centroCusto);
        Task SaveMeioDePagamento(MeioPagamentoDto meioPgm);
        Task SavePlanoDeConta(PlanoContaDto plano);
        Task SaveSubConta(SubContaDto subConta);
    }
}

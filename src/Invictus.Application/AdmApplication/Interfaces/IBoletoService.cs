using Invictus.Dtos.Financeiro;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IBoletoService
    {
        List<BoletoLoteResponse> GerarBoletosEmLote(List<Parcela> boletosLote, DadosPessoaDto pessoa);
        Task<List<BoletoLoteResponse>> GerarBoletosUnicos(List<Parcela> boletosLote, decimal valorBonusPontualidade, DadosPessoaDto pessoa, int qndBoletosSalvos);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Extensions.Mappings
{
    public static class DTOPainelCDEExtensions
    {
        //public static IEnumerable<PainelCDEEventosDTO> ToDTOPainelCDE(this IEnumerable<WorkOrder> workOrders)
        //{
        //    foreach (var item in workOrders)
        //    {
        //        yield return item.ToEventosCDEDTO();
        //    }
        //}

        //private static PainelCDEEventosDTO ToEventosCDEDTO(this WorkOrder workOrders)
        //{
        //    return new PainelCDEEventosDTO()
        //    {
        //        codigoWorkOrder = workOrders.Id,
        //        ordem = workOrders.Ordem,
        //        recurso = workOrders?.Recurso?.Descricao,
        //        dataInicio = workOrders.HorarioInicio,
        //        datafim = workOrders.HorarioFim,
        //        produto = workOrders?.Produto,
        //        reserva = workOrders?.Reserva,
        //        descricao = workOrders?.ProdutoDescricao,
        //        equipe = workOrders?.Equipe,
        //        equipamentos = workOrders?.Equipamentos + " " + workOrders?.EquipamentosExtras,
        //        //equipamentosExtras = workOrder?.EquipamentosExtras,
        //        informacoes = workOrders?.Informacoes,
        //        instrucoes = workOrders?.Intrucoes
        //    };
        //}
    }
}

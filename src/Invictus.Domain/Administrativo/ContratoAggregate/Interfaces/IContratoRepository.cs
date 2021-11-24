using Invictus.Domain.Administrativo.ContratosAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.ContratoAggregate.Interfaces
{
    public interface IContratoRepository : IDisposable
    {
        // Task AddContrato(Contrato newContrato);
        Task SaveContrato(Contrato newContrato);
        Task UpdateContrato(Contrato newContrato);
        Task SaveConteudo(List<Conteudo> conteudos);
        void RemoveConteudos(List<Conteudo> conteudos);
        void Commit();
    }
}

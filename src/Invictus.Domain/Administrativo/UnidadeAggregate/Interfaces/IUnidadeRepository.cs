using Invictus.Domain.Administrativo.ContratosAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.UnidadeAggregate.Interfaces
{
    public interface IUnidadeRepository : IDisposable
    {
        Task AddUnidade(Unidade unidade);
        //Task UpdateUnidade(Unidade editedUnidade);
        Task EditUnidade(Unidade editedUnidade);
        Task SaveSala(Sala newSala);
        Task EditSala(Sala newSala);
        //Task SaveContrato(Contrato newContrato);
        void Save();
    }
}

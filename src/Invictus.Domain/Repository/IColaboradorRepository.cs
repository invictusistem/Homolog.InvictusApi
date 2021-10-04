using Invictus.Domain.Administrativo.ColaboradorAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Repository
{
    public interface IColaboradorRepository : IDisposable
    {
        void AddColaborador(Colaborador newColaborador);
        void DeleteColaborador(int colaboradorId);
        void UpdateColaborador(Colaborador colaborador);
        void EditColaborador(int colaboradorId, string perfil, bool perfilAtivo);
    }
}

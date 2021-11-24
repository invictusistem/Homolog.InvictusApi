using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.UnidadeAuth.Interfaces
{
    public interface IAutorizacaoRepo : IDisposable
    {
        Task SaveAutorizacao(Autorizacao aut);
        void Commit();
    }
}

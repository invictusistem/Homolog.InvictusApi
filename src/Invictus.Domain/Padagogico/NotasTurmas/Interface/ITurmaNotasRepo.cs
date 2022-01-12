using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Padagogico.NotasTurmas.Interface
{
    public interface ITurmaNotasRepo : IDisposable
    {
        Task SaveList(IEnumerable<TurmaNotas> notas);
        void UpdateNotas(IEnumerable<TurmaNotas> notas);
        void Commit();
    }
}

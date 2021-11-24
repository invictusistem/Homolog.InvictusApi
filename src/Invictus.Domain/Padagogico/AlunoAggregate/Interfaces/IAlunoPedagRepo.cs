using Invictus.Domain.Pedagogico.AlunoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Padagogico.AlunoAggregate.Interfaces
{
    public interface IAlunoPedagRepo : IDisposable
    {
       Task SaveAnotacao(AlunoAnotacao anotacao);
        void Commit();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.FuncionarioAggregate.Interfaces
{
    public interface IPessoaRepo : IDisposable
    {
        Task AddPessoa(Pessoa pessoa);
        Task EditPessoa(Pessoa pessoa);
        void Commit();
    }
}

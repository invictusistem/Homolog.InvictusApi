using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.AlunoAggregate.Interface
{
    public interface IAlunoRepo : IDisposable
    {
        //Task SaveAluno(Aluno newAluno);
        //Task Edit(Aluno newAluno);
        Task SaveAlunoPlano(AlunoPlanoPagamento newPlano);
        Task SaveAlunoDocs(IEnumerable<AlunoDocumento> docs);
        Task SaveAlunoDoc(AlunoDocumento docs);
        Task EditAlunoDoc(AlunoDocumento doc);
        void Commit();
    }
}

using Invictus.Domain.Pedagogico.EstagioAggregate;
using System;

namespace Invictus.Domain.Pedagogico.Models.IPedagModelRepository
{
    public interface IPedagModelsRepository : IDisposable
    {
        void CreateEstagio(Estagio estagio);
        void AddFilesEstagio(Documento documento, int alunoId);
    }
}

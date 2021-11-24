

using Invictus.Data.Mapping.PedagMappings.AlunoMapping;
using Invictus.Data.Mapping.PedagMappings.NotasTurmasMapping;
using Microsoft.EntityFrameworkCore;

namespace Invictus.Data.Mapping.PedagMappings
{
    public class PedagModelBuilder
    {
        public static void RegisterBuilders(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TurmaNotasDbMapping());
            modelBuilder.ApplyConfiguration(new AlunoAnotacoesDbMapping());


        }
    }
}

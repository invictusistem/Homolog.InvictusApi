

using Invictus.Data.Mapping.PedagMappings.AlunoMapping;
using Invictus.Data.Mapping.PedagMappings.EstagioMappping;
using Invictus.Data.Mapping.PedagMappings.NotasTurmasMapping;
using Microsoft.EntityFrameworkCore;

namespace Invictus.Data.Mapping
{
    public class PedagModelBuilder
    {
        public static void RegisterBuilders(ModelBuilder modelBuilder)
        {   
            modelBuilder.ApplyConfiguration(new AlunoAnotacoesDbMapping());
            modelBuilder.ApplyConfiguration(new DocumentoEstagioDbMapping());
            modelBuilder.ApplyConfiguration(new EstagioDbMapping());
            modelBuilder.ApplyConfiguration(new MatriculaEstagioDbMapping());
            modelBuilder.ApplyConfiguration(new TypeEstagioDbMapping());
            modelBuilder.ApplyConfiguration(new TurmaNotasDbMapping());
        }
    }
}

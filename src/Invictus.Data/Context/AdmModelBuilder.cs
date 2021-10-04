using Invictus.Data.Mapping.AdministrativoMappings.UnidadeMapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Context
{
    public class AdmModelBuilder
    {
        public static void RegisterBuilders(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UnidadeDbMapping());
        }
    }
}

﻿using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.ModelsMapping
{
    public class TypePacoteDbMapping : IEntityTypeConfiguration<TypePacote>
    {
        public void Configure(EntityTypeBuilder<TypePacote> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("TypePacote");
        }
    }
}

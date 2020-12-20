using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dashboard.Data.Configurations
{
    public class ImportConfiguration : IEntityTypeConfiguration<Import>
    {
        public void Configure(EntityTypeBuilder<Import> builder)
        {
            builder.ToTable("Import");
            builder.HasKey(x => x.ID);

        }
    }
}

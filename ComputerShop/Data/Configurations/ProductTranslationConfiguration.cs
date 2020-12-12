using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dashboard.Data.Entities;

namespace Dashboard.Data.Configurations
{
    public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Details).HasMaxLength(500);

            builder.HasOne(x => x.Product).WithMany(x => x.ProductTranslations).HasForeignKey(x => x.ProductId);

        }
    }
}

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

            builder.HasOne(x => x.Product).WithMany(pt => pt.ProductTranslations)
                .HasForeignKey(t => t.ProductId);

            builder.HasOne(x => x.Transaction).WithMany(pt => pt.ProductTranslations)
                .HasForeignKey(pc => pc.TransactionID);

        }
    }
}

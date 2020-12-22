using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dashboard.Data.Configurations
{
    public class ProductInImportConfiguration : IEntityTypeConfiguration<ProductInImport>
    {
        public void Configure(EntityTypeBuilder<ProductInImport> builder)
        {
            builder.ToTable("ProductInImport");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.HasOne(x => x.Import).WithMany(x => x.ProductInImports)
                .HasForeignKey(x => x.ImportID);
            builder.HasOne(x => x.Product).WithOne(x => x.ProductInImports)
                .HasForeignKey<ProductInImport>(x => x.ProductID);
        }
    }
}

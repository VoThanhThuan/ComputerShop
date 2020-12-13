using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dashboard.Data.Configurations
{
    public class ProductGuaranteeConfiguration : IEntityTypeConfiguration<ProductGuarantee>
    {
        public void Configure(EntityTypeBuilder<ProductGuarantee> builder)
        {
            builder.ToTable("ProductGuarantee");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.HasOne(x => x.Product).WithOne(x => x.ProductGuarantee).HasForeignKey<ProductGuarantee>(x => x.ProductId);

        }
    }
}

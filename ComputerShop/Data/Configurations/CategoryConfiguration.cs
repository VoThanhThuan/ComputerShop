using Microsoft.EntityFrameworkCore;
using Dashboard.Data.Entities;
using Dashboard.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dashboard.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            

            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
        }
    }
}
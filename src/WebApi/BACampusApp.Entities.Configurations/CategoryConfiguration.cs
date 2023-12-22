using BACampusApp.Core.Entities.Interfaces;

namespace BACampusApp.Entities.Configurations
{
    public class CategoryConfiguration : AuditableEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasOne(d => d.ParentCategory)

                    .WithMany(p => p.SubCategories)

                    .HasForeignKey(d => d.ParentCategoryId);

                    //.HasConstraintName("FK_Employees_Employees");

            builder.Property(x => x.Name);
            builder.HasOne(x=>x.TechnicalUnit).WithMany(x=>x.Categories).HasForeignKey(x=>x.TechnicalUnitId);
        }
    }
}

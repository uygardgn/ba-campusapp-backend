namespace BACampusApp.Entities.Configurations
{
    public class EducationConfiguration : AuditableEntityTypeConfiguration<Education>
    {
        public override void Configure(EntityTypeBuilder<Education> builder)
        {
			builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
			builder.Property(x => x.CourseHours).IsRequired();
			builder.HasOne(x => x.Category).WithMany(x => x.Educations).HasForeignKey(x => x.SubCategoryId);
			base.Configure(builder);
		}
    }
}

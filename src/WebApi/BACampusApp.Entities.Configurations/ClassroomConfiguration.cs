namespace BACampusApp.Entities.Configurations
{
    public class ClassroomConfiguration : AuditableEntityTypeConfiguration<Classroom>
	{
		public override void Configure(EntityTypeBuilder<Classroom> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Name).HasMaxLength(128).IsRequired();
			builder.Property(e => e.OpenDate).IsRequired();
			builder.HasOne(x => x.Education).WithMany(x => x.Classrooms).HasForeignKey(x => x.EducationId);
        }
    }
}

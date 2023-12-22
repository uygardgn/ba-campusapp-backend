namespace BACampusApp.Entities.Configurations
{
    public class StudentHomeworkConfiguration : AuditableEntityTypeConfiguration<StudentHomework>
	{
		public override void Configure(EntityTypeBuilder<StudentHomework> builder)
		{
			builder.HasOne(x => x.HomeWork).WithMany(x => x.StudentHomeworks).HasForeignKey(x => x.HomeWorkId);
			builder.HasOne(x => x.Student).WithMany(x => x.StudentHomeworks).HasForeignKey(x => x.StudentId);
			base.Configure(builder);
		}
	}
}

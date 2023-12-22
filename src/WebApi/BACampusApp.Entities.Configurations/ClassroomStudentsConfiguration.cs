namespace BACampusApp.Entities.Configurations
{
    public class ClassroomStudentsConfiguration : AuditableEntityTypeConfiguration<ClassroomStudent>
    {
        public void Configure(EntityTypeBuilder<ClassroomStudent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartDate);
            builder.Property(x => x.EndDate);

            builder.HasOne(x=>x.Student).WithMany(x=>x.ClassroomStudents).HasForeignKey(x=>x.StudentId);
            builder.HasOne(x=>x.Classroom).WithMany(x=>x.ClassroomStudents).HasForeignKey(x=>x.ClassroomId);


        }
    }
}

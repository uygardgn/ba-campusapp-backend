namespace BACampusApp.Entities.Configurations
{
    public class ClassroomTrainersConfiguration : AuditableEntityTypeConfiguration<ClassroomTrainer>
    {
        public void Configure(EntityTypeBuilder<ClassroomTrainer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartDate);
            builder.Property(x => x.EndDate);

            builder.HasOne(x=>x.Trainer).WithMany(x=>x.ClassroomTrainers).HasForeignKey(x=>x.TrainerId);
            builder.HasOne(x=>x.Classroom).WithMany(x=>x.ClassroomTrainers).HasForeignKey(x=>x.ClassroomId);
        }
    }
}

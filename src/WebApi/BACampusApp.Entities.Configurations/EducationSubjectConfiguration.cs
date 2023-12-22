namespace BACampusApp.Entities.Configurations
{
    public class EducationSubjectConfiguration : BaseEntityTypeConfiguration<EducationSubject>
    {
        public override void Configure(EntityTypeBuilder<EducationSubject> builder)
        {
            builder.HasOne(x => x.Education).WithMany(x => x.EducationSubjects).HasForeignKey(x => x.EducationId);
            builder.HasOne(x => x.Subject).WithMany(x => x.EducationSubjects).HasForeignKey(x => x.SubjectId);
            base.Configure(builder);
        }
    }
}

namespace BACampusApp.Entities.Configurations
{
    public class SupplementaryResourceConfiguration : AuditableEntityTypeConfiguration<SupplementaryResource>
    {
        public override void Configure(EntityTypeBuilder<SupplementaryResource> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            //builder.Property(x => x.EducationSubjectId).IsRequired(false);
            builder.Property(x => x.FileURL).IsRequired();
            builder.Property(x => x.ResourceType).IsRequired();
            //builder.HasOne(x => x.EducationSubject).WithMany(x => x.Id).HasForeignKey(x => x.);

            base.Configure(builder);
        }
    }
}
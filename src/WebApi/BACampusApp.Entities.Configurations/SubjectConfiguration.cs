namespace BACampusApp.Entities.Configurations
{
    public class SubjectConfiguration : AuditableEntityTypeConfiguration<Subject>
    {
        public override void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            base.Configure(builder);
        }
    }
}

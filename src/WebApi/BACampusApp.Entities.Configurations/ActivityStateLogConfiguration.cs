namespace BACampusApp.Entities.Configurations
{
    public class ActivityStateLogConfiguration : AuditableEntityTypeConfiguration<ActivityStateLog>
    {
        public void Configure(EntityTypeBuilder<ActivityStateLog> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ItemId).IsRequired();
            builder.Property(e => e.Description).IsRequired().HasMaxLength(256);
        }
    }
}

namespace BACampusApp.Entities.Configurations
{
    public class TechnicalUnitConfiguration : AuditableEntityTypeConfiguration<TechnicalUnits>
    {
        public void Configure(EntityTypeBuilder<TechnicalUnits> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(128);

        }
    }
}

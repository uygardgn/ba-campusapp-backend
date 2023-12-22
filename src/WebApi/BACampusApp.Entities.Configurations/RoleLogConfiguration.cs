namespace BACampusApp.Entities.Configurations
{
    public class RoleLogConfiguration : BaseEntityTypeConfiguration<RoleLog>
    {
        public void Configure(EntityTypeBuilder<RoleLog> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ActiveRole).IsRequired();
            builder.Property(e => e.ActionType).IsRequired();
        }
    }
}

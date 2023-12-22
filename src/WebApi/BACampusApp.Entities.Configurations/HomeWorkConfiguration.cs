namespace BACampusApp.Entities.Configurations
{
    public class HomeWorkConfiguration:AuditableEntityTypeConfiguration<HomeWork>
    {
        public override void Configure(EntityTypeBuilder<HomeWork> builder)
        {
			builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
			builder.Property(x => x.SubjectId).IsRequired();
			base.Configure(builder);
		}
    }
}

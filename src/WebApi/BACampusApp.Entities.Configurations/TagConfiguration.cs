namespace BACampusApp.Entities.Configurations
{
    public class TagConfiguration : BaseEntityTypeConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

            base.Configure(builder);
        }
    }
}
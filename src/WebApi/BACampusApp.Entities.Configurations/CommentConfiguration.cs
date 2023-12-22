namespace BACampusApp.Entities.Configurations
{
    public class CommentConfiguration :AuditableEntityTypeConfiguration<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
			builder.Property(x => x.Content).IsRequired();
			builder.Property(x => x.Title).HasMaxLength(256);
            builder.Property(x => x.ItemType).IsRequired();
			base.Configure(builder);
		}
    }
}

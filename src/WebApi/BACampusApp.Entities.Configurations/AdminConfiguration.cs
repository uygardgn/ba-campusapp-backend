namespace BACampusApp.Entities.Configurations
{
    public class AdminConfiguration : BaseUserEntityTypeConfiguration<Admin>
	{
		public void Configure(EntityTypeBuilder<Admin> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.FirstName).IsRequired().HasMaxLength(256);
			builder.Property(x => x.LastName).IsRequired().HasMaxLength(256);
			builder.Property(x => x.FullName).HasMaxLength(256);
			builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
			builder.Property(x => x.Gender).IsRequired();
			builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(22);
			builder.Property(x => x.DateOfBirth).IsRequired();
			builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.Address).IsRequired();
        }
	}
}

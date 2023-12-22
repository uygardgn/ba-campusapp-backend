namespace BACampusApp.Entities.Configurations
{
    public class UserPasswordsConfiguration : BaseEntityTypeConfiguration<UserPasswords>
    {
        public void Configure(EntityTypeBuilder<UserPasswords> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserId).IsRequired();
        }
    }
}

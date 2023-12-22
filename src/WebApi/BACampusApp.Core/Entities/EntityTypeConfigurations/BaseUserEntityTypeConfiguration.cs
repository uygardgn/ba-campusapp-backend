using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BACampusApp.Core.Entities.EntityTypeConfigurations;

public class BaseUserEntityTypeConfiguration<TEntity> : AuditableEntityTypeConfiguration<TEntity> where TEntity : BaseUser
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.FirstName)
               .HasMaxLength(maxLength: 256)
               .IsRequired();

        builder.Property(x => x.LastName)
               .HasMaxLength(maxLength: 256)
               .IsRequired();

        builder.Property(x => x.Email)
               .HasMaxLength(maxLength: 256)
               .IsRequired();

        builder.Property(x => x.Gender)
               .IsRequired();

        builder.Property(x => x.PhoneNumber)
               .HasMaxLength(maxLength: 15)
               .IsRequired(false);

        builder.Property(x => x.Image)
               .IsRequired(false);

        builder.Property(x => x.Address)
               .HasMaxLength(maxLength: 256)
               .IsRequired(false);
    }
}

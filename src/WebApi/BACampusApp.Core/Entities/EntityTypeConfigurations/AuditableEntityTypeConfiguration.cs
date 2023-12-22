using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BACampusApp.Core.Entities.EntityTypeConfigurations;

public class AuditableEntityTypeConfiguration<TEntity> : BaseEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.DeletedBy).HasMaxLength(128).IsRequired(false);
        builder.Property(x => x.DeletedDate).IsRequired(false);
    }
}

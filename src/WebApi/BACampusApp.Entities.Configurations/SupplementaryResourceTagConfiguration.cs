namespace BACampusApp.Entities.Configurations
{
    public class SupplementaryResourceTagConfiguration : BaseEntityTypeConfiguration<SupplementaryResourceTag>
    {
        public override void Configure(EntityTypeBuilder<SupplementaryResourceTag> builder)
        {
            #region Many To Many Relationship

            builder.HasOne(x => x.SupplementaryResources).WithMany(x => x.SupplementaryResourceTags).HasForeignKey(x => x.SupplementaryResourceId);
            builder.HasOne(x => x.Tag).WithMany(x => x.SupplementaryResourceTags).HasForeignKey(x => x.TagId);
            #endregion


            base.Configure(builder);
        }
    }
}
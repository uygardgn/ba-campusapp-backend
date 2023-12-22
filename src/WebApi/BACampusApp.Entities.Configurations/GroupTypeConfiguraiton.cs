using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class GroupTypeConfiguraiton : AuditableEntityTypeConfiguration<GroupType>
    {
        public override void Configure(EntityTypeBuilder<GroupType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(128).IsRequired();
        }
    }
}

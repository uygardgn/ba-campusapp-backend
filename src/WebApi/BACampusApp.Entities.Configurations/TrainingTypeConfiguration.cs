using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class TrainingTypeConfiguration : AuditableEntityTypeConfiguration<TrainingType>
    {
        public override void Configure(EntityTypeBuilder<TrainingType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(128).IsRequired();
        }
    }
}

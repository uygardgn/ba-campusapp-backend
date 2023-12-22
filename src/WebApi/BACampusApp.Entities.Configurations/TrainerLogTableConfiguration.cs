using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class TrainerLogTableConfiguration : BaseEntityTypeConfiguration<TrainerLogTable>
    {
        public void Configure(EntityTypeBuilder<TrainerLogTable> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description);
            builder.Property(x=>x.TrainerActionType).IsRequired();

            base.Configure(builder);
        }
    }
}

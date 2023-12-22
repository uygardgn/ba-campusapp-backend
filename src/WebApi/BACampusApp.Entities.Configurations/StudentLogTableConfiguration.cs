using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class StudentLogTableConfiguration : BaseEntityTypeConfiguration<StudentLogTable>
    {
        public void Configure(EntityTypeBuilder<StudentLogTable> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.StudentActionType).IsRequired();
            base.Configure(builder);
        }
    }
}

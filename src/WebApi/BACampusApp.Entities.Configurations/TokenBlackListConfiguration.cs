using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class TokenBlackListConfiguration : AuditableEntityTypeConfiguration<TokenBlackList>
    {
        public override void Configure(EntityTypeBuilder<TokenBlackList> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Token).IsRequired();
        }
    }
}

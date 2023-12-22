using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class RoleConfiguration:IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            new IdentityRole
            {
                Name="Admin",NormalizedName="ADMIN"
            },
            new IdentityRole
            {
                Name="Trainer",NormalizedName="TRAINER"
            },
            new IdentityRole
            {
                Name="Student",NormalizedName="STUDENT"
            });
        }
    }
}

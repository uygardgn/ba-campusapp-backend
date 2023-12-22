using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class SupplementaryResourceEducationSubjectConfiguration : BaseEntityTypeConfiguration<SupplementaryResourceEducationSubject>
    {
        public override void Configure(EntityTypeBuilder<SupplementaryResourceEducationSubject> b)
        {
            #region Many To Many Relationship

            b.HasOne(sres => sres.SupplementaryResources)
                         .WithMany(sr => sr.SupplementaryResourceEducationSubjects)
                         .HasForeignKey(sres => sres.SupplementaryResourceId)
                         .OnDelete(DeleteBehavior.Cascade)
                         .IsRequired();

            // Subject ile ilişki
            b.HasOne(sres => sres.Subjects)
                .WithMany(sub => sub.SupplementaryResourceEducationSubjects)
                .HasForeignKey(sres => sres.SubjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            // Education ile ilişki
            b.HasOne(sres => sres.Educations)
                .WithMany(edu => edu.SupplementaryResourceEducationSubjects)
                .HasForeignKey(sres => sres.EducationId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            #endregion


            base.Configure(b);
        }
    }
}
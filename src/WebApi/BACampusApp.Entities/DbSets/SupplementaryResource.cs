namespace BACampusApp.Entities.DbSets
{
    public class SupplementaryResource : AuditableEntity
    {
        public SupplementaryResource()
        {
            SupplementaryResourceTags = new HashSet<SupplementaryResourceTag>();
            SupplementaryResourceEducationSubjects = new HashSet<SupplementaryResourceEducationSubject>();
        }
        public string Name { get; set; }
        public string FileURL { get; set; }
    
        public FileType? FileType { get; set; }//dosya tipi
        public byte[]? ByteArrayFormat { get; set; }//yüklenen dosya resim tipinde ise byte dizisi formatı
        public ResourceType ResourceType { get; set; }

        public string? Feedback { get; set; }
     
        public ResourcesTypeStatus? ResourcesTypeStatus { get; set; }

        public virtual ICollection<SupplementaryResourceEducationSubject> SupplementaryResourceEducationSubjects { get; set; }

        //Navigation Properties
        public virtual ICollection<SupplementaryResourceTag> SupplementaryResourceTags { get; set; }
    }
}
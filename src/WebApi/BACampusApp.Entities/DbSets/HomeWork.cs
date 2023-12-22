namespace BACampusApp.Entities.DbSets
{
    public class HomeWork:AuditableEntity
    {
		public HomeWork()
		{
			StudentHomeworks = new HashSet<StudentHomework>();
		}
		public string Title { get; set; }//Ödev Başlığı
		public string? Intructions { get; set; }//Yönergeler
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string? ReferansFile { get; set; }//dosya yolu ancak bu senaryoda birden fazla eklenemiyor.
        public FileType? FileType { get; set; }//dosya tipi
        public byte[]? ByteArrayFormat { get; set; }//yüklenen dosya resim tipinde ise byte dizisi formatı
        public Guid SubjectId { get; set; }
		public bool IsLateTurnedIn { get; set; }//geç teslim edilebilir mi?
		public bool IsHasPoint { get; set; }//puanlı mı puansız mı?
		public string? Description { get; set; }
        public virtual ICollection<StudentHomework> StudentHomeworks { get; set; }//atanan öğrenciler
		public Guid? ClassroomId { get; set; }

	}
}

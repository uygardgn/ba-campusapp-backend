namespace BACampusApp.Entities.DbSets
{
    public class StudentHomework : AuditableEntity
	{
		public Guid HomeWorkId { get; set; }
		public Guid StudentId { get; set; }
		public string? AttachedFile { get; set; }
        public FileType? FileType { get; set; }//dosya tipi
        public string? ReferansFile { get; set; }//dosya yolu ancak bu senaryoda birden fazla eklenemiyor.
        public byte[]? ByteArrayFormat { get; set; }//yüklenen dosya resim tipinde ise byte dizisi formatı
        public DateTime? SubmitDate { get; set; }//Teslim tarihi
		public double? Point { get; set; }//Ödev puanı
		public HomeworkState HomeworkState { get; set; }//Ödev Durumu ->atandı/teslim edildi/teslim edilmedi

		public virtual HomeWork HomeWork { get; set; }
		public virtual Student Student { get; set; }
        public string? Feedback { get; set; }
    }
}


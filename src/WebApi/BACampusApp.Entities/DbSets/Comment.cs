using BACampusApp.Core.Enums;

namespace BACampusApp.Entities.DbSets
{
    public class Comment : AuditableEntity
	{
		public Guid ItemId { get; set; }//hangi modüle yorum yapılacaksa(ödev,öğrenci, yardımcı kaynak vb.) o property'nin Id'si
		public string Content { get; set; }
		public string Title { get; set; }
        public ItemType ItemType { get; set; }
        public Guid UserId { get; set; }
    }
}

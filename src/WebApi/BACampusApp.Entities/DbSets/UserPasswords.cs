using System.ComponentModel.DataAnnotations.Schema;

namespace BACampusApp.Entities.DbSets
{
    public class UserPasswords : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
    }
}

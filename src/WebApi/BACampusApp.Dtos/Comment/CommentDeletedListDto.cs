using BACampusApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Comment
{
    public class CommentDeletedListDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ItemId { get; set; }
        public Guid UserId { get; set; }
        public ItemType ItemType { get; set; }

    }
}

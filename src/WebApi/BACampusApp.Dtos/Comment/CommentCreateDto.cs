using BACampusApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Comment
{
    public class CommentCreateDto
    {
        public Guid ItemId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ItemType ItemType { get; set; }
        public Guid UserId { get; set; }

    }
}

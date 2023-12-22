using BACampusApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Comment
{
    public class CommentListDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ItemId { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public ItemType ItemType { get; set; }

        public string itemType
        {
            get
            {
                switch (ItemType)
                {
                    case ItemType.Student:
                        return "Student";
                    case ItemType.HomeWork:
                        return "Home Work";
                    case ItemType.SupplementaryResource:
                        return "Supplementary Resource";
                    default:
                        return "Bilinmeyen Değer";
                }
            }
        }

    }
}

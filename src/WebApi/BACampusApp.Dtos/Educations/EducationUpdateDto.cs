using BACampusApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Educations
{

    public class EducationUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CourseHours { get; set; }
        public string Description { get; set; }
        public Guid SubCategoryId { get; set; }


    }
}

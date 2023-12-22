using BACampusApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Educations
{
    public class EducationListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CourseHours { get; set; }
        public string Description { get; set; }
        public string SubCategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}

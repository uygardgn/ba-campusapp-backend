using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Educations
{
    public class EducationDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CourseHours { get; set; }
        public string Description { get; set; }
        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }



    }
}

using Microsoft.AspNetCore.Identity;

namespace BACampusApp.Dtos.Educations
{
    public class EducationCreateDto
    {
        public string Name { get; set; }
        public int CourseHours { get; set; }
        public string Description { get; set; }

        public Guid SubCategoryId { get; set; }

    }
}

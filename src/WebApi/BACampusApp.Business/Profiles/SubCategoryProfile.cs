using BACampusApp.Dtos.SubCategory;
using System.Runtime.ConstrainedExecution;

namespace BACampusApp.Business.Profiles
{
    public class SubCategoryProfile : Profile
    {
        public SubCategoryProfile()
        {
            CreateMap<SubCategoryCreateDto, Category>();
            CreateMap<Category, SubCategoryDto>();
            CreateMap<SubCategoryUpdateDto, Category>();
        }
    }
}

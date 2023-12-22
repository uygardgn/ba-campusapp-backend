using AutoMapper;
using BACampusApp.Dtos.Categorys;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile() 
        {
            CreateMap<CategoryCreateDto, Category>();           
            CreateMap<Category, CategoryDetailsDto>();
            CreateMap<Category, CategoryListDto>();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDeletedListDto>();

            CreateMap<Category, CategoryListByParentIdDto>().ReverseMap()
                
                .ForMember(x => x.ParentCategoryId, opt => opt.MapFrom(x => x.ParentCategoryId))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));
            CreateMap<Category, CategoryListBySubCategoryDto>().ReverseMap()
                .ForMember(x => x.SubCategories, opt => opt.MapFrom(x => x.SubCategoryId))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));
        }
    }
}

using AutoMapper;
using BACampusApp.Dtos.Educations;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.Business.Profiles
{
    public class EducationProfile : Profile
    {
        public EducationProfile()
        {
            CreateMap<EducationCreateDto, Education>();
            CreateMap<Education, EducationDetailsDto>();
            CreateMap<EducationUpdateDto, Education>();
            CreateMap<Education, EducationListDto>();
            CreateMap<Education, EducationDto>();
            CreateMap<EducationDto, EducationUpdateDto>();
            CreateMap<EducationUpdateDto, EducationDetailsDto>();
            CreateMap<Education, EducationDeletedListDto>();
            CreateMap<Education, EducationDetailsDto>()
               .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(x => x.SubCategoryId))
               .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(x => x.Category.Name))
               .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(x => x.Category.ParentCategoryId))
               .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(x => x.Category.ParentCategory.Name))
               .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom((src, dest, destMember, context) =>
              {
                  if (src.Category.ParentCategory != null)
                  {
                      return src.Category.ParentCategory.Name;
                  }
                  return src.Category.Name; // Eğer ebeveyn kategori yoksa kategori adını kullan
              }))
            .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom((src, dest, destMember, context) =>
             {
                 if (src.Category.ParentCategoryId != null)
                 {
                     return src.Category.ParentCategoryId;
                 }
                 return src.SubCategoryId; // Eğer ebeveyn kategori yoksa kategori id kullan
             }));
        }
    }
}
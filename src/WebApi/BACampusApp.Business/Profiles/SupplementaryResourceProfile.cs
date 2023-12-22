using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.StaticFiles;

namespace BACampusApp.Business.Profiles
{
    public class SupplementaryResourceProfile : Profile
    {
        public SupplementaryResourceProfile()
        {
            CreateMap<SupplementaryResourceCreateDto, SupplementaryResource>();



            CreateMap<SupplementaryResource, SupplementaryResourceListDto>()
                .ForMember(dest => dest.MimeType, opt => opt.MapFrom<SupplementaryResourceListResolver>())/*.ForMember(dest => dest.SubjectId, opt => opt.MapFrom(x => x.EducationSubject.SubjectId)).ForMember(dest => dest.SubjectName, opt => opt.MapFrom(x => x.EducationSubject.Subject.Name))*/;


            CreateMap<SupplementaryResource, SupplementaryResourceDetailsDto>()/*.ForMember(dest => dest.EducationId, opt => opt.MapFrom(x => x.EducationSubject.EducationId)).ForMember(dest => dest.SubjectId, opt => opt.MapFrom(x => x.EducationSubject.SubjectId))*/ ;



            CreateMap<SupplementaryResource, SupplementaryResourceDto>();
            CreateMap<SupplementaryResourceUpdateDto, SupplementaryResource>().ForMember(dest => dest.FileURL, opt =>
            {
                opt.MapFrom<SupplementaryResourceUpdateResolver>();
            }).ReverseMap();

            CreateMap<SupplementaryResource, SupplementaryResourceDeletedListDto>();

            CreateMap<SupplementaryResourceRecoverDto, SupplementaryResource>();

            CreateMap<SupplementaryResource, SupplementaryResourceDeletedDetailsDto>();

            CreateMap<SupplementaryResource, SupplementaryResourceFeedBackDto>().ReverseMap();
        }



    }
    public class SupplementaryResourceUpdateResolver : IValueResolver<SupplementaryResourceUpdateDto, SupplementaryResource, string>
    {
        public string Resolve(SupplementaryResourceUpdateDto source, SupplementaryResource destination, string destMember, ResolutionContext context)
        {
            return (source.FileURL != null) ? destination.FileURL : destination.FileURL;
        }
    }

    public class SupplementaryResourceListResolver : IValueResolver<SupplementaryResource, SupplementaryResourceListDto, string>
    {
        public string Resolve(SupplementaryResource source, SupplementaryResourceListDto destination, string destMember, ResolutionContext context)
        {
            string contentType = string.Empty;
            if (source.FileURL != null && source.ResourceType == ResourceType.Document)
            {
                new FileExtensionContentTypeProvider().TryGetContentType(source.FileURL, out contentType);
                contentType = contentType ?? "application/octet-stream";
            }
            return contentType;
        }
    }
}
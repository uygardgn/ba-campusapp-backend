namespace BACampusApp.Business.Profiles
{
    public class BranchProfile : Profile
    {
        public BranchProfile()
        {
            CreateMap<BranchCreateDto, Branch>();
            CreateMap<Branch, BranchDto>();
            CreateMap<BranchUpdateDto, Branch>();
            CreateMap<Branch, BranchDeletedListDto>();
        }
    }
}

using BACampusApp.Dtos.GroupType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupType = BACampusApp.Entities.DbSets.GroupType;

namespace BACampusApp.Business.Concretes
{
    public class GroupTypeManager : IGroupTypeService
    {
        private readonly IGroupTypeRepository _groupTypeRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public GroupTypeManager(IGroupTypeRepository repository, IMapper mapper, IStringLocalizer<Resource> stringLocalizer)
        {
            _groupTypeRepository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// Bu metot yeni bir GroupType nesnesi oluşturmak için kullanılmaktadır.
        /// </summary>
        /// <param name="groupTypeCreateDto"></param>
        /// <returns></returns>
        public async Task<IResult> CreateAsync(GroupTypeCreateDto groupTypeCreateDto)
        {
            if (await _groupTypeRepository.AnyAsync(x => x.Name.ToLower() == groupTypeCreateDto.Name.ToLower()))
                return new ErrorResult(_stringLocalizer[Messages.GroupTypeAlreadyExists]);
            var toBeCreated = _mapper.Map<GroupType>(groupTypeCreateDto);
            await _groupTypeRepository.AddAsync(toBeCreated);
            await _groupTypeRepository.SaveChangesAsync();
            var groupTypeDto = _mapper.Map<GroupTypeDto>(toBeCreated);
            return new SuccessDataResult<GroupTypeDto>(groupTypeDto, _stringLocalizer[Messages.AddSuccess]);
        }
        /// <summary>
        /// Bu metot bir GroupType nesnesi silmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var hasClassroom = await _groupTypeRepository.GetByIdAsync(id, true);
            if (hasClassroom == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            await _groupTypeRepository.DeleteAsync(hasClassroom);
            await _groupTypeRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var entites = await _groupTypeRepository.GetAllDeletedAsync();
            if (entites.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<GroupTypeDeleteListDto>>(_mapper.Map<List<GroupTypeDeleteListDto>>(entites), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetAllAsync()
        {
            var groupTypes = await _groupTypeRepository.GetAllAsync();
            if (groupTypes.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            var groupTypeDtos = _mapper.Map<IEnumerable<GroupTypeDto>>(groupTypes);
            return new SuccessDataResult<IEnumerable<GroupTypeDto>>(groupTypeDtos, _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var groupType = await _groupTypeRepository.GetByIdAsync(id);
            if (groupType == null)
                return new ErrorResult(_stringLocalizer[Messages.BranchNotFound]);
            var groupTypeDto = _mapper.Map<GroupTypeDto>(groupType);
            return new SuccessDataResult<GroupTypeDto>(groupTypeDto, _stringLocalizer[Messages.FoundSuccess]);
        }
        /// <summary>
        /// Bu metot bir Branch nesnesini güncellemek için kullanılmaktadır.
        /// </summary>
        /// <param name="groupTypeUpdateDto"></param>
        /// <returns></returns>
        public async Task<IResult> UpdateAsync(GroupTypeUpdateDto groupTypeUpdateDto)
        {
            var entity = await _groupTypeRepository.GetByIdAsync(groupTypeUpdateDto.Id);
            if (entity == null)
                return new ErrorResult(_stringLocalizer[Messages.BranchNotFound]);
            var updatedEntity = _mapper.Map(groupTypeUpdateDto, entity);
            await _groupTypeRepository.UpdateAsync(updatedEntity);
            await _groupTypeRepository.SaveChangesAsync();
            return new SuccessDataResult<GroupTypeUpdateDto>(groupTypeUpdateDto, _stringLocalizer[Messages.UpdateSuccess]);
        }
    }
}
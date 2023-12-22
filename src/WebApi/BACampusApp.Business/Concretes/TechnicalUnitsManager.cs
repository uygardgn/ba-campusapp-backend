using BACampusApp.Dtos.TechnicalUnits;
using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{
    public class TechnicalUnitsManager : ITechnicalUnitsService
    {
        private readonly IMapper _mapper;
        private readonly ITechnicalUnitsRepository _technicalUnitsRepository;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public TechnicalUnitsManager(IMapper map, ITechnicalUnitsRepository technicalUnitsRepository, IStringLocalizer<Resource> stringLocalizer)
        {
            _mapper = map;
            _technicalUnitsRepository = technicalUnitsRepository;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        ///     Veritabanına yeni bir TechnicalUnits ekler.
        /// </summary>
        /// <param name="newUnits">TUnitsCreateDto tipinde newUnits parametresini alır </param>
        ///  <returns>SuccessDataResult<TUnitsCreateDto>, ErrorDataResult<TUnitsCreateDto></returns>
        public async Task<IResult> AddAsync(TUnitCreateDto TUnitsCreateDto)
        {
            bool hasUnits = await _technicalUnitsRepository.AnyAsync(s => s.Name.ToLower() == TUnitsCreateDto.Name.ToLower());
            if (hasUnits)
                return new ErrorResult(_stringLocalizer[Messages.TechnicalUnitsAlreadyExists]);
            var TUnit = await _technicalUnitsRepository.AddAsync(_mapper.Map<TechnicalUnits>(TUnitsCreateDto));
            await _technicalUnitsRepository.SaveChangesAsync();
            return new SuccessDataResult<TUnitDto>(_mapper.Map<TUnitDto>(TUnit), _stringLocalizer[Messages.AddSuccess]);
        }

        /// <summary>
        /// Belirtilen Id'ye sahip TechnicalUnits nesnesini silinmiş olarak işaretler.
        /// Eğer belirtilen Id'ye sahip bir TechnicalUnits nesnesi bulunamazsa, hata döner.
        /// </summary>
        /// <param name="id">Silinmiş olarak işaretlenecek TechnicalUnits nesnesinin Id'si</param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingTunits = await _technicalUnitsRepository.GetByIdAsync(id);
            if (deletingTunits == null)
                return new ErrorResult(_stringLocalizer[Messages.TechnicalUnitsNotFound]);
            if (deletingTunits.Categories.Any(cat => cat.Status == Status.Active))
            {
                return new ErrorResult(_stringLocalizer[Messages.TecnicalUnitCanNotDelete]);
            }
            await _technicalUnitsRepository.DeleteAsync(deletingTunits);
            await _technicalUnitsRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }

        /// <summary>
        /// GetByIdAsync() metodu database de kayıtlı id si verilen TechnicalUnits'i çeker ve TUnitsDetailsDto'ya Map'leyerek TechnicalUnits nesnesine çevirir. En son olarak bu nesneyi ve işlemin durumuna göre verilmek istenen mesajı birlikte döner.
        /// </summary>
        /// /// <param name="id">detayları getirilmek istenen category nesnesinin Guid tipinde Id si </param>
        /// <returns>SuccessDataResult<TUnitsDetailsDto>(TUnitsDetailsDto, Messages.FoundSuccess)</returns> 
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var tUnits = await _technicalUnitsRepository.GetByIdAsync(id);
            if (tUnits == null)
                return new ErrorResult(_stringLocalizer[Messages.TechnicalUnitsNotFound]);
            var tUnitsDto = _mapper.Map<TUnitDetailsDto>(tUnits);
            return new SuccessDataResult<TUnitDetailsDto>(tUnitsDto, _stringLocalizer[Messages.FoundSuccess]);
        }
        /// <summary>
        ///  Bu metot veritabanındaki tüm TechnicalUnits çeker ve bu TechnicalUnits listesini TUnitsListDto ile eşleyip çıktısını verir.
        /// </summary>
        /// <returns>SuccessDataResult<TUnitsListDto>, ErrorDataResult<TUnitsListDto></returns>
        public async Task<IResult> GetListAsync()
        {
            var tUnits = await _technicalUnitsRepository.GetAllAsync();
            if (tUnits.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<TUnitListDto>>(_mapper.Map<List<TUnitListDto>>(tUnits), _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// Bu metot TechnicalUnits nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity">güncellenmek istenen TechnicalUnits nesnesinin TUnitsUpdateDto tipinde entity'si</param>
        /// <returns>ErrorDataResult<TUnitsUpdateDto>, SuccessDataResult<TUnitsUpdateDto></returns>
        public async Task<IResult> UpdateAsync(TUnitUpdateDto tUnitsUpdateDto)
        {
            bool hasUnits = await _technicalUnitsRepository.AnyAsync(s => s.Name.ToLower() == tUnitsUpdateDto.Name.ToLower());
            if(hasUnits)
                return new ErrorResult(_stringLocalizer[Messages.TechnicalUnitsAlreadyExists]);
            
            var tUnit = await _technicalUnitsRepository.GetByIdAsync(tUnitsUpdateDto.Id);
            if (tUnit == null)
                return new ErrorResult(_stringLocalizer[Messages.TechnicalUnitsNotFound]);
            await _technicalUnitsRepository.UpdateAsync(_mapper.Map(tUnitsUpdateDto, tUnit));
            await _technicalUnitsRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var tUnits = await _technicalUnitsRepository.GetAllDeletedAsync();
            if (tUnits.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<TUnitDeletedListDto>>(_mapper.Map<List<TUnitDeletedListDto>>(tUnits), _stringLocalizer[Messages.ListedSuccess]);
        }
    }
}

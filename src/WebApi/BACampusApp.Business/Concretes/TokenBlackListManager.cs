using BACampusApp.Dtos.TokenBlackList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class TokenBlackListManager : ITokenBlackListService
    {
        private readonly ITokenBlackListRepository _tokenBlackListRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public TokenBlackListManager(ITokenBlackListRepository tokenBlackListRepository, IMapper mapper, IStringLocalizer<Resource> stringLocalizer)
        {
            _tokenBlackListRepository = tokenBlackListRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Bu metot yeni bir TokenBlackList nesnesi oluşturmak için kullanılmaktadır.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(string token)
        {
            TokenBlackListCreateDto tokenBlackListCreateDto = new();
            tokenBlackListCreateDto.Token = token;  

            var toBeCreated = _mapper.Map<TokenBlackList>(tokenBlackListCreateDto);
           
            await _tokenBlackListRepository.AddAsync(toBeCreated);
            var isAdded = await _tokenBlackListRepository.SaveChangesAsync() > 0;

            return isAdded;
        }
       

    
    }
}

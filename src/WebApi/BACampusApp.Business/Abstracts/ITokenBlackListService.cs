using BACampusApp.Dtos.TokenBlackList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface ITokenBlackListService
    {
       /// <summary>
       /// Çıkış yapan userın tokenını blackliste atmak için kullanılır
       /// </summary>
       /// <param name="token"></param>
       /// <returns></returns>
        Task<bool> CreateAsync(string token);

    }
}

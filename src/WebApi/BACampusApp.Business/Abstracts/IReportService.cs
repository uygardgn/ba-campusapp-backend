using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface IReportService
    {
        /// <summary>
        /// Bu metot tüm aktif öğrencilerin sayısının dönmesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>    
        Task<IResult> ActiveStudentCountAsync();

        /// <summary>
        /// Bu metot tüm aktif eğitmenlerin sayısının dönmesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>    
        Task<IResult> ActiveTrainerCountAsync();

        /// <summary>
        /// Tüm kullanıcı tiplerinin aktif kullanıcı sayısının dönmesini sağlar
        /// </summary>
        /// <returns></returns>
        Task<IResult> ActiveAllUserCount();
    }
}

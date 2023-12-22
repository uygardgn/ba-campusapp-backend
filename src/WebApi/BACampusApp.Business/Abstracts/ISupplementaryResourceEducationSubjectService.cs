using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface ISupplementaryResourceEducationSubjectService
    {
        /// <summary>
        /// Bu metot SupplementaryResourceEducationSubject nesnesinin oluşturulması için kullanılmakatadır.
        /// </summary>
        /// <param name="supplementaryResourceEducationSubjectsCreateDto"></param>
        /// <returns></returns>
        Task<IResult> CreateAsync(SupplementaryResourceEducationSubjectCreateDto supplementaryResourceEducationSubjectsCreateDto);

        /// <summary>
        /// Bu metot verilen id'ye uygun olarak SupplementaryResourceEducationSubject nesnesinin silinmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> DeleteAsync(Guid id);

        /// <summary>
        /// Bu metot verilen nesne içerisindeki id'ye uygun olarak SupplementaryResourceEducationSubject nesnesinin güncellenmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="supplementaryResourceEducationSubjectsUpdateDto"></param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(SupplementaryResourceEducationSubjectUpdateDto supplementaryResourceEducationSubjectsUpdateDto);

        /// <summary>
        /// Bu metot tüm SupplementaryResourceEducationSubject nesnelerinin listelenmesi için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetAllAsync();

        /// <summary>
        /// Bu metot verilen id'ye uygun SupplementaryResourceEducationSubject nesnesinin gösterilmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        ///  Bu metot Yöneticinin,silinen tüm SupplemantaryResource nesnesi listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();
    }
}

namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;

   public interface ILogViewer
   {
        LogDto Get(int id);

        /// <summary>
        /// Inserts a new item.
        /// </summary>
        /// <param name="t">The business object. </param>
        int Insert(LogDto entity);

        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="id">The business object's id</param>
        bool Delete(int id);

        /// <summary>
        /// Read all item in the collection by Id
        /// </summary>
        /// <returns></returns>   
        List<LogDto>  GetAll(int iskip, int itake, string isearch);
        List<LogDto> GetAll(int iskip, int itake);
        int Count();
    }
}

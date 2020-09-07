namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Generic repository base interface. 
    /// Provides basic CRUD (Create, Read, Update, Delete) and a couple more methods.
    /// </summary>
    /// <typeparam name="T">The business object type.</typeparam>
    public interface IBaseRepository<T> where T:class
    {
        /// <summary>
        /// Reads an individual item.
        /// </summary>
        /// <param name="id">The business object's id.</param>
        /// <returns></returns>
        T Get(int id);       

        /// <summary>
        /// Inserts a new item.
        /// </summary>
        /// <param name="t">The business object. </param>
        Int32 Insert(T entity);       

        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="id">The business object's id</param>
        bool Delete(int id);
       
    }
}

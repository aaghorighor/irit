using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suftnet.Cos.DataAccess
{
    /// <summary>
    /// Generic repository base interface. 
    /// Provides basic CRUD (Create, Read, Update, Delete) and a couple more methods.
    /// </summary>
    /// <typeparam name="T">The business object type.</typeparam>
    public interface IRepository<T> where T:class
    {

        /// <summary>
        /// Read all item in the collection
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// Read all item in the collection by Id
        /// </summary>
        /// <returns></returns>
        List<T> GetAll(int Id);

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
        int Insert(T entity);

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="t">The business object.</param>
        bool Update(T entity);

        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="id">The business object's id</param>
        bool Delete(int id);       
    }
}

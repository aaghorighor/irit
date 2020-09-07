namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Settings :ISettings
    {
        public int TenantId { get; set; }  

        /// <summary>
        /// Add new entity 
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Id of new entity</returns>
        public int Insert(Action.Setting entity)
        {
            int id = 0;
            using (var context = DataContextFactory.CreateContext())
            {
                context.Settings.Add(entity);
                context.SaveChanges();
                id = entity.ID;
            }
            return id;
        }

        /// <summary>
        /// Update a Setting record in the database
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>True or false indicating update status</returns>
        public bool Update(Action.Setting entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            { 
               var objToUpdate = context.Settings.Single(o => o.ID == entity.ID);

               if (objToUpdate != null)
               {
                   objToUpdate.Active = entity.Active;
                   objToUpdate.Description = entity.Description;
                   objToUpdate.Title = entity.Title;
                   objToUpdate.ClassId = entity.ClassId;
                   objToUpdate.ImageUrl = entity.ImageUrl;

                    try
                   {
                       context.SaveChanges();
                       response = true;
                   }
                   catch (ChangeConflictException)
                   {
                  
                       context.SaveChanges();
                       response = true;
                   }
               }              
            }         
            
            return response;
        }

        /// <summary>
        /// Delete main settings and all children
        /// </summary>
        /// <param name="id">Id to delete</param>
        /// <returns>false or true indicating delete status</returns>
        public bool Delete(int id)
        {
           bool response = false;
           using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Settings.FirstOrDefault(o => o.ID == id);
                if (objToDelete != null)
                {
                    var commons = context.Commons.Where(x => x.Settingid == (int)id);
                                       
                    context.Commons.RemoveRange(context.Commons.Where(x => x.Settingid ==(int)id));
                    context.Settings.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
               
            }
            return response; // return status
        }

        ///// <summary>
        ///// Get child Setting by passing in parent Id
        ///// </summary>
        ///// <param name="id">Id</param>
        ///// <returns></returns>
        public IEnumerable<SettingsDTO> GetAllByID(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                return context.Settings.Where(o => o.ID == Id).Select(o => new SettingsDTO { QueryTitle = o.Title, ImageUrl = o.ImageUrl, Active = o.Active, Description = o.Description, Id = o.ID, ClassId = o.ClassId, Title = o.Title }).OrderBy(ob => ob.Title).ToList();
            }
        }              

        /// <summary>
        /// Gets list of Setting in a given order
        /// </summary>
        /// <returns>List of Setting</returns>
        public IEnumerable<SettingsDTO> GetAll()
        {
           using (var context = DataContextFactory.CreateContext())
           {
               var result = (from o in context.Settings
                            join c in context.Commons on o.ClassId equals c.ID 
                            orderby o.Title
                             select new SettingsDTO { QueryTitle = o.Title, ImageUrl = o.ImageUrl, Active = o.Active, Description = o.Description, Id = o.ID, Class = c.Title, ClassId = o.ClassId, Title = o.Title }).ToList();

             return  result;
           }
        }

        public IEnumerable<SettingsDTO> GetAll(int classId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Settings
                              join c in context.Commons on o.ClassId equals c.ID
                              where o.ClassId == classId 
                              orderby o.Title
                              select new SettingsDTO { QueryTitle = o.Title, ImageUrl = o.ImageUrl, Active = o.Active, Description = o.Description, Id = o.ID, Class = c.Title, ClassId = o.ClassId, Title = o.Title }).ToList();

                return result;
            }
        }

        ///// <summary>
        ///// Get a Setting by passing in Id
        ///// </summary>
        ///// <param name="id">Id</param>
        ///// <returns></returns>
        public SettingsDTO Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Settings
                              join c in context.Commons on o.ClassId equals c.ID
                              where o.ID == Id 
                              orderby o.Title
                              select new SettingsDTO { QueryTitle = o.Title, ImageUrl = o.ImageUrl, Active = o.Active, Description = o.Description, Id = o.ID, Class = c.Title, ClassId = o.ClassId, Title = o.Title }).FirstOrDefault();
                return result;
            }       
               
        }     
    }
}
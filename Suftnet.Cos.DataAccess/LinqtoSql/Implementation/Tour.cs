namespace Suftnet.Cos.DataAccess
{  
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Tour : ITour
    {        
        public List<TourDto> GetAll()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tours
                                 join s in context.Commons on o.StyleTypeId equals s.ID
                                 orderby o.Id descending 
                                 select new TourDto { StyleType = s.Title, StyleTypeId = o.StyleTypeId, CreatedDT= o.CreatedDt, Active = o.Active, Description = o.Description, SortOrder = o.SortOrder, ImageUrl = o.ImageUrl, Title = o.Title, Id = o.Id }).ToList();              
                return objResult.Count == 0 ? new List<TourDto>() : objResult;    
             }
        }

        public List<TourDto> LoadTours()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tours
                                 join s in context.Commons on o.StyleTypeId equals s.ID
                                 where o.Active == true
                                 orderby o.SortOrder ascending
                                 select new TourDto { StyleType = s.Title, StyleTypeId = o.StyleTypeId, CreatedDT= o.CreatedDt, Active = o.Active, Description = o.Description, SortOrder = o.SortOrder, ImageUrl = o.ImageUrl, Title = o.Title, Id = o.Id }).ToList();
                return objResult.Count == 0 ? new List<TourDto>() : objResult;
            }
        }

        public List<TourDto> GetAll(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tours
                                 join s in context.Commons on o.StyleTypeId equals s.ID
                                 where o.StyleTypeId == Id
                                 orderby o.Id descending
                                 select new TourDto { StyleType = s.Title, StyleTypeId = o.StyleTypeId, CreatedDT= o.CreatedDt, Active = o.Active, Description = o.Description, SortOrder = o.SortOrder, ImageUrl = o.ImageUrl, Title = o.Title, Id = o.Id }).ToList();
                return objResult.Count == 0 ? new List<TourDto>() : objResult;
            }
        }

        public TourDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tours
                                 join s in context.Commons on o.StyleTypeId equals s.ID
                                 where o.Id == Id                               
                                 select new TourDto { StyleType = s.Title, StyleTypeId = o.StyleTypeId, CreatedDT= o.CreatedDt, Active = o.Active, Description = o.Description, ImageUrl = o.ImageUrl, SortOrder = o.SortOrder, Title = o.Title, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }     
               
        public int Insert(TourDto entity)
        {
            int id = 0;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Tour() { StyleTypeId = entity.StyleTypeId, ImageUrl = entity.ImageUrl, Description = entity.Description, Title = entity.Title, Active = entity.Active,  SortOrder = entity.SortOrder, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };

                context.Tours.Add(obj);
                context.SaveChanges();
                id = obj.Id;
            }
            return id;
        }
       
        public bool Update(TourDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Tours.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.Active = entity.Active;
                    objToUpdate.Description = entity.Description;
                    objToUpdate.Title = entity.Title;
                    objToUpdate.ImageUrl = entity.ImageUrl;
                    objToUpdate.StyleTypeId = entity.StyleTypeId;
                    objToUpdate.SortOrder = entity.SortOrder;
               
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
               
        public bool Delete(int id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Tours.SingleOrDefault(o => o.Id == id);
                if (objToDelete != null)
                {
                    context.Tours.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;     
        }            
     
    }
}

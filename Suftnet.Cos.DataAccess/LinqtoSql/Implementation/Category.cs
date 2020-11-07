namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class Category : ICategory
    {       
        public CategoryDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Categories                                                      
                                 where o.Id == Id 
                                 select new CategoryDto { IndexNo = o.IndexNo, Description = o.Description, ImageUrl = o.ImageUrl, Name = o.Name, Status = o.Status, CreatedDT = o.CreatedDt,  CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {

                var objToDelete = context.Categories.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Categories.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public Guid Insert(CategoryDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Category() { Id = entity.Id, IndexNo = entity.IndexNo, Description = entity.Description, Status = entity.Status, Name = entity.Name, ImageUrl = entity.ImageUrl, TenantId = entity.TenantId, CreatedDt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.Categories.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool Update(CategoryDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Categories.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {                                                  
                    objToUpdate.Description = entity.Description;
                    objToUpdate.Status = entity.Status;
                    objToUpdate.ImageUrl = entity.ImageUrl;
                    objToUpdate.Name = entity.Name;
                    objToUpdate.IndexNo = entity.IndexNo;

                    try
                    {
                        context.SaveChanges();
                        response = true;
                    }
                    catch (ChangeConflictException)
                    {                    
                        response = false;
                    }
                }

                return response;
            }
        }

        public List<CategoryDto> GetAll(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Categories
                                 where o.TenantId == tenantId
                                 orderby o.IndexNo ascending
                                 select new CategoryDto { IndexNo = o.IndexNo, Description = o.Description, ImageUrl = o.ImageUrl, Name = o.Name, Status = o.Status, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public List<CategoryDto> GetBy(bool status, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.Categories                              
                                 where o.TenantId == tenantId && o.Status == status
                                 orderby o.IndexNo ascending
                                 select new CategoryDto { IndexNo = o.IndexNo, Description = o.Description, ImageUrl = o.ImageUrl, Name = o.Name, Id = o.Id }).ToList();
                return obj;
            }
        }

        public List<MobileCategoryDto> GetBy(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.Categories
                           where o.TenantId == tenantId && o.Status == true
                           orderby o.IndexNo ascending
                           select new MobileCategoryDto { IndexNo = o.IndexNo, Description = o.Description, ImageUrl = o.ImageUrl, Name = o.Name, Id = o.Id }).ToList();
                return obj;
            }
        }
    }
}



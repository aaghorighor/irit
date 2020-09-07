namespace Suftnet.Cos.DataAccess
{   
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Faq : IFaq
    {
        public FaqDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Faqs
                                 where o.Id == Id                          
                                 select new FaqDto { SortOrderId = o.SortOrderId, Publish = o.Publish, Title = o.Title, ShortDescription = o.ShortDescription, CreatedBy = o.CreatedBy,CreatedDT= o.CreatedDt, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        } 
    
        public bool Delete(int Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Faqs.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Faqs.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;          
        }

        public int Insert(FaqDto entity)
        {          
            using (var context = DataContextFactory.CreateContext())
            {
                var task = new Action.Faq() {  SortOrderId = entity.SortOrderId, ShortDescription = entity.ShortDescription, Publish = entity.Publish, Title = entity.Title,  CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.Faqs.Add(task);
                context.SaveChanges();
                return task.Id;
            }          
        }

        public bool Update(FaqDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Faqs.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.ShortDescription = entity.ShortDescription;
                    objToUpdate.Publish = entity.Publish;              
                    objToUpdate.Title = entity.Title;
                    objToUpdate.SortOrderId = entity.SortOrderId;

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

        public List<FaqDto> GetAll()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Faqs                    
                                 orderby o.Id descending
                                 select new FaqDto { SortOrderId = o.SortOrderId, Publish = o.Publish,Title = o.Title, ShortDescription = o.ShortDescription, CreatedBy = o.CreatedBy, CreatedDT= o.CreatedDt, Id = o.Id }).ToList();
                return objResult;
            }                           
        }

        public List<FaqDto> LoadFaq()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Faqs                              
                                 where o.Publish == true 
                                 orderby o.Id descending
                                 select new FaqDto { SortOrderId = o.SortOrderId, Publish = o.Publish, Title = o.Title, ShortDescription = o.ShortDescription, CreatedBy = o.CreatedBy, CreatedDT= o.CreatedDt, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public List<FaqDto> GetAll(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Faqs                               
                                 orderby o.Id descending
                                 select new FaqDto { SortOrderId = o.SortOrderId, Publish = o.Publish, Title = o.Title, ShortDescription = o.ShortDescription, CreatedBy = o.CreatedBy, CreatedDT= o.CreatedDt, Id = o.Id }).ToList();
                return objResult;
            }                                    
        }
    
    }
}

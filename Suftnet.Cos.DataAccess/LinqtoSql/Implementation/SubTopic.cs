namespace Suftnet.Cos.DataAccess
{   
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class SubTopic : ISubTopic
    {
        public SubTopicDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.SubTopics                                                           
                                 where o.Id == Id
                                 select new SubTopicDto { Title = o.Title, IndexNo = o.IndexNo, ImageUrl = o.ImageUrl, TopicId = o.TopicId, Description = o.Description, CreatedBy = o.CreatedBy,CreatedDT= o.CreatedDt, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        } 
    
        public bool Delete(int Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.SubTopics.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.SubTopics.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;          
        }

        public int Insert(SubTopicDto entity)
        {          
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.SubTopic() { Title = entity.Title, IndexNo = entity.IndexNo, ImageUrl = entity.ImageUrl, Description = entity.Description, TopicId = entity.TopicId, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.SubTopics.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }          
        }

        public bool Update(SubTopicDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.SubTopics.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.Description = entity.Description;                
                    objToUpdate.TopicId = entity.TopicId;                    
                    objToUpdate.ImageUrl = entity.ImageUrl;
                    objToUpdate.Title = entity.Title;
                    objToUpdate.IndexNo = entity.IndexNo;
              
                    try
                    {
                        context.SaveChanges();
                        response = true;
                    }
                    catch (ChangeConflictException)
                    {                                        
                        response = true;
                    }
                }
            }
            return response;
        }
            

        public List<SubTopicDto> GetAll(int Id)
        {           
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.SubTopics                                                           
                                 where o.TopicId == Id
                                 orderby o.IndexNo ascending
                                 select new SubTopicDto { Title = o.Title, IndexNo = o.IndexNo, ImageUrl = o.ImageUrl, TopicId = o.TopicId, Description = o.Description, CreatedBy = o.CreatedBy, CreatedDT= o.CreatedDt, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public List<SubTopicDto> Fetch(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.SubTopics                              
                                 where o.TopicId == Id
                                 orderby o.IndexNo ascending
                                 select new SubTopicDto { Title = o.Title, IndexNo = o.IndexNo, ImageUrl = o.ImageUrl, TopicId = o.TopicId, Description = o.Description, CreatedBy = o.CreatedBy, CreatedDT = o.CreatedDt, Id = o.Id }).ToList();
                return objResult;
            }
        }
    }
}

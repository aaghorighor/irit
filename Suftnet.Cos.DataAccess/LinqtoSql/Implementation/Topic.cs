namespace Suftnet.Cos.DataAccess
{   
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Topic : ITopic
    {
        public TopicDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Topics
                                 join s in context.Commons on o.TopicId equals s.ID                               
                                 where o.Id == Id
                                 select new TopicDto { VideoUrl = o.VideoUrl, VideoId = o.VideoId, IndexNo = o.IndexNo, ImageUrl = o.ImageUrl, Topic = s.Title,  Publish = o.Publish, TopicId = o.TopicId, Description = o.Description, CreatedBy = o.CreatedBy,CreatedDT= o.CreatedDT, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Find(int topicId, int chapterId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Topics                          
                                 where o.TopicId == topicId && o.ChapterId == chapterId
                                 select o).FirstOrDefault();
                return objResult != null ? true : false;
            }
        }

        public bool Delete(int Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Topics.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Topics.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;          
        }

        public int Insert(TopicDto entity)
        {          
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Topic() { VideoUrl =entity.VideoUrl, VideoId = entity.VideoId, IndexNo = entity.IndexNo, ImageUrl = entity.ImageUrl, Description = entity.Description, Publish = entity.Publish, ChapterId = entity.ChapterId, TopicId = entity.TopicId, CreatedBy = entity.CreatedBy, CreatedDT = entity.CreatedDT };
                context.Topics.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }          
        }

        public bool Update(TopicDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Topics.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.Description = entity.Description;
                    objToUpdate.Publish = entity.Publish;
                    objToUpdate.TopicId = entity.TopicId;                    
                    objToUpdate.ImageUrl = entity.ImageUrl;
                    objToUpdate.IndexNo = entity.IndexNo;
                    objToUpdate.VideoId = entity.VideoId;
                    objToUpdate.VideoUrl = entity.VideoUrl;

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
            

        public List<TopicDto> GetAll(int Id)
        {           
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Topics
                                 join s in context.Commons on o.TopicId equals s.ID                                
                                 where o.ChapterId == Id
                                 orderby o.IndexNo ascending
                                 select new TopicDto { VideoUrl = o.VideoUrl, VideoId = o.VideoId, IndexNo = o.IndexNo, ImageUrl = o.ImageUrl, Topic = s.Title, Publish = o.Publish, TopicId = o.TopicId, Description = o.Description, CreatedBy = o.CreatedBy, CreatedDT= o.CreatedDT, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public List<TopicDto> Fetch(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Topics
                                 join t in context.Commons on o.TopicId equals t.ID
                                 where o.ChapterId == Id && o.Publish == true
                                 orderby o.IndexNo ascending
                                 select new TopicDto { VideoUrl = o.VideoUrl, VideoId = o.VideoId,  IndexNo = o.IndexNo, ImageUrl = o.ImageUrl, Topic = t.Title, Publish = o.Publish, TopicId = o.TopicId, Description = o.Description, CreatedBy = o.CreatedBy, CreatedDT = o.CreatedDT, Id = o.Id }).ToList();
                return objResult;
            }
        }
    }
}

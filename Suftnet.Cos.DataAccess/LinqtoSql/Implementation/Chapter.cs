namespace Suftnet.Cos.DataAccess
{   
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Chapter : IChapter
    {
        public ChapterDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Chapters
                                 join s in context.Commons on o.SubSectionId equals s.ID
                                 join e in context.Settings on o.SectionId equals e.ID
                                 where o.Id == Id
                                 select new ChapterDto { ImageUrl = o.ImageUrl, Section = e.Title, SectionId = o.SectionId, SubSection = s.Title,  Publish = o.Publish, SubSectionId = o.SubSectionId, Description = o.Description, CreatedBy = o.CreatedBy,CreatedDT= o.CreatedDt, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Find(int subSectionId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Chapters                                
                                 where o.SubSectionId == subSectionId
                                 select o).FirstOrDefault();
                return objResult == null ? false: true;
            }
        }

        public bool Delete(int Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Chapters.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Chapters.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;          
        }

        public int Insert(ChapterDto entity)
        {          
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Chapter() { ImageUrl = entity.ImageUrl, Description = entity.Description, Publish = entity.Publish, SectionId = entity.SectionId, SubSectionId = entity.SubSectionId, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.Chapters.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }          
        }

        public bool Update(ChapterDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Chapters.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.Description = entity.Description;
                    objToUpdate.Publish = entity.Publish;
                    objToUpdate.SubSectionId = entity.SubSectionId;
                    objToUpdate.SectionId = entity.SectionId;                 
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

        public List<ChapterDto> GetAll()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Chapters
                                 join s in context.Commons on o.SubSectionId equals s.ID
                                 join e in context.Settings on o.SectionId equals e.ID
                                 orderby o.Id descending
                                 select new ChapterDto { ImageUrl = o.ImageUrl, SectionId = o.SectionId, Section = e.Title, SubSection = s.Title, Publish = o.Publish, SubSectionId = o.SubSectionId, Description = o.Description, CreatedBy = o.CreatedBy, CreatedDT= o.CreatedDt, Id = o.Id }).ToList();
                return objResult;
            }                           
        }      

        public List<ChapterDto> GetAll(int Id)
        {           
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Chapters
                                 join s in context.Commons on o.SubSectionId equals s.ID
                                 join e in context.Settings on o.SectionId equals e.ID
                                 where o.SectionId == Id
                                 orderby o.Id ascending
                                 select new ChapterDto { ImageUrl = o.ImageUrl, SectionId = o.SectionId, Section = e.Title, SubSection = s.Title, Publish = o.Publish, SubSectionId = o.SubSectionId, Description = o.Description, CreatedBy = o.CreatedBy, CreatedDT= o.CreatedDt, Id = o.Id }).ToList();
                return objResult;
            }
        }       

    }
}

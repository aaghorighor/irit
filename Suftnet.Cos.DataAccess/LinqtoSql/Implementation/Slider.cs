namespace Suftnet.Cos.DataAccess
{  
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Slider : ISlider
    {        
        public List<SliderDto> GetAll()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Sliders
                                 join s in context.Commons on o.SliderTypeId equals s.ID
                                 orderby o.Id descending 
                                 select new SliderDto { SliderType = s.Title, SliderTypeId = o.SliderTypeId, CreatedDT= o.CreatedDt, Publish = o.Publish, Description = o.Description, ImageUrl = o.ImageUrl, Title = o.Title, Id = o.Id }).ToList();
              
                return objResult;    
             }
        }

        public IEnumerable<SliderDto> LoadSlides()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Sliders                                                                                                  
                                 where o.Publish == true
                                 orderby o.Id descending
                                 select new SliderDto { SliderTypeId = o.SliderTypeId, CreatedDT= o.CreatedDt, Publish = o.Publish, Description = o.Description, ImageUrl = o.ImageUrl, Title = o.Title, Id = o.Id }).ToList();

                return objResult;
            }
        }

        public List<SliderDto> GetAll(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Sliders
                                 join s in context.Commons on o.SliderTypeId equals s.ID
                                 where o.Publish == true 
                                 orderby o.Id descending
                                 select new SliderDto { SliderType = s.Title, SliderTypeId = o.SliderTypeId, CreatedDT= o.CreatedDt, Publish = o.Publish, Description = o.Description, ImageUrl = o.ImageUrl, Title = o.Title, Id = o.Id }).ToList();

                return  objResult;
            }
        }

        public SliderDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Sliders
                                 join s in context.Commons on o.SliderTypeId equals s.ID
                                 where o.Id == Id
                                 orderby o.Id descending
                                 select new SliderDto { SliderType = s.Title, SliderTypeId = o.SliderTypeId, CreatedDT= o.CreatedDt, Publish = o.Publish, Description = o.Description, ImageUrl = o.ImageUrl, Title = o.Title, Id = o.Id }).FirstOrDefault();

                return objResult;
            }
        }     
               
        public int Insert(SliderDto entity)
        {
            int id = 0;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Slider() { SliderTypeId = entity.SliderTypeId, Alt = entity.Title, ImageUrl = entity.ImageUrl, Description = entity.Description, Title = entity.Title, Publish = entity.Publish, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };

                context.Sliders.Add(obj);
                context.SaveChanges();
                id = obj.Id;
            }
            return id;
        }
       
        public bool Update(SliderDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Sliders.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.Publish = entity.Publish;
                    objToUpdate.Description = entity.Description;
                    objToUpdate.Title = entity.Title;
                    objToUpdate.Alt = entity.Alt;
                    objToUpdate.ImageUrl = entity.ImageUrl;
                    objToUpdate.SliderTypeId = entity.SliderTypeId;

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
                var objToDelete = context.Sliders.SingleOrDefault(o => o.Id == id);
                if (objToDelete != null)
                {
                    context.Sliders.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;     
        }            
     
    }
}

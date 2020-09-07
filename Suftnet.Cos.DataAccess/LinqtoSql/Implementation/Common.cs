namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

   public class Common :ICommon
    {     
        public bool Delete(int id)
        {
            bool response = false;    
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Commons.FirstOrDefault(o => o.ID == id);

                if (objToDelete != null)
                {
                    context.Commons.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response; // return status          
        }               

        public List<CommonDto> GetAll()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Commons                            
                              orderby o.Title
                              select new CommonDto { ImageUrl = o.ImageUrl, Active = o.Active, code = o.code, Description = o.Description, Id = o.ID, Indexno = o.Indexno, SettingId = o.Settingid, Title = o.Title }).ToList();
                return result;
            }
        }       
       
        public int Insert(CommonDto entity)
        {
            Int32 id = 0;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Common() { ImageUrl = entity.ImageUrl, Description = entity.Description, Active = entity.Active, Settingid = entity.SettingId, code = entity.code, Indexno = entity.Indexno, Title = entity.Title, CreatedBy = entity.CreatedBy, CreatedDT = entity.CreatedDT };
                context.Commons.Add(obj);
                context.SaveChanges();
                id = obj.ID;
            }
            return id;
        }

        public bool Update(CommonDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Commons.SingleOrDefault(o => o.ID == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.Active = entity.Active;
                    objToUpdate.Title = entity.Title;
                    objToUpdate.Indexno = entity.Indexno;
                    objToUpdate.code = entity.code;
                    objToUpdate.ImageUrl = entity.ImageUrl;
                    objToUpdate.Description = entity.Description;

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

        public CommonDto Get(int id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Commons
                              where o.ID == id
                              orderby o.Title
                              select new CommonDto { ImageUrl = o.ImageUrl, Active = o.Active, code = o.code, Description = o.Description, Id = o.ID, Indexno = o.Indexno, SettingId = o.Settingid, Title = o.Title }).FirstOrDefault();
                return result;
            }
        }

        public List<CommonDto> GetNotSystem(int classId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Commons
                              join x in context.Settings on o.Settingid equals x.ID
                              where x.ClassId == classId
                              orderby o.ID ascending
                              select new CommonDto { ImageUrl = o.ImageUrl, Active = o.Active, code = o.code, Description = o.Description, Id = o.ID, Indexno = o.Indexno, SettingId = o.Settingid, Title = o.Title }).ToList();
                return result;
            }
        }

        public List<CommonDto> GetAll(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Commons
                              where o.Settingid == Id
                              orderby o.Indexno  ascending
                              select new CommonDto { ImageUrl = o.ImageUrl, Active = o.Active, code = o.code, Description = o.Description, Id = o.ID, Indexno = o.Indexno, SettingId = o.Settingid, Title = o.Title }).ToList();
                return result;
            }
        }

        public List<CommonDto> Load(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Commons
                              where o.Settingid == Id && o.Active == true
                              orderby o.Indexno ascending
                              select new CommonDto { ImageUrl = o.ImageUrl, Active = o.Active, code = o.code, Description = o.Description, Id = o.ID, Indexno = o.Indexno, SettingId = o.Settingid, Title = o.Title }).ToList();
                return result;
            }
        }

        public List<CommonDto> GetSupportType(int Id, string supportTypeCode)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Commons
                              where o.Settingid == Id && o.code == supportTypeCode
                              orderby o.Indexno ascending
                              select new CommonDto { ImageUrl = o.ImageUrl, Active = o.Active, code = o.code, Description = o.Description, Id = o.ID, Indexno = o.Indexno, SettingId = o.Settingid, Title = o.Title }).ToList();
                return result;
            }
        }

        public List<CommonDto> GetTenantRoles(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Commons
                              where o.Settingid == Id && o.code == "0"
                              orderby o.Title
                              select new CommonDto { ImageUrl = o.ImageUrl, Active = o.Active, code = o.code, Description = o.Description, Id = o.ID, Indexno = o.Indexno, SettingId = o.Settingid, Title = o.Title }).ToList();
                return result;
            }
        }

    }
}

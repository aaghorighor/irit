namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Editor : IEditor
    {      
        public bool Delete(int id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Editors.FirstOrDefault(o => o.ID == id);
                if (objToDelete != null)
                {
                    context.Editors.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;         
        }

        public List<EditorDTO> GetAll()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Editors
                                 join c in context.Commons on o.ContentTypeid equals c.ID                                
                                 orderby c.Title
                                 select new EditorDTO { CreatedDT= o.CreatedDT, Active = o.Active, Contents = o.Contents, Id = o.ID, ContentType = c.Title, ContentTypeid = o.ContentTypeid, ImageUrl = o.ImageUrl, Title = o.Title }).ToList();
                return objResult;
            }
        }

        public List<EditorDTO> GetAll(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Editors
                                 join c in context.Commons on o.ContentTypeid equals c.ID
                                 where o.ContentTypeid == Id && o.TenantId == Id
                                 orderby c.Title
                                 select new EditorDTO { CreatedDT= o.CreatedDT, Active = o.Active, Contents = o.Contents, Id = o.ID, ContentType = c.Title, ContentTypeid = o.ContentTypeid, ImageUrl = o.ImageUrl, Title = o.Title }).ToList();
                return objResult;
            }
        }

        public List<EditorDTO> GetAll(int Id, int tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Editors
                                 join c in context.Commons on o.ContentTypeid equals c.ID
                                 where o.ContentTypeid == Id && o.TenantId == tenantId
                                 orderby c.Title
                                 select new EditorDTO { CreatedDT= o.CreatedDT, Active = o.Active, Contents = o.Contents, Id = o.ID, ContentType = c.Title, ContentTypeid = o.ContentTypeid, ImageUrl = o.ImageUrl, Title = o.Title }).ToList();
                return objResult;
            }
        }

        public EditorDTO Get(int id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Editors
                                 join c in context.Commons on o.ContentTypeid equals c.ID
                                 where o.ID == id
                                 orderby c.Title
                                 select new EditorDTO { CreatedDT= o.CreatedDT, Active = o.Active, Contents = o.Contents, Id = o.ID, ContentType = c.Title, ContentTypeid = o.ContentTypeid, ImageUrl = o.ImageUrl, Title = o.Title }).FirstOrDefault();
                return objResult;
            }
        }

        public int Insert(EditorDTO entity)
        {
            Int32 id = 0;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Editor() { Contents = entity.Contents, Active = entity.Active, ImageUrl = entity.ImageUrl, ContentTypeid = entity.ContentTypeid, Title = entity.Title, CreatedBy = entity.CreatedBy, CreatedDT = entity.CreatedDT };
                context.Editors.Add(obj);
                context.SaveChanges();
                id = obj.ID;
            }
            return id;
        }

        public bool Update(EditorDTO entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Editors.SingleOrDefault(o => o.ID == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.Active = entity.Active;
                    objToUpdate.Title = entity.Title;
                    objToUpdate.ContentTypeid = entity.ContentTypeid;
                    objToUpdate.Contents = entity.Contents;
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
    }
}

namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System.Collections.Generic;
    using System.Linq;

    public class LogViewer : ILogViewer
    {
        public LogDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.LogViewers                                                                
                                 where o.Id == Id
                                 select new LogDto { CreatedBy = o.CreatedBy, CreatedDt= o.CreatedDt, Description = o.Description, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Delete(int Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.LogViewers.FirstOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.LogViewers.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;
        }

        public int Insert(LogDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.LogViewer() { Description = entity.Description, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDt };
                context.LogViewers.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }
     

        public List<LogDto> GetAll(int iskip, int itake, string isearch)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                context.Database.CommandTimeout = 30 * 60;

                var objResult = (from o in context.LogViewers
                                 where o.Description.Contains(isearch)
                                 orderby o.Id descending
                                 select new LogDto { CreatedBy = o.CreatedBy, CreatedDt = o.CreatedDt, Description = o.Description, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }

        public List<LogDto> GetAll(int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.LogViewers
                                 orderby o.Id descending
                                 select new LogDto { CreatedBy = o.CreatedBy, CreatedDt = o.CreatedDt, Description = o.Description, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }

        public int Count()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.LogViewers
                                 select o).Count();
                return objResult;
            }
        }
    }
}

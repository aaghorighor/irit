namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System.Collections.Generic;
    using System.Linq;

    public class Logger : ILogViewer
    {
        public LogDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Loggers                                                                
                                 where o.Id == Id
                                 select new LogDto { CreatedBy = o.CreatedBy, CreatedDt= o.CreatedDT, Description = o.Description, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Delete(int Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Loggers.FirstOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Loggers.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;
        }

        public bool Delete()
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var rowDeleted = context.Database.ExecuteSqlCommand("delete from logger");
                response = rowDeleted == 0 ? false : true;
            }
            return response;
        }

        public int Insert(LogDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Logger() { Description = entity.Description, CreatedBy = entity.CreatedBy, CreatedDT = entity.CreatedDt };
                context.Loggers.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }
     

        public LogAdapter GetAll(int iskip, int itake, string isearch)
        {

            if(!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var obj = (from o in context.Loggers
                                     where o.Description.Contains(isearch)
                                     orderby o.Id descending
                                     select new LogDto {  CreatedBy = o.CreatedBy, CreatedDt = o.CreatedDT, Description = o.Description, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                    return new LogAdapter { Count = Count(), Logs = obj };
                }
            }else
            {
                return GetAll(iskip, itake);
            }
           
        }

        public LogAdapter GetAll(int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.Loggers
                                 orderby o.Id descending
                                 select new LogDto { CreatedBy = o.CreatedBy, CreatedDt = o.CreatedDT, Description = o.Description, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return new LogAdapter { Count = Count(), Logs = obj };
            }
        }

        public int Count()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Loggers
                                 select o).Count();
                return objResult;
            }
        }
    }
}

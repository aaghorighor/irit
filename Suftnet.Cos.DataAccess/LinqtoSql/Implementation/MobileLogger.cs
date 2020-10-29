namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System.Collections.Generic;
    using System.Linq;

    public class MobileLogger : IMobileLogger
    {
        public MobileLogDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.MobileLoggers                                                                
                                 where o.Id == Id
                                 select new MobileLogDto { CreatedBy = o.CreatedBy, CreatedDt= o.CreatedDT, PACKAGE_NAME = o.PACKAGE_NAME, BUILD = o.BUILD, STACK_TRACE = o.STACK_TRACE, ANDROID_VERSION = o.ANDROID_VERSION, APP_VERSION_CODE = o.APP_VERSION_CODE, AVAILABLE_MEM_SIZE = o.AVAILABLE_MEM_SIZE, CRASH_CONFIGURATION = o.CRASH_CONFIGURATION, REPORT_ID = o.REPORT_ID, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Delete(int Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.MobileLoggers.FirstOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.MobileLoggers.Remove(objToDelete);
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
                var rowDeleted = context.Database.ExecuteSqlCommand("delete from mobileLogger");
                response = rowDeleted == 0 ? false : true;
            }
            return response;
        }

        public int Insert(MobileLogDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.MobileLogger() { REPORT_ID = entity.REPORT_ID, PACKAGE_NAME = entity.PACKAGE_NAME, BUILD = entity.BUILD, STACK_TRACE = entity.STACK_TRACE, ANDROID_VERSION = entity.ANDROID_VERSION, APP_VERSION_CODE = entity.APP_VERSION_CODE, AVAILABLE_MEM_SIZE = entity.AVAILABLE_MEM_SIZE, CRASH_CONFIGURATION = entity.CRASH_CONFIGURATION, CreatedBy = entity.CreatedBy, CreatedDT = entity.CreatedDt };
                context.MobileLoggers.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }     

        public MobileLoggerAdapter GetAll(int iskip, int itake, string isearch)
        {
            if(!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var obj = (from o in context.MobileLoggers
                                     where o.PACKAGE_NAME.Contains(isearch) || o.ANDROID_VERSION.Contains(isearch) || o.STACK_TRACE.Contains(isearch) || o.BUILD.Contains(isearch) || o.CRASH_CONFIGURATION.Contains(isearch)
                                     orderby o.Id descending
                                     select new MobileLogDto { CreatedBy = o.CreatedBy, CreatedDt = o.CreatedDT, PACKAGE_NAME = o.PACKAGE_NAME, BUILD = o.BUILD, STACK_TRACE = o.STACK_TRACE, ANDROID_VERSION = o.ANDROID_VERSION, APP_VERSION_CODE = o.APP_VERSION_CODE, AVAILABLE_MEM_SIZE = o.AVAILABLE_MEM_SIZE, CRASH_CONFIGURATION = o.CRASH_CONFIGURATION, REPORT_ID = o.REPORT_ID, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                    return new MobileLoggerAdapter { Count = Count(), MobileLogs = obj };
                }
            }else
            {
                return GetAll(iskip, itake);
            }            
        }

        public MobileLoggerAdapter GetAll(int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.MobileLoggers
                                 orderby o.Id descending
                                 select new MobileLogDto { CreatedBy = o.CreatedBy, CreatedDt = o.CreatedDT, PACKAGE_NAME = o.PACKAGE_NAME, BUILD = o.BUILD, STACK_TRACE = o.STACK_TRACE, ANDROID_VERSION = o.ANDROID_VERSION, APP_VERSION_CODE = o.APP_VERSION_CODE, AVAILABLE_MEM_SIZE = o.AVAILABLE_MEM_SIZE, CRASH_CONFIGURATION = o.CRASH_CONFIGURATION, REPORT_ID = o.REPORT_ID, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return new MobileLoggerAdapter { Count = Count(), MobileLogs = obj };
            }
        }

        public int Count()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.MobileLoggers
                                 select o).Count();
                return objResult;
            }
        }
    }
}

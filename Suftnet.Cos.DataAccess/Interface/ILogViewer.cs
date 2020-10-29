namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;

    public interface ILogViewer
    {
        LogDto Get(int id);
        int Insert(LogDto entity);
        bool Delete(int id);
        LogAdapter GetAll(int iskip, int itake, string isearch);
        LogAdapter GetAll(int iskip, int itake);
        int Count();
        bool Delete();
    }

    public interface IMobileLogger
    {
        MobileLogDto Get(int id);
        int Insert(MobileLogDto entity);
        bool Delete(int id);
        MobileLoggerAdapter GetAll(int iskip, int itake, string isearch);
        MobileLoggerAdapter GetAll(int iskip, int itake);
        int Count();
        bool Delete();
    }
}

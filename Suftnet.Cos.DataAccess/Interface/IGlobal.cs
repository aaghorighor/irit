namespace Suftnet.Cos.DataAccess
{
    using System;
    public interface IGlobal 
    {      
        GlobalDto  Get();
       
        int Insert(GlobalDto entity);

        bool Update(GlobalDto entity);
    }
}

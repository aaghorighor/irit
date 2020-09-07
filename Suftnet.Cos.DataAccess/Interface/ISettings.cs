namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface ISettings 
    {
        Int32 Insert(Action.Setting entity);
        bool Update(Action.Setting entity);
        bool Delete(int id);
        IEnumerable<SettingsDTO> GetAllByID(int id);
        IEnumerable<SettingsDTO> GetAll();
        IEnumerable<SettingsDTO> GetAll(int classId);
        SettingsDTO Get(int id);    
    }
}

namespace Suftnet.Cos.DataAccess
{
   using System;
   using System.Collections.Generic;

   public interface ICommon : IRepository<CommonDto>
    {
        List<CommonDto> GetTenantRoles(int Id);
        List<CommonDto> GetSupportType(int Id, string supportTypeCode);
        List<CommonDto> GetNotSystem(int classId);
        List<CommonDto> Load(int Id);
    }
}

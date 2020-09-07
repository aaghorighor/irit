namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using System;

    public class TenantCommonRepository : ITenantCommonRepository
    {
        private readonly ICommon _commond;
        private readonly ITenantCommon _tenantCommond;       

        public TenantCommonRepository(ICommon commond, ITenantCommon tenantCommond)
        {
            _tenantCommond = tenantCommond;
            _commond = commond;           
        }

        public void Add(int commonId)
        {
            var common = _commond.Get(commonId);
            var tenantCommon = _tenantCommond.GetById(commonId);

            foreach (var item in tenantCommon)
            {
                item.Active = common.Active;
                item.Code = common.code;           
                item.Description = common.Description;
                item.Id = common.Id;
                item.Indexno = common.Indexno;
                item.SettingId = common.SettingId;
                item.Title = common.Title;
                item.CreatedBy = Environment.UserDomainName;
                item.CreatedDT = DateTime.Now;

                _tenantCommond.Insert(item);
            }
        }       
        
        public void Update(int commonId)
        {
            var common = _commond.Get(commonId);
            var tenantCommon = _tenantCommond.GetById(commonId);

            foreach (var item in tenantCommon)
            {
                item.Active = common.Active;
                item.Code = common.code;       
                item.Description = common.Description;
                item.Id = common.Id;
                item.Indexno = common.Indexno;
                item.SettingId = common.SettingId;            
                item.Title = common.Title;
                item.CreatedBy = Environment.UserDomainName;
                item.CreatedDT = DateTime.Now;

               _tenantCommond.Update(item);
            }        
        }       
    }
}

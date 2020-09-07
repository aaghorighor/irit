namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
       
    using Common;

    public class CreateCommonCommand : ICommand
    {       
        private readonly ITenantCommon _tenantCommon;
        private readonly ICommon _common;
        public CreateCommonCommand(ITenantCommon tenantCommon, ICommon common)
        {
            _tenantCommon = tenantCommon;
            _common = common;           
        }

        public int TenantId { get; set; }
        public string CreatedBy { get; set; }

        public void Execute()
        {
            this.PrepareTenantCommon();
        }

        #region private function
        private void PrepareTenantCommon()
        {
            var items = _common.GetNotSystem((int)eClass.Application); 

            foreach(var item in items)
            {
                var tenantCommon = new TenantCommonDto
                {
                    Active = item.Active,
                    Code = item.code,
                    SettingId = item.SettingId,
                    Description = item.Description,
                    Indexno = item.Indexno,
                    Title = item.Title,
                    TenantId = TenantId,

                    CreatedBy = this.CreatedBy,
                    CreatedDT = DateTime.UtcNow                   
                };

                _tenantCommon.Insert(tenantCommon);
            }            
        }
        #endregion

    }
}
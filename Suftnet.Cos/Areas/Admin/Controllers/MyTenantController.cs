namespace Suftnet.Cos.Admin.Controllers
{
    using Common;
    using Core;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Web.Services.Interface;
    using Web.ViewModel;

    public class MyTenantController : AdminBaseController
    {
        #region Resolving dependencies        
  
        private readonly ITenant _tenant;
        private readonly ITenantAddress _address;

        public MyTenantController(ITenant tenant, ITenantAddress address)
        {
            _tenant = tenant;
            _address = address;
        }

        #endregion
           
        public async Task<ActionResult> Entry(string tenantId)
        {
            var model = await Task.Run(() => _tenant.Get(new Guid(tenantId)));
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonResult Create(TenantModel entityToCreate)
        {
            try
            {                  
                entityToCreate.CreatedBy = this.UserName;
                entityToCreate.CreatedDT = DateTime.Now;

                _address.UpdateByAddressId(entityToCreate);                
                _tenant.Update(Web.Mapper.Map.From(entityToCreate));

                entityToCreate.flag = (int)flag.Update;

                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

    }
}


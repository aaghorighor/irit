namespace Suftnet.Cos.Admin.Controllers
{
    using System;
    using System.Web.Mvc;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Common;
    using Core;
  
    public class SettingsController : AdminBaseController
    {
        #region Resolving dependencies        
  
        private readonly IGlobal _global;
        private readonly ICommon _common;
        private readonly IAddress _address;

        public SettingsController(IAddress address, IGlobal global, ICommon common)
        {
            _global = global;
            _common = common;
            _address = address;
        }

        #endregion
           
        public ActionResult Index()
        {          
            return View(_global.Get());
        }
       
        public JsonResult Create(GlobalDto entityToCreate)
        {                   
            try
            {
                    entityToCreate.CreatedBy = this.UserName;
                    entityToCreate.CreatedDT = DateTime.Now;

                if (entityToCreate.Id == 0)
                {
                    entityToCreate.AddressId = _address.Insert(entityToCreate);
                    _global.Insert(entityToCreate);                   
                    entityToCreate.flag = (int)flag.Add;
                }
                else
                {
                    _address.UpdateByAddressId(entityToCreate);
                    _global.Update(entityToCreate);
                    entityToCreate.flag = (int)flag.Update;
                }

                GeneralConfiguration.Configuration.Settings.General = entityToCreate;

                return Json(new { ok = true, flag = entityToCreate.flag, objrow = _global.Get() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }      

    }
}


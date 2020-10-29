namespace Suftnet.Cos.Admin.Controllers
{  
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Service;
    using Suftnet.Cos.Web;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using Suftnet.Cos.Web.Mapper;

    public class TenantsController : AdminBaseController
    {
        #region Resolving dependencies        
  
        private readonly ITenant _tenant;
        private readonly ITenantAddress _address;
        public TenantsController(ITenant tenant, ITenantAddress address)
        {
            _tenant = tenant;
            _address = address;
        }

        #endregion
           
        public ActionResult Index()
        {            
            return View();
        }

        [OutputCache(Duration = 0, VaryByParam = "*")]
        public ActionResult Entry(string name, string queryString)
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Fetch(string queryString)
        {
            Ensure.Argument.NotNull(queryString);

            var model = await Task.Run(() => _tenant.Get(new Guid(queryString)));
            return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Fetch(DataTableAjaxPostModel param)
        {
            var model = await Task.Run(()=> _tenant.GetAll(param.start, param.length, param.search.value));

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.TenantDto.Count,
                recordsFiltered = model.Count,
                data = model.TenantDto
            },
                      JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]       
        [ValidateAntiForgeryToken]
        public JsonResult Edit(TenantModel entityToCreate)
        {
            Ensure.Argument.NotNull(entityToCreate);

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.AjaxErrors()
                });
            }

            entityToCreate.CreatedBy = this.UserName;
            entityToCreate.CreatedDT = DateTime.UtcNow;
                                
            _address.UpdateByAddressId(entityToCreate);
            _tenant.AdminUpdate(Map.From(entityToCreate));
                       
            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }
    }
}


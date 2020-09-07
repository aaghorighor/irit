namespace Suftnet.Cos.BackOffice
{
    using Service;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class TableController : BackOfficeBaseController
    {      
        private readonly ITable _table;

        public TableController(ITable table)
        {
            _table = table;
        }

        public async Task<ActionResult> Index()
        {
            var model = await System.Threading.Tasks.Task.Run(() => _table.GetAll());
            return View(model);           
        }


        [HttpGet]
        public async Task<JsonResult> Get(int Id)
        {
            try
            {
                Ensure.Argument.NotNull(Id);

                var model = await System.Threading.Tasks.Task.Run(() => _table.Get(Id));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonResult Create(TableDto entityToCreate)
        {
            try
            {
                Ensure.Argument.NotNull(entityToCreate);

                entityToCreate.CreatedDT = DateTime.UtcNow;
                entityToCreate.CreatedBy = this.UserName;
                entityToCreate.TenantId = this.TenantId;

                if (entityToCreate.Id == 0)
                {
                    entityToCreate.Id = _table.Insert(entityToCreate);
                    entityToCreate.flag = (int)flag.Add;
                }
                else
                {
                    _table.Update(entityToCreate);
                    entityToCreate.flag = (int)flag.Update;

                }

                return Json(new { ok = true, flag = entityToCreate.flag, objrow = _table.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public async Task<JsonResult> Delete(int Id)
        {
            try
            {
                Ensure.Argument.NotNull(Id);

                var model = await System.Threading.Tasks.Task.Run(() => _table.Delete(Id));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);               
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }
    }
}

    

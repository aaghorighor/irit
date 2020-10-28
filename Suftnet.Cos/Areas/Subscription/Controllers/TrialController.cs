namespace Suftnet.Cos.Subscription.Controllers
{
    using Common;
    using CommonController.Controllers;
    using Core;
    using Suftnet.Cos.DataAccess;
    using System.Web.Mvc;
    using Web;
    using Web.Command;
   
    [AuthorizeActionFilter(Constant.BackOfficeOnly)]
    public class TrialController : BaseController
    {    
        private readonly IFactoryCommand _factoryCommand;
        public TrialController(IFactoryCommand factoryCommand)
        {        
            _factoryCommand = factoryCommand;
        }

        [HttpGet]
        public ActionResult Cancel()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var adapter = new StripeAdapterModel();
            var model = tenant.Get(this.TenantId);
                      
            adapter.Tenant = model;
            return View(adapter);
        }

        [HttpPost]
        public ActionResult Cancel(string reason)
        {                     
            var command = _factoryCommand.Create<DeleteTenantCommand>();
            command.TenantId = this.TenantId;
            System.Threading.Tasks.Task.Run(() => command.Execute());

            return RedirectToAction("confirmation", "trial", new { area = "subscription" });
        }

        public ActionResult Confirmation()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var model = tenant.Get(this.TenantId);
            var adapter = new StripeAdapterModel();
                      
            adapter.Tenant = model;
            return View(adapter);
        }

        //[HttpGet]
        //public ActionResult Download()
        //{
        //    var command = _factoryCommand.Create<DownloadCommand>();
        //    command.TenantId = this.TenantId;
        //    command.Execute();
           
        //    return File(command.Content.ToArray(), "application/zip", this.UserName +".zip");                      
        //}      

    }
}
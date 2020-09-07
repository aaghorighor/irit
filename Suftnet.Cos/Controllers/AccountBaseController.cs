namespace Suftnet.Cos.Web
{
    using Command;
    using Core;
    using DataAccess;
    using Services.Interface;
    using Suftnet.Cos.Common;
    using System.Linq;
  
    using System.Web.Mvc;   
    using CommonController.Controllers;
    
    public class AccountBaseController : BaseController
    {      
        protected void Binder(string userId, int tenantId)
        {          
            var container = StructureMapConfig.GetConfiguredContainer(this.HttpContext);                  
        }      
    }
}

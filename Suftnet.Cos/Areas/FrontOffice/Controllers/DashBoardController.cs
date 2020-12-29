namespace Suftnet.Cos.FrontOffice
{
    using System.Web.Mvc;
   
    public class DashBoardController : FrontOfficeBaseController
    {     
        #region Resolving dependencies
              
        #endregion
        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Index()
        {           
            return View();         
        }
       
    }
}


namespace Suftnet.Cos.BackOffice
{
    using Suftnet.Cos.Service;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Web.Mvc;
    using Web.ActionFilter;

    public class PoleDisplayController : FrontOfficeBaseController
    {
        [ValidateInput(false)]
        //[ZeroCacheActionFilterAttribute]
        [HttpPost]
        public ActionResult Pending(string item, decimal amount, string ipAddress)
        {
            try
            {
                if(!string.IsNullOrEmpty(ipAddress))
                {
                    //BellaHotelclient(ipAddress).PoleDisplay(new Item { Amount = amount, Text = item });
                }            

                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
               return  Logger(ex);
            }           
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Complete(string item, decimal amount, string ipAddress)
        {
            try
            {
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    //BellaHotelclient(ipAddress).PoleDisplayTotal(new Item { Amount = amount, Text = item });
                }

                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }      
        }

        [HttpPost]
        public ActionResult Clear(string ipAddress)
        {
            try
            {
                if (!string.IsNullOrEmpty(ipAddress))
                {
                   // BellaHotelclient(ipAddress).ClearPoleDisplay();
                }              

                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        #region

        private void BellaHotelclient(string ipAddress)
        {
            //return new BellaHotelClient(new Uri("http://" + ipAddress), "000", "000");
        }

        #endregion
    }
}
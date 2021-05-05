namespace Suftnet.Cos.BackOffice
{   
    using Stimulsoft.Report;
    using Stimulsoft.Report.Mvc;
    using Suftnet.Cos.Common;  
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.Mvc;

    //[ControllerFilter(Views.Reports)]
    public class ReportController : BackOfficeBaseController
    {      
        #region Resolving dependencies

        private readonly IReport _report;
        private readonly IOrder _order;     
        private readonly IUser _userAccount;      
        private readonly ICategory _category;
        private readonly IMenu _menu;

        public ReportController(
            IUser userAccount, ICategory category,
            IReport report, IOrder order, IMenu menu)
        {
            _report = report;
            _order = order;         
            _userAccount = userAccount;        
            _category = category;
            _menu = menu;
        }

        #endregion

        public ActionResult Index(TermDto termdto)
        {
            TempData["term"] = termdto;
            return View();
        }

        public ActionResult GetSalesReport(TermDto termdto)
        {
            StiReport report = new StiReport();        

            if (TempData["term"] != null)
            {
                var term = TempData["term"] as TermDto;
                term.TenantId = this.TenantId;           
                var settings = new TenantDto { CurrencySymbol= term.Currency, Name = this.TenantName, Email = this.TenantEmail, Mobile = this.TenantMobile, CompleteAddress = this.TenantAddress };

                switch (term.ReportTypeId)
                {                 
                    case ReportType.Menu:

                        report.Load(Server.MapPath("~/Content/Reports/MenuItem.mrt"));

                       var menu = new List<MenuDto>();
                       var menuDataSets = new DataSet();
                       var menuCategoryWrapper = new MenuCategoryWrapper();

                        menu = _menu.GetAll(this.TenantId);

                       if (menu.Count > 0)
                       {
                           report.Dictionary.Clear();

                            menuCategoryWrapper.Menu = menu;
                            menuCategoryWrapper.Category = _category.GetAll(this.TenantId);

                           string xmlString = Util.Serialize<MenuCategoryWrapper>(menuCategoryWrapper);
                           System.Xml.XmlTextReader xmlTextReader = new System.Xml.XmlTextReader(new System.IO.StringReader(xmlString));
                           xmlTextReader.Read();
                           menuDataSets.ReadXml(xmlTextReader, System.Data.XmlReadMode.InferSchema);

                            //productDs.WriteXmlSchema(@"c:\menu.sxd");
                            //productDs.WriteXml(@"c:\menu.xml");

                           report = new Reports.Suftnet_Menu();
                           report.RegData(menuDataSets);
                           report.Dictionary.Synchronize();

                           //// bind setting sections to gobal setting object
                           report.RegBusinessObject("Settings", "Setting", settings);
                           report.RegBusinessObject("Terms", "Term", term);
                       }
                       else {

                           report.Load(Server.MapPath("~/Content/Reports/Blank.mrt"));                       
                       }

                       break;                   

                    case ReportType.Delivery:

                        report.Load(Server.MapPath("~/Content/Reports/DeliveryOrder.mrt"));
                       var readyDelivery = _order.FetchDeliveryByStatus(this.TenantId, new Guid(eOrderStatus.Ready));

                       if (readyDelivery != null)
                       {
                           report.RegBusinessObject("Settings", "Setting", settings);
                           report.RegBusinessObject("Orders", "Order", readyDelivery);                         
                       }
                       else
                       {
                           report.Load(Server.MapPath("~/Content/Reports/Blank.mrt"));
                       }

                       break;                                  

                    case ReportType.Order:

                        report.Load(Server.MapPath("~/Content/Reports/DineInOrder.mrt"));

                        term.OrderTypeId = new Guid(eOrderType.DineIn);

                        term.StartDt = term.StartDate.ToShortDateString();
                        term.EndDt = term.EndDate.ToShortDateString();
                        term.TenantId = this.TenantId;

                        var orders = _report.GetOrders(term);                      

                        if (orders != null)
                        {
                            report.RegBusinessObject("Settings", "Setting", settings);
                            report.RegBusinessObject("Terms", "Term", term);
                            report.RegBusinessObject("Orders", "Order", orders);
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/Blank.mrt"));
                        }

                        break;

                    case ReportType.Reservation:

                        report.Load(Server.MapPath("~/Content/Reports/ReservationOrder.mrt"));

                        term.OrderTypeId = new Guid(eOrderType.Reservation);
                    
                        var reservationOrder = _report.GetReservationOrders(term);             

                        if (reservationOrder != null)
                        {
                            report.RegBusinessObject("Settings", "Setting", settings);
                            report.RegBusinessObject("Terms", "Term", term);
                            report.RegBusinessObject("Orders", "Order", reservationOrder);
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/Blank.mrt"));
                        }

                        break;

                    case ReportType.BestSeller:

                        report.Load(Server.MapPath("~/Content/Reports/BestSeller.mrt"));

                        var bestSeller = _report.GetBestSellers(term);

                        term.StartDt = term.StartDate.ToShortDateString();
                        term.EndDt = term.EndDate.ToShortDateString();
                     
                        if (bestSeller != null)
                        {
                            report.RegBusinessObject("Settings", "Setting", settings);
                            report.RegBusinessObject("Terms", "Term", term);
                            report.RegBusinessObject("BestSellers", "BestSeller", bestSeller);
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/Blank.mrt"));
                        }

                        break;

                    case ReportType.Payment:

                        report.Load(Server.MapPath("~/Content/Reports/Payment.mrt"));

                        var payments = _report.GetPaymentByDates(term);

                        term.StartDt = term.StartDate.ToShortDateString();
                        term.EndDt = term.EndDate.ToShortDateString();

                        if (payments != null)
                        {
                            report.RegBusinessObject("Settings", "Setting", settings);
                            report.RegBusinessObject("Terms", "Term", term);
                            report.RegBusinessObject("Payments", "Payment", payments);
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/Blank.mrt"));
                        }

                        break;
                }
            }

            return StiMvcViewer.GetReportResult(report);
        }
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
    }

}


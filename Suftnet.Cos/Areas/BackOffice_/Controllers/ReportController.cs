namespace Suftnet.Cos.BackOffice
{
    using Cos.CommonController.Controllers;
    using Stimulsoft.Report;
    using Stimulsoft.Report.Mvc;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.Mvc;

    //[ControllerFilter(Views.Reports)]
    public class ReportController : BackOfficeBaseController
    {      
        #region Resolving dependencies

        private readonly IReport _report;
        private readonly IOrder _order;
        private readonly lOrderDetail _orderDetail;
        private readonly IUserAccount _userAccount;
        private readonly ICommon _common;
        private readonly ICategory _category;

        public ReportController(ICommon common,
            IUserAccount userAccount, ICategory category,
            IReport report, IOrder order, 
            lOrderDetail orderDetail)
        {
            _report = report;
            _order = order;
            _orderDetail = orderDetail;
            _userAccount = userAccount;
            _common = common;
            _category = category;
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
                GeneralConfiguration.Configuration.Settings.General.CurrencySymbol = term.Currency;

                switch (term.ReportTypeId)
                {
                    case (int)ReportType.Sales:

                        term.StatusId = (int)OrderStatus.Complete;

                        report.Load(Server.MapPath("~/Content/Reports/Sales.mrt"));
                        var repSales = new List<OrderDto>();

                        if (term.UserId > 0 )
                        {                            
                            var user = _userAccount.Get(term.UserId);
                            if (user != null)
                            {
                                term.Cashier = user.FirstName + " " + user.LastName;                               
                            }

                            term.UserName = this.UserName;

                            repSales = _report.GetSalesByUserName(term) as List<OrderDto>;
                        }
                        else
                        {
                            repSales = _report.GetSales(term) as List<OrderDto>;
                        }

                        if (repSales != null)
                        {
                            report.RegBusinessObject("Settings", "Setting", GeneralConfiguration.Configuration.Settings.General);
                            report.RegBusinessObject("Terms", "Term", term);
                            report.RegBusinessObject("SalesOrder", "SalesOrder", repSales);
                        }
                        else {

                            report.Load(Server.MapPath("~/Content/Reports/BlankReport.mrt"));   
                        }

                        break;

                    case (int)ReportType.Menu:

                        report.Load(Server.MapPath("~/Content/Reports/MenuItem.mrt"));

                       var menu = new List<MenuDto>();
                       var menuDataSets = new DataSet();
                       var menuCategoryWrapper = new MenuCategoryWrapper();

                        menu = _report.GetMenus() as List<MenuDto>;

                       if (menu.Count > 0)
                       {
                           report.Dictionary.Clear();

                            menuCategoryWrapper.Menu = menu;
                            menuCategoryWrapper.Category = _category.GetAll();

                           string xmlString = Extensions.Serialize<MenuCategoryWrapper>(menuCategoryWrapper);
                           System.Xml.XmlTextReader xmlTextReader = new System.Xml.XmlTextReader(new System.IO.StringReader(xmlString));
                           xmlTextReader.Read();
                           menuDataSets.ReadXml(xmlTextReader, System.Data.XmlReadMode.InferSchema);

                            //productDs.WriteXmlSchema(@"c:\menu.sxd");
                            //productDs.WriteXml(@"c:\menu.xml");

                           report = new Reports.Suftnet_Menu();
                           report.RegData(menuDataSets);
                           report.Dictionary.Synchronize();

                           //// bind setting sections to gobal setting object
                           report.RegBusinessObject("Settings", "Setting", GeneralConfiguration.Configuration.Settings.General);
                           report.RegBusinessObject("Terms", "Term", term);
                       }
                       else {

                           report.Load(Server.MapPath("~/Content/Reports/BlankReport.mrt"));                       
                       }

                       break;

                    case (int)ReportType.Invoice:

                        report.Load(Server.MapPath("~/Content/Reports/SalesOrderInvoice.mrt"));
                       var reportSalesOrder = _order.Get(term.OrderId);

                       if (reportSalesOrder != null)
                       {
                           report.RegBusinessObject("Settings", "Setting", GeneralConfiguration.Configuration.Settings.General);
                           report.RegBusinessObject("SalesOrder", "Order", reportSalesOrder);
                           report.RegBusinessObject("SalesOrder", "OrderDetails", _orderDetail.GetAll(reportSalesOrder.Id));
                       }
                       else
                       {
                           report.Load(Server.MapPath("~/Content/Reports/BlankReport.mrt"));
                       }

                       break;


                    case (int)ReportType.Delivery:

                        report.Load(Server.MapPath("~/Content/Reports/Delivery.mrt"));
                       var pendingDelivery = _order.GetAll((int)OrderStatus.Ready);

                       if (pendingDelivery != null)
                       {
                           report.RegBusinessObject("Settings", "Setting", GeneralConfiguration.Configuration.Settings.General);
                           report.RegBusinessObject("Orders", "Order", pendingDelivery);                         
                       }
                       else
                       {
                           report.Load(Server.MapPath("~/Content/Reports/BlankReport.mrt"));
                       }

                       break;                                  

                    case (int)ReportType.Order:

                        report.Load(Server.MapPath("~/Content/Reports/Order.mrt"));

                        var orders = _report.GetOrders(term);

                        term.StartDt = term.StartDate.ToShortDateString();
                        term.EndDt = term.EndDate.ToShortDateString();                      

                        if (orders != null)
                        {
                            report.RegBusinessObject("Settings", "Setting", GeneralConfiguration.Configuration.Settings.General);
                            report.RegBusinessObject("Terms", "Term", term);
                            report.RegBusinessObject("Orders", "Order", orders);
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/Blank.mrt"));
                        }

                        break;

                    case (int)ReportType.BestSeller:

                        report.Load(Server.MapPath("~/Content/Reports/BestSeller.mrt"));

                        var bestSeller = _report.GetBestSellers(term);

                        term.StartDt = term.StartDate.ToShortDateString();
                        term.EndDt = term.EndDate.ToShortDateString();

                        if (bestSeller != null)
                        {
                            report.RegBusinessObject("Settings", "Setting", GeneralConfiguration.Configuration.Settings.General);
                            report.RegBusinessObject("Terms", "Term", term);
                            report.RegBusinessObject("BestSellers", "BestSeller", bestSeller);
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


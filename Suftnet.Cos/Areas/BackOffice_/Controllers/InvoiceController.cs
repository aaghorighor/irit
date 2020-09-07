namespace Suftnet.Cos.BackOffice
{
    using Common;
    using Stimulsoft.Report;
    using Stimulsoft.Report.Mvc;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Web.Mvc;
    using System.Linq;

    public class InvoiceController : BackOfficeBaseController
    {      
        #region Resolving dependencies

        private readonly IReport _report;
        private readonly IOrder _order;
        private readonly lOrderDetail _orderDetail;    
        private readonly IMenu _menu;

        public InvoiceController(IReport report, IMenu menu, IOrder order,
            lOrderDetail orderDetail)
        {
            _menu = menu;
            _report = report;
            _order = order;
            _orderDetail = orderDetail;
        }

        #endregion

        public ActionResult Index(TermDto termdto)
        {
            TempData["term"] = termdto;
            return View();
        }

        public ActionResult GetSalesReport()
        {
            StiReport report = new StiReport();

            var global = GeneralConfiguration.Configuration.Settings.General;

            if (TempData["term"] != null)
            {
                var term = TempData["term"] as TermDto;
                global.CurrencySymbol = term.Currency;
                term.TenantId = this.TenantId;

                switch (term.ReportTypeId)
                {          
                    case 278: //// Sales order invoice

                        report.Load(Server.MapPath("~/Content/FrontOfficeReports/SalesOrderInvoice.mrt"));
                       var reportSalesOrder = _order.Get(term.OrderId);

                       if (reportSalesOrder != null)
                       {
                           report.RegBusinessObject("Settings", "Setting", global);
                           report.RegBusinessObject("SalesOrder", "Order", reportSalesOrder);
                           report.RegBusinessObject("SalesOrder", "OrderDetails", _orderDetail.GetAll(reportSalesOrder.Id));
                       }
                       else
                       {
                           report.Load(Server.MapPath("~/Content/FrontOfficeReports/Blank.mrt"));
                       }

                       break;

                    case (int)ReportType.OutOfStock:

                        report.Load(Server.MapPath("~/Content/Reports/OfsMenu.mrt"));                     
                 
                        var menus = _menu.CutOffCount();

                        if (menus > 0)
                        {
                            report.RegBusinessObject("Menus", "Menu", menus);
                            report.RegBusinessObject("Settings", "Setting", global);
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/BlankReport.mrt"));
                        }

                        break;

                    case (int)ReportType.Delivery:

                        report.Load(Server.MapPath("~/Content/Reports/OrderDelivery.mrt"));                      
                  
                        var salesOrder = _report.GetDelivery((int)OrderStatus.Ready, this.TenantId);
                        if (salesOrder.Count() > 0)
                        {
                            //// bind setting sections to gobal setting object
                            report.RegBusinessObject("Orders", "Order", salesOrder);
                            report.RegBusinessObject("Settings", "Setting", global);
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/BlankReport.mrt"));
                        }

                        break;

                }
            }

            return StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
        }

        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult(HttpContext);
        }

        public ActionResult PrintReport()
        {
            return StiMvcViewer.PrintReportResult(HttpContext);
        }

        public FileResult ExportReport()
        {
            return StiMvcViewer.ExportReportResult(HttpContext);
        }

        public ActionResult Interaction()
        {
            return StiMvcViewer.InteractionResult(HttpContext);
        }

        #region

        private static List<CategoryDto> GetProductCategories()
        {

            //ITable itable = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITable>();
            //var categories = itable.GetById(InvoiceController.t.);

            //var category = new List<CategoryDto>();

            //foreach (var item in categories)
            //{
            //    category.Add(new CategoryDto { Id = item.ID, Name = item.Title });
            //}

            //return category;

            return new List<CategoryDto>();
        }


        #endregion

    }

}


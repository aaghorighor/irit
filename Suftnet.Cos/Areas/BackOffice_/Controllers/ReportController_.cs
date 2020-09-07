namespace Suftnet.Cos.BackOffice
{
    using CommonController.Controllers;
    using Core;
    using Stimulsoft.Report;
    using Stimulsoft.Report.Mvc;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Command;
    using Web.Mapper;
    
    public class ReportController : BackOfficeBaseController
    {       
        #region Resolving dependencies

        private  IIncome _income;      
        private  IMember _member;
        private  IEvent _event;
        private  IAttendance _attendance;
        private  IAsset _asset;
        private  ISmallGroup _ministry;      
        private  ITenant _tenant;       
        private  ICommon _common;           
        private  IReport _report;   
        
        public ReportController()
        {
            ResolveDependencies();
        }

        #endregion

        public ActionResult Index(TermDto termdto)
       {
            TempData["term"] = termdto;
            return View();
       }

       public ActionResult GetReportSnapshot()
       {
            StiReport report = new StiReport();          

           if (TempData["term"] != null)
           {
               var term = TempData["term"] as TermDto;
               term.TenantId = this.TenantId;
               term.EndDate = term.FinishDate;

               switch (term.ReportTypeId)
               {                   
                   case (int)eReport.Member: //// Member    

                        report.Load(Server.MapPath("~/Content/Reports/Membership.mrt"));

                        var members = _report.MemberFilter(term);

                        if (members != null)
                        {
                            report.RegBusinessObject("Tenants", "Tenant", this.ToTenantSettings(term.CurrencySymbol));
                            report.RegBusinessObject("Term", "term", term);
                            report.RegBusinessObject("Member", "Member", members);
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/BlankReport.mrt"));
                        }

                        break;

                   case (int)eReport.Event: //// Event

                        report.Load(Server.MapPath("~/Content/Reports/EventCalender.mrt"));
                        var events = _report.EventFilter(term);

                        if (events != null)
                        {
                            report.RegBusinessObject("Tenants", "Tenant", this.ToTenantSettings(term.CurrencySymbol));
                            report.RegBusinessObject("Term", "term", term);
                            report.RegBusinessObject("Events", "Event", Map.From(events));
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/BlankReport.mrt"));
                        }

                        break;                                

                    case (int)eReport.Asset: //// Asset    

                       report.Load(Server.MapPath("~/Content/Reports/Asset.mrt"));
                       List<AssetDto> repAsset = new List<AssetDto>();
                       
                        if (term.AssetTypeId > 0)
                        {
                            repAsset = _asset.GetAll(term.AssetTypeId, term.TenantId) as List<AssetDto>;
                        }
                        else {
                            repAsset = _asset.GetAll(term.TenantId) as List<AssetDto>;
                        }
                     
                        if (repAsset != null)
                        {
                            report.RegBusinessObject("Tenants", "Tenant", this.ToTenantSettings(term.CurrencySymbol));
                            report.RegBusinessObject("Term", "term", term);
                            report.RegBusinessObject("Asset", "Asset", repAsset);
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/BlankReport.mrt"));    
                        }

                       break;

                   case (int)eReport.Attendance: //// Attendance   

                       report.Load(Server.MapPath("~/Content/Reports/Attendance.mrt"));
                        var repAttendance = _attendance.GetAttendance(term);

                        if (repAttendance != null)
                        {
                            report.RegBusinessObject("Tenants", "Tenant", this.ToTenantSettings(term.CurrencySymbol));
                            report.RegBusinessObject("Term", "term", term);
                            report.RegBusinessObject("Attendance", "Attendance", repAttendance);
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/BlankReport.mrt"));    
                        }

                        break;

                   case (int)eReport.Income: //// Income   

                        if(term.GiftAid)
                        {
                            report.Load(Server.MapPath("~/Content/Reports/GiftAid.mrt"));
                        }
                        else
                        {
                            report.Load(Server.MapPath("~/Content/Reports/Income.mrt"));
                        }

                        List<IncomeDto> repGive = new List<IncomeDto>();
                       
                        if (term.IncomeTypeId > 0)
                        {
                            repGive = term.GiftAid == true ? _income.GetByGiftAidAndTypeId(term) as List<IncomeDto> : _income.GetByTypeId(term) as List<IncomeDto>;
                        }
                        else {
                            repGive = term.GiftAid == true ? _income.GetByGiftAid(term) as List<IncomeDto> : _income.Get(term) as List<IncomeDto>;
                        }
                    
                        if (repGive != null)
                        {
                            report.RegBusinessObject("Tenants", "Tenant", this.ToTenantSettings(term.CurrencySymbol));
                            report.RegBusinessObject("Term", "term", term);
                            report.RegBusinessObject("Give", "Give", repGive);
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

        private void ResolveDependencies()
        {
            _income = GeneralConfiguration.Configuration.DependencyResolver.GetService<IIncome>();           
            _member = GeneralConfiguration.Configuration.DependencyResolver.GetService<IMember>();
            _event = GeneralConfiguration.Configuration.DependencyResolver.GetService<IEvent>();
            _attendance = GeneralConfiguration.Configuration.DependencyResolver.GetService<IAttendance>();
            _asset = GeneralConfiguration.Configuration.DependencyResolver.GetService<IAsset>();
            _ministry = GeneralConfiguration.Configuration.DependencyResolver.GetService<ISmallGroup>();
            _tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();           
            _common = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();               
            _report = GeneralConfiguration.Configuration.DependencyResolver.GetService<IReport>();
        }
      
    }        
    
}


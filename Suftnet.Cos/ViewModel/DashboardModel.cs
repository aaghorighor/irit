using Suftnet.Cos.DataAccess;
using System.Collections.Generic;

namespace Suftnet.Cos.Web.ViewModel
{
    public class DashboardModel
    {
        public int FreeTables { get; set; }
        public int DineIn { get; set; }
        public int PendingDeliveries { get; set; }
        public int Reservations { get; set; }
    }

    public class AdminDashboardModel
    {    
        public int Tenants { get; set; }
        public int Paid { get; set; }
        public int Expired { get; set; }
        public int Cancelled { get; set; }
        public int Mobile { get; set; } // logger
        public int Web { get; set; } // logger
        public int Trials { get; set; }
        public int Suspended { get; set; }
    }

    public class BoostrapModel
    {
        public List<MobileCategoryDto> Categories { get; set; }
        public List<MobileMenuDto> Menus { get; set; }
        public List<MobileAddonDto> Addons { get; set; }
        public List<MobileTaxDto> Taxes { get; set; }
        public List<MobileDiscountDto> Discounts { get; set; }
        public dynamic Outlet { get; set; }
    }

}
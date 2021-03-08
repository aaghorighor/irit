using Suftnet.Cos.DataAccess;
using System.Collections.Generic;

namespace Suftnet.Cos.Web.ViewModel
{
    public class DashboardModel
    {
        public int FreeTables { get; set; } = 0;
        public int DineIn { get; set; } = 0;
        public int PendingDeliveries { get; set; } = 0;
        public int Reservations { get; set; } = 0;
    }

    public class AdminDashboardModel
    {
        public int Tenants { get; set; } = 0;
        public int Paid { get; set; } = 0;
        public int Expired { get; set; } = 0;
        public int Cancelled { get; set; } = 0;
        public int Mobile { get; set; } = 0; // logger
        public int Web { get; set; } = 0; // logger
        public int Trials { get; set; } = 0;
        public int Suspended { get; set; } = 0;
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

    public class UserBoostrap
    {       
        public dynamic user { get; set; }     
    }

}
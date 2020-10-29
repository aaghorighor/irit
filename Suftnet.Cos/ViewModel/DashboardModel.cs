namespace Suftnet.Cos.Web.ViewModel
{
    public class DashboardModel
    {
        public int Members { get; set; }
        public int Events { get; set; }
        public int BirthDays { get; set; }
        public bool Expired { get; set; }
        public string PlanTypeId { get; set; }
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

}
namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Data.Entity;   
    using Microsoft.AspNet.Identity.EntityFramework;
    using Identity;

    public partial class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext()
            : base("name=DataContext")
        {           
            this.Configuration.LazyLoadingEnabled = false;          
        }

        public DataContext(string connectionString)
           : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Address> Addresses { get; set; }      
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }       
        public virtual DbSet<Common> Commons { get; set; }     
        public virtual DbSet<Device> Devices { get; set; }       
        public virtual DbSet<Editor> Editors { get; set; }       
        public virtual DbSet<Faq> Faqs { get; set; }       
        public virtual DbSet<Global> Globals { get; set; }      
        public virtual DbSet<LogViewer> LogViewers { get; set; }        
        public virtual DbSet<MobilePermission> MobilePermissions { get; set; }
        public virtual DbSet<MobileSlider> MobileSliders { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }     
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<PlanFeature> PlanFeatures { get; set; }       
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }        
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<TenantCommon> TenantCommons { get; set; }      
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Tutorial> Tutorials { get; set; }   
        public virtual DbSet<MemberAccount> MemberAccounts { get; set; }       
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<SubTopic> SubTopics { get; set; }
        public virtual DbSet<Addon> Addons { get; set; }
        public virtual DbSet<AddonType> AddonTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderPayment> OrderPayments { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {      
            base.OnModelCreating(modelBuilder);
        }
        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}

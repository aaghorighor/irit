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

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public virtual DbSet<CustomerDeliveryStatus> CustomerDeliveryStatuses { get; set; }
        public virtual DbSet<CustomerOrder> CustomerOrders { get; set; }
        public virtual DbSet<CustomerOrderDelivery> CustomerOrderDeliveries { get; set; }
        public virtual DbSet<CustomerOrderNotification> CustomerOrderNotifications { get; set; }
        public virtual DbSet<Addon> Addons { get; set; }
        public virtual DbSet<AddonType> AddonTypes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Common> Commons { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Editor> Editors { get; set; }
        public virtual DbSet<Faq> Faqs { get; set; }
        public virtual DbSet<Global> Globals { get; set; }
        public virtual DbSet<Logger> Loggers { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MobilePermission> MobilePermissions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderPayment> OrderPayments { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<PlanFeature> PlanFeatures { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<SubTopic> SubTopics { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public virtual DbSet<TenantState> TenantStates { get; set; }      
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<TenantAddress> TenantAddress { get; set; }
       // public virtual DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public virtual DbSet<MobileLogger> MobileLoggers { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {                 
                        
            modelBuilder.Entity<Tenant>()
             .HasRequired(e => e.TenantAddresses);         

            modelBuilder.Entity<Tenant>()
            .HasRequired(e => e.TenantStates);

            base.OnModelCreating(modelBuilder);
        }
        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}

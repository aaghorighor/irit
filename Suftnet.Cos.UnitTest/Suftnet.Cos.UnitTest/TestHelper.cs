namespace Suftnet.Cos.UnitTest
{
    using System;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Core;
    using System.Web.Mvc;
    using Microsoft.Practices.ServiceLocation;

    public static class TestHelper
    {
        public static UserAccountDto GetUserPrincipal()
        {
            var accountService = GeneralConfiguration.Configuration.DependencyResolver.GetService<IUserAccount>();
            var user = accountService.Get(1);
            return user;
        }       

        public static string GetRandomString(int length = 10)
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);
        }

        public static string GetFakeEmail()
        {
            return GetRandomString() + "@test.com";
        }

        public static DateTime GetDateTime(int day = 2)
        {
            return DateTime.Now.AddDays(day);
        }

        public static string GetRandomPhoneNumber(int count = 10)
        {
            var random = new Random();
            var result = "0";
            for (int i = 0; i < count - 1; i++)
            {
                var number = random.Next(9);
                result += number.ToString();
            }
            return result;
        }       

        public static int GetRandomNumber(int count = 3)
        {
            var random = new Random();
            var result = "0";
            for (int i = 0; i < count - 1; i++)
            {
                var number = random.Next(9);
                result += number.ToString();
            }
            return Convert.ToInt32(result);
        }

        public static SubscriptionDto RandomSubscription()
        {
            var subscription = new SubscriptionDto();
            subscription.ExpirationDate = GetDateTime(30);
            subscription.CreatedBy = "L-" + GetRandomString();
            subscription.CreatedDT = GetDateTime();
            subscription.PaymentDate = GetDateTime(2);
            subscription.StartDate = GetDateTime(2);
            subscription.StatusId = GetRandomNumber(3);
            subscription.PlanId = GetRandomNumber(3);
            subscription.TenantId = GetRandomNumber(3);

            return subscription;
        }

        public static TransactionDto RandomTransaction()
        {
            var transaction = new TransactionDto();
            transaction.Amount = GetRandomNumber(4);
            transaction.CreatedBy = "L-" + GetRandomString(5);
            transaction.CreatedDT = GetDateTime();
            transaction.PaymentDate = GetDateTime(2);
            transaction.ActionId = GetRandomNumber(2);
            transaction.ResultId = GetRandomNumber(3);
            transaction.TenantId = GetRandomNumber(3);
            transaction.PaymentDate = GetDateTime(3);

            return transaction;
        }

        public static AddressDto RandomAddress()
        {
            var address = new AddressDto();
            address.AddressLine1 = GetRandomString(30);
            address.CreatedBy = "L-" + GetRandomString(5);
            address.CreatedDT = GetDateTime();
            address.AddressLine2 = "L-" + GetRandomString(50);
            address.AddressLine3 = "L-" + GetRandomString(50);
            address.Country = "L-" + GetRandomString(12);
            address.PostCode = "L-" + GetRandomString(12);
            address.TenantId = GetRandomNumber(3);

            return address;
        }

        public static UserDto RandomUser()
        {
            var user = new UserDto();
            user.Approval = true;         
            user.Email = "L-" + GetRandomString(10);
            user.FirstName = "L-" + GetRandomString(12);
            user.LastName = "L-" + GetRandomString(12);        
            user.Mobile = GetRandomString(3);
            user.Note = GetRandomString(3);
            user.PhotoUrl = GetRandomString(3);    
            user.StatusId = GetRandomNumber(3);          
            user.UserId = GetRandomNumber(3);

            return user;
        }

        public static UserAccountDto RandomUserAccount()
        {
            var user = new UserAccountDto();
            user.CreatedBy = "L-" + GetRandomString();
            user.CreatedDT = GetDateTime();          
            user.Email = "L-" + GetRandomString(50);            
            user.TenantId = GetRandomNumber(3);
            user.UserName = GetRandomString(10);
            user.UserId = GetRandomNumber(3);

            return user;
        }

        public static TenantDto RandomTenant()
        {
            var tenant = new TenantDto();
            tenant.Name = "L-" + GetRandomString(15);
            tenant.Mobile = GetRandomString(15);
            tenant.Description = "L-" + GetRandomString(10);
            tenant.Email = GetFakeEmail();
            tenant.Telephone = GetRandomPhoneNumber(5);
            tenant.CreatedBy = "L-" + GetRandomString();
            tenant.CreatedDT = GetDateTime();
            tenant.StatusId = GetRandomNumber(3);          
            tenant.AddressId = GetRandomNumber(3);

            return tenant;
        }

        public static TenantAdapterDto RandomTenantAdapter(this TenantAdapterDto tenant)
        {
            tenant.Subscription = RandomSubscription();
            tenant.Tenant = RandomTenant();
            tenant.Transaction = RandomTransaction();
            tenant.User = RandomUserAccount();

            return tenant;
        }

    }
}

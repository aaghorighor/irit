namespace Suftnet.Cos.UnitTest
{
    using Moq;
    using NUnit.Framework;
    using Suftnet.Cos.Admin.Controllers;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [TestFixture]
    public class SubscriptionControllerTest
    {
        private Mock<ISubscription> _subscription;
        private Mock<IUserAccount> _userAccount;
        private SubscriptionController _subscriptionController;
        private SubscriptionDto subScription;
        private SubscriptionDto subscriptionToCreate;

        [SetUp]
        public void SetUp()
        {
            _subscription = new Mock<ISubscription>(MockBehavior.Loose);
            _userAccount = new Mock<IUserAccount>(MockBehavior.Loose);

            subScription = new SubscriptionDto { Id = 1, flag = (int)flag.Add, PlanId = (int)eLookUp.MinistryType, TenantId = 12, CreatedBy = "Tester", CreatedDT = DateTime.Now, ExpirationDate = DateTime.Today.AddDays(90), PaymentDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now.AddDays(1), StatusId = (int)eLookUp.ExpenseType };
            subscriptionToCreate = new SubscriptionDto { Id = 1, flag = (int)flag.Update, PlanId = (int)eLookUp.MinistryType, TenantId = 12, CreatedBy = "Tester", CreatedDT = DateTime.Now, ExpirationDate = DateTime.Today.AddDays(90), PaymentDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now.AddDays(1), StatusId = (int)eLookUp.ExpenseType };
            
            _subscription.Setup(s => s.Get(It.IsAny<int>())).Returns(subScription);      
            _subscription.Setup(s => s.Update(It.IsAny<SubscriptionDto>())).Returns(true);
            _subscription.Setup(s => s.Insert(It.IsAny<SubscriptionDto>())).Returns(1);
            _subscription.Setup(s => s.Delete(It.IsAny<int>())).Returns(true);
            _subscription.Setup(s => s.Get(It.IsAny<int>())).Returns(subscriptionToCreate);

            _subscriptionController = new SubscriptionController(_subscription.Object);
        }

        [Test]
        public void When_Subscription_Is_Created_It_Should_ReturnOk()
        {           
           var target = _subscriptionController.Create(subscriptionToCreate);
           dynamic result = target.Data;

           Assert.AreEqual(true, result.ok);
           Assert.AreEqual((int)flag.Update, result.flag);
           Assert.AreEqual(subscriptionToCreate, result.objrow);
        }

        [Test]
        public void Index_Should_Returned_Subscription_List()
        {
            var tenantId = 10;
            var result = _subscriptionController.Index(tenantId);
            var target = (result as ViewResult).Model as List<SubscriptionDto>;
           
            target.ShouldEqual(target);                   
        }

    }    
}

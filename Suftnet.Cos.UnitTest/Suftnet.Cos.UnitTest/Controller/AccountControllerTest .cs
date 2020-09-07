namespace Suftnet.Cos.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using NUnit.Framework;
    using Moq;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web;
    using Services;
   
   [TestFixture]
   public class AccountControllerTests  :TestBase
   {
        private Mock<ISmtp> _iSmtp;
        private Mock<IUserAccount> _userAccount;
        private Mock<IMember> _member;
        private Mock<ISecurity> _security;
        private Mock<IEditor> _editor;
        private AccountController _controller;

       [SetUp]
       public void SetUp()
       {
           base.Initialize();

            _iSmtp = new Mock<ISmtp>(MockBehavior.Default);
            _userAccount = new Mock<IUserAccount>(MockBehavior.Default);
            _member = new Mock<IMember>(MockBehavior.Default);
            _security = new Mock<ISecurity>(MockBehavior.Default);
            _editor = new Mock<IEditor>(MockBehavior.Default);
            _controller = new AccountController(_iSmtp.Object, _userAccount.Object, _member.Object, _security.Object, _editor.Object);

            _controller.SetupControllerContext(CreateMockHttpContext().Object);
       }

       [TearDown]
       public void TearDown()
       {
           base.TeardownHttpContext();
       }

       [Test]
       public void LoginShouldReturnedLoginNameAsView()
       {
           var result = _controller.Login() as ViewResult;
           result.ViewName.ShouldBeTheSameAs("login");
       }

        [Test]
        public void LoginShouldNotBeNullWhenCalled()
        {
            var result = _controller.Login() as ViewResult;
            result.ShouldNotBeNull();
        }

        [Test]
        public void LoginShouldReturnedLoginModel()
        {
            var result = _controller.Login() as ViewResult;
            result.Model.ShouldBe<LoginModel>();
        }

        [Test]
       public void When_User_Has_Already_Be_Authentication_Ok_Should_Be_True()
       {
           var model = new LoginModel { Password = "123456789", Username = "kabelsus@tester.com", Remember = true };                   
           dynamic result = _controller.Login(model, "") as JsonResult;
           var data = result.Data;
          
           Assert.AreEqual(true, data.ok);
       }    

   }
}

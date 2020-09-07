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
   public class SupportControllerTest : TestBase
   {
       
       [SetUp]
       public void SetUp()
       {
           base.Initialize();           
       }

       [TearDown]
       public void TearDown()
       {
           base.TeardownHttpContext();
       }     

   }
}

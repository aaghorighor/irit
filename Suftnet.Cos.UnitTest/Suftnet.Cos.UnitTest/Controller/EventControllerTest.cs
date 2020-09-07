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
    using Web.Command;

    [TestFixture]
   public class EventControllerTest : TestBase
   {
        private Mock<IEvent> _event = new Mock<IEvent>(MockBehavior.Loose);
        private Mock<IEventFile> _eventFile = new Mock<IEventFile>(MockBehavior.Loose);
        private Mock<IEventMember> _eventMember = new Mock<IEventMember>(MockBehavior.Loose);
        private Mock<IFactoryCommand> _factoryCommand = new Mock<IFactoryCommand>(MockBehavior.Loose);
        private EventController _eventControlloer;

        [SetUp]
       public void SetUp()
       {
           base.Initialize();
           SetupController();
       }

       [TearDown]
       public void TearDown()
       {
           base.TeardownHttpContext();
       }

       [Test]

       #region private function 

       private void SetupEventMember()
        {
            _eventMember.Setup(x => x.Insert(It.IsAny<EventMemberDto>())).Returns(1);
        }
       private void SetupEventFile()
        {
            _eventFile.Setup(x => x.Get(It.IsAny<int>())).Returns(new EventFileDto());
        }
       private void SetupfactoryCommand()
        {
          //Mock<IEventListCommand> eventListCommand = new Mock<IEventListCommand>(MockBehavior.Loose);
    
        }
       private void SetupController()
        {
            SetupEventMember();
            SetupEventFile();

           _eventControlloer = new EventController(_eventFile.Object, _factoryCommand.Object, _eventMember.Object);
        }

        #endregion
    }
}

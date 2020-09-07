namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;  
    using ViewModel;  
    using Common;
    using System;

    public class MyDashboardCommand : IMyDashboardCommand
    {      
        private readonly ISmallGroup _smallGroup;
        private readonly IMember _member;
        private readonly IEventTimeLine _eventTimeLine;
        private readonly IServiceTimeLine _serviceTimeLine;

        public MyDashboardCommand(IMember member, ISmallGroup SmallGroup,
            IEventTimeLine eventTimeLine, IServiceTimeLine serviceTimeLine)
        {          
            _smallGroup = SmallGroup;
            _member = member;
            _eventTimeLine = eventTimeLine;
            _serviceTimeLine = serviceTimeLine;
        }               
              
        public int MemberId { get; set; }

        public MyDashboardModel Execute()
        {
            return DashboardAsync();
        }

        #region private function
        private MyDashboardModel DashboardAsync()
        {
            var member = _member.Get(MemberId);

            var dashboardModel = new MyDashboardModel
            {
                BirthDays = _member.GetMemberBirthDayCountOfTheMonth(member.TenantId, DateTime.UtcNow.Month, (int)MemberStatus.Member),
                MySchedules = MySchedules()
            };

          return dashboardModel;
        }    
        
        private int MySchedules()
        {
            var eventTimeLines = _eventTimeLine.Count(this.MemberId);
            var serviceTimeLines = _serviceTimeLine.Count(this.MemberId);

            return eventTimeLines + serviceTimeLines;
        }

        #endregion

    }
}
namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using Core;
    using System.Collections.Generic;
    using ViewModel;
    using Cos.Services;
    using Common;
    using System;

    public class DashboardCommand : IDashboardCommand
    {
        private readonly IMember _member;
        private readonly IEvent _event;

        public DashboardCommand(IMember member, IEvent @event)
        {
            _member = member;
            _event = @event;
        }               
              
        public int TenantId { get; set; }

        public DashboardModel Execute()
        {
            return DashboardAsync();
        }

        #region private function
        public DashboardModel DashboardAsync()
        {
            var dashboardModel = new DashboardModel
            {
                Events = _event.Count((int)EventStatus.Open, TenantId),
                Members = _member.Count(TenantId, (int)MemberStatus.Member),
                BirthDays = _member.GetMemberBirthDayCountOfTheMonth(TenantId, DateTime.UtcNow.Month, (int)MemberStatus.Member)
            };

          return dashboardModel;
        }             

        #endregion

    }
}
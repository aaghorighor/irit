namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using ViewModel;
    using Extension;
    using Common;
    using DataAccess.Identity;

    public class ChangeCommand : ICommand
    {
        private readonly IMember _member;
        private readonly IUser _userAccount;
        private readonly IMemberAccount _memberAccount;

        public ChangeCommand(IMember member, IUser userAccount, IMemberAccount memberAccount)
        {
            _member = member;
            _userAccount = userAccount;
            _memberAccount = memberAccount;
        }
               
        public MemberChangeModel MemberChangeModel { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public void Execute()
        {
            ApplicationUser = this.PrepareChangeAsync();           
        }

        #region private function
       
        private ApplicationUser PrepareChangeAsync()
        {         
          var memberChangeDto = new MemberChangeDto
            {
                MemberId = MemberChangeModel.MemberExternalId.ToDecrypt().ToInt(),
                NewTenantId = MemberChangeModel.NewTenantExternalId.ToDecrypt().ToInt(),
                TenantId = MemberChangeModel.TenantExternalId.ToDecrypt().ToInt(),
                AreaId = (int)eArea.MemberOffice,               
                StatusId = (int)MemberStatus.Visitor
            };

            ChangeMember(memberChangeDto);
            ChangeUser(memberChangeDto);
            ChangeMemberAccount(memberChangeDto);
            ChangeMemberFamily(memberChangeDto);

           var model = _userAccount.GetUserByMemberId(MemberChangeModel.MemberExternalId.ToDecrypt().ToInt());
           return model;
        }  

        private void ChangeMember(MemberChangeDto memberChangeDto)
        {
            _member.Change(memberChangeDto);
        }
        private void ChangeMemberFamily(MemberChangeDto memberChangeDto)
        {
            var members = _member.GetMembersForChange(memberChangeDto);

            foreach (var member in members)
            {
                _member.Change(member);
            }
        }

        private void ChangeUser(MemberChangeDto memberChangeDto)
        {
            _userAccount.Change(memberChangeDto);
        }

        private void ChangeMemberAccount(MemberChangeDto memberChangeDto)
        {
            _memberAccount.Change(memberChangeDto.MemberId, memberChangeDto.NewTenantId);
        }

        
        #endregion

    }
}
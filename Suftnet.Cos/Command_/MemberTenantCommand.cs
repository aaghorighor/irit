namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;

    public class MemberTenantCommand : ICommand
    {
        private readonly IMember _member;
        public MemberTenantCommand(IMember member)
        {
            _member = member;          
        }

        public int MemberId { get; set; }      
        public void Execute()
        {
            _member.DeleteAll(this.MemberId);
        }      
    }
}
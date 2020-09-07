namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGetMyFamilyCommand
    {
        int FamilyMemberId { get; set; }
        Task<IEnumerable<MemberModel>> Execute();
    }
}
namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using System;
    public interface IRegistrationCommand : ICommand
    {
        MemberDto EntityToCreate { get; set; }      
        string CreatedBy { get; set; }
        DateTime CreatedDt { get; set; }       
   }
}

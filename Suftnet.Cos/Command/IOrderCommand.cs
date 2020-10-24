namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using System;

    public interface IOrderCommand
    {
        DateTime CreatedDt { get; set; }
        string CreatedBy { get; set; }
        Guid TenantId { get; set; }
        OrderedSummaryDto OrderedSummary { get; set; }
        void Execute();
   }
}

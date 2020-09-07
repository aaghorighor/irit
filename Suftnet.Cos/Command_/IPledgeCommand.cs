using System;

namespace Suftnet.Cos.Web.Command
{
    public interface IPledgeCommand
   {
        int PledgeId { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDT { get; set; }
        void Execute();
   }
}

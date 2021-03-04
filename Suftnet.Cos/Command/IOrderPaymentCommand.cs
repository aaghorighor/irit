namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web.ViewModel;
    using System;

    public interface IOrderPaymentCommand
    {
        DeliveryOrderAdapter entityToCreate { get; set; }     
        string Reason { get; set; }
        bool Error { get; set; }
        void Execute();
   }
}

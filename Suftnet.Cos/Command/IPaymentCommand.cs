namespace Suftnet.Cos.Web.Command
{   
    public interface IPaymentCommand
    {       
        PaymentModel Model { get; set; }
        void Execute();
   }
}

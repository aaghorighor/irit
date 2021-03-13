namespace Suftnet.Cos.Web.Command
{   
    public interface IPaymentCommand
    {       
        PaymentModel paymentModel { get; set; }
        void Execute();
   }
}

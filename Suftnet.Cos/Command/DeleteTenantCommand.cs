namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using System;

    public class DeleteTenantCommand : ICommand
    {
        private readonly ITenant _tenant;
               
        public DeleteTenantCommand(ITenant tenant)
        {
            _tenant = tenant;                  
        }

        public Guid TenantId { get; set; }
        
        public void Execute()
        {                      
           
            _tenant.Delete(this.TenantId);
        }       
    }
}
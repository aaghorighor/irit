namespace Suftnet.Cos.Web.Command
{
    using Service;
    using Suftnet.Cos.DataAccess;

    public class CheckForExtingCustomerCommand :ICommand
    {
        private readonly IUser _userAccount;
        public CheckForExtingCustomerCommand(IUser userAccount)
        {
            _userAccount = userAccount;                     
        }

        public string UserName { get; set; }
        public bool IsCustomerNew { get; set; } = true;

        public void Execute()
        {
            this.CheckIfCustomerExist();
        }

        #region private function
        private void CheckIfCustomerExist()
        {
            IsCustomerNew = _userAccount.CheckEmailAddress(UserName);            
        }
        #endregion

    }
}
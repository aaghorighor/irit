namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using ViewModel;    
    using System;
    using System.Threading.Tasks;
    using Suftnet.Cos.Services.Interface;
    
    public class UserBoostrapCommand : IUserBoostrapCommand
    {            
        private readonly IJwToken _jwToken;
        private readonly IMobilePermission _mobilePermission;
      
        public UserBoostrapCommand(
            IMobilePermission mobilePermission,
             IJwToken jwToken)
        {           
            _jwToken = jwToken;           
            _mobilePermission = mobilePermission;
        }               
              
        public Guid TenantId { get; set; }
        public MobileTenantDto User { get; set; }

        public async Task<UserBoostrap> Execute()
        {
            var task = await Task.Factory.StartNew(() => {

                var model = new UserBoostrap
                {
                    user = new
                    {
                        externalId = User.ExternalId,
                        firstName = User.FirstName,
                        lastName = User.LastName,                      
                        areaId = User.AreaId,
                        area = User.Area,
                        phoneNumber = User.PhoneNumber,
                        userName = User.UserName,                      
                        permissions = GetUserPermissions(),
                        token = _jwToken.Create(User.UserName, User.Id)
                    }                   
                };

                return model;
            });

            return task;
        }

        #region private function
        private string GetUserPermissions()
        {
            var array = _mobilePermission.GetPermissionByUserId(User.Id);
            string result = string.Join(",", array);
            return result;
        }
        #endregion

    }
}
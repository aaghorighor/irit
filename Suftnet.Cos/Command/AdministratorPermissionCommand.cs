namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
  
    using Common;   
    using System.Linq;

    public class AdministratorPermissionCommand : ICommand
    {
        private readonly IPermission _permission;
        private readonly ICommon _common;
        public AdministratorPermissionCommand(IPermission permission, ICommon common)
        {
            _permission = permission;
            _common = common;
        }
      
        public int UserId { get; set; }
        public string IdentityId { get; set; }
        public string CreatedBy { get; set; }
        public void Execute()
        {
            this.PrepareUserPermission();
        }

        #region private function

        private void PrepareUserPermission()
        {
            var permissions = _common.GetAll((int)eSettings.View);
            _permission.Clear(this.IdentityId);           
                      
            if(permissions.Any())
            {
                foreach(var permission in permissions)
                {
                    var permissionDto = new PermissionDto
                    {
                        IdentityId = this.IdentityId,
                        ViewId = permission.Id,                      
                        Create = Cos.Common.Permission.Enable,
                        Edit = Cos.Common.Permission.Enable,
                        Remove = Cos.Common.Permission.Enable,
                        Get = Cos.Common.Permission.Enable,
                        GetAll = Cos.Common.Permission.Enable,

                        CreatedBy = CreatedBy,
                        CreatedDT = DateTime.UtcNow
                    };

                    _permission.Insert(permissionDto);
                }          
            }           
        }

        #endregion

    }
}
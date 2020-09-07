namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
  
    using Common;   
    using System.Linq;

    public class CreateUserPermissionCommand : ICommand
    {
        private readonly IPermission _permission;
        private readonly ICommon _common;
        public CreateUserPermissionCommand(IPermission permission, ICommon common)
        {
            _permission = permission;
            _common = common;
        }
      
        public int UserId { get; set; }
        public string IdentityId { get; set; }
        public string CreatedBy { get; set; }
        public void Execute()
        {
            this.Create();
        }

        #region private function

        private void Create()
        {
            var backOfficePermissions = _common.GetAll((int)eSettings.Backofficepages);
                      
            if(backOfficePermissions.Any())
            {
                foreach(var permission in backOfficePermissions)
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

            var frontOfficePermissions = _common.GetAll((int)eSettings.FrontOfficepages);

            if (frontOfficePermissions.Any())
            {
                foreach (var permission in frontOfficePermissions)
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
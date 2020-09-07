namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
    using Common;
    using System.Linq;

    public class PermissionCommand : ICommand
    {
        private readonly IPermission _permission;
        private readonly ICommon _common;
        public PermissionCommand(IPermission permission, ICommon common)
        {
            _permission = permission;
            _common = common;
        }
      
        public string UserId { get; set; }         
        public int PermissionTypeId { get; set; }   
        public string CreatedBy { get; set; }
        public void Execute()
        {
            switch(PermissionTypeId)
            {
                case (int)eSettings.Backofficepages:
                    BackOfficePermissions();
                    break;

                case (int)eSettings.FrontOfficepages:
                    FrontOfficePermissions();
                    break;
            }         
        }

        #region private function

        private void BackOfficePermissions()
        {              
            var backOfficePermissions = _common.GetAll((int)eSettings.Backofficepages);
            _permission.Clear(this.UserId);

            if (backOfficePermissions.Any())
            {
                foreach (var permission in backOfficePermissions)
                {
                    var permissionDto = new PermissionDto
                    {
                        IdentityId = this.UserId,
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
                        IdentityId = this.UserId,
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
        private void FrontOfficePermissions()
        {          
            var frontOfficePermissions = _common.GetAll((int)eSettings.FrontOfficepages);

            if (frontOfficePermissions.Any())
            {
                foreach (var permission in frontOfficePermissions)
                {
                    var permissionDto = new PermissionDto
                    {
                        IdentityId = this.UserId,
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
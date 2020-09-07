namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;

    using ViewModel;
    using Common;
    using Services.Implementation;
    using System.Web;
    using DataAccess.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Core;
    using System.Threading.Tasks;

    public class CreateUserCommand: ICommand
    {       
        private readonly IMember _member;
        private ApplicationUserManager _userManager;
        public CreateUserCommand(IMember member)
        {            
            _member = member;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null && HttpContext == null)
                {
                    return new ApplicationUserManager(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>());
                }

                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TenantId { get; set; }
        public string CreatedBy { get; set; }
        public HttpRequestBase HttpContext { get; set; }
        public CheckoutModel UserModel { get; set; }
        public void Execute()
        {
            Create().ConfigureAwait(false);
        }

        #region private function
        private async Task Create()
        {
            var applicationUser = new ApplicationUser
            {
                TenantId = TenantId,
                Area = GetArea((int)eArea.BackOffice),
                AreaId = (int)eArea.BackOffice,
                FirstName = UserModel.FirstName,
                LastName = UserModel.LastName,
                ImageUrl = Constant.DefaultMemberImageUrl,
                Active = true,
                Email = UserModel.Email,
                PhoneNumber = UserModel.Mobile,
                UserName = UserModel.Email,

                MemberId = this.CreateMember()
            };

            var result = await UserManager.CreateAsync(applicationUser, Constant.DefaultPassword);

            if (result.Succeeded)
            {
                User = await UserManager.FindByEmailAsync(UserModel.Email);
                return;
            }
        }

        private int CreateMember()
        {
            var memberModel = new MemberDto()
            {
                FirstName = UserModel.FirstName,
                LastName = UserModel.LastName,
                Mobile = string.IsNullOrEmpty(UserModel.Mobile) == true ? "" : UserModel.Mobile,
                Email = string.IsNullOrEmpty(UserModel.Email) == true ? "" : UserModel.Email,
                TenantId = TenantId,          
                MemberTypeId = 423, // None           
                GenderId = 373, // Male     
                DateOfBirth = DateTime.UtcNow,
                JoinDate = DateTime.UtcNow,
                FileName = Constant.DefaultMemberImageUrl,
                IsEmail = true,
                IsSms = true,
                IsVisible = true,
                StatusId  = 381, //Member
                AddressId = this.AddressId,

                CreatedBy = UserModel.FirstName,
                CreatedDT = DateTime.UtcNow
            };

            return _member.Insert(memberModel);
        }
        private string GetArea(int? areaId)
        {
            var service = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();
            var model = service.Get((int)areaId);
            return model.Title;
        }                

        #endregion

    }
}
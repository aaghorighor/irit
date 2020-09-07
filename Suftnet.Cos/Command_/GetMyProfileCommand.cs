namespace Suftnet.Cos.Web.Command
{
    using Extension;
    using Suftnet.Cos.DataAccess;

    public class GetMyProfileCommand : ICommand
    {           
        private readonly IMember _member;
      
        public GetMyProfileCommand(IMember member)
        {            
            _member = member;        
        }
         
        public int MemberId { get; set; }
        public MemberModel MemberModel { get; set; }

        public void Execute()
        {
            this.PrepareMyProfile();
        }

        #region private function

        private void PrepareMyProfile()
        {
            var model = _member.Get(this.MemberId);

            if(model == null)
            {
                MemberModel = null;
            }
            else
            {
                MapFrom(model);
            }               
        }

        private void MapFrom(MemberDto model)
        {
            var memberModel = new MemberModel();
          
            memberModel.FirstName = model.FirstName;
            memberModel.LastName = model.LastName;
            memberModel.Mobile = string.IsNullOrEmpty(model.Mobile) == true ? "" : model.Mobile ;
            memberModel.Email = string.IsNullOrEmpty(model.Email) == true ? "" : model.Email;          
            memberModel.MemberTypeId = model.MemberTypeId;
            memberModel.MemberType = model.MemberType;         
            memberModel.GenderId = model.GenderId;
            memberModel.Gender = model.Gender;        
            memberModel.StatusId = model.StatusId;
            memberModel.Status = model.Status;       
            memberModel.DateOfBirth = model.DateOfBirthDT;
            memberModel.JoinDate = model.MembershipDT;
            memberModel.FileName = model.FileName == null ? Suftnet.Cos.Common.Constant.DefaultMemberImageUrl : model.FileName;
            memberModel.IsEmail = model.IsEmail == null ? false : model.IsEmail;
            memberModel.IsSms = model.IsSms == null ? false : model.IsSms;

            memberModel.AddressId = model.AddressId;
            memberModel.AddressLine1 = string.IsNullOrEmpty(model.AddressLine1) == true ? "" : model.AddressLine1;
            memberModel.AddressLine2 = string.IsNullOrEmpty(model.AddressLine2) == true ? "" : model.AddressLine2;
            memberModel.AddressLine3 = string.IsNullOrEmpty(model.AddressLine3) == true ? "" : model.AddressLine3;
            memberModel.CompleteAddress = string.IsNullOrEmpty(model.AddressLine) == true ? "" : model.AddressLine;
            memberModel.County = string.IsNullOrEmpty(model.County) == true ? "" : model.County;
            memberModel.Town = string.IsNullOrEmpty(model.Town) == true ? "" : model.Town;
            memberModel.PostCode = string.IsNullOrEmpty(model.PostCode) == true ? "" : model.PostCode;
            memberModel.Country = string.IsNullOrEmpty(model.Country) == true ? "" : model.Country;
            memberModel.Longitude = model.Longitude;
            memberModel.Latitude = model.Latitude;
            memberModel.Description = model.Description == null ? "" : model.Description;
            memberModel.ExternalId = model.Id.ToString().ToEncrypt();
            memberModel.FamilyExternalId = model.FamilyId != null ? model.FamilyId.ToString().ToEncrypt() : null;
            MemberModel =  memberModel;
        }

        #endregion

    }
}
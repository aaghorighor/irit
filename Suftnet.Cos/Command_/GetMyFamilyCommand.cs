namespace Suftnet.Cos.Web.Command
{
    using Extension;
    using Suftnet.Cos.DataAccess;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GetMyFamilyCommand : IGetMyFamilyCommand
    {           
        private readonly IFamily _family;
      
        public GetMyFamilyCommand(IFamily family)
        {
            _family = family;        
        }
         
        public int FamilyMemberId { get; set; }
       
        public async Task<IEnumerable<MemberModel>> Execute()
        {
            var model = await System.Threading.Tasks.Task.Run(()=> this.PrepareMyProfile());

            return model;
        }

        #region private function

        private List<MemberModel> PrepareMyProfile()
        {
            var model = _family.GetByFamilyId(this.FamilyMemberId);

            return MapFrom(model);
        }

        private List<MemberModel> MapFrom(List<FamilyDto> familyMember)
        {
            var memberModels = new List<MemberModel>();

            foreach(var model in familyMember)
            {
                if(model.Id == FamilyMemberId)
                {
                    continue;
                }

                var memberModel = new MemberModel();

                memberModel.FirstName = model.FirstName;
                memberModel.LastName = model.LastName;
                memberModel.Mobile = model.Mobile == null ? "" : model.Mobile;
                memberModel.Email = model.Email == null ? "" : model.Email;
                memberModel.MemberType = model.MemberType;
                memberModel.FileName = model.FileName == null ? "" : model.FileName;
                memberModel.ExternalId = model.Id.ToString().ToEncrypt();
                memberModel.FamilyExternalId = this.FamilyMemberId.ToString().ToEncrypt();
            
                memberModels.Add(memberModel);
            }

            return memberModels;
        }

        #endregion

    }
}
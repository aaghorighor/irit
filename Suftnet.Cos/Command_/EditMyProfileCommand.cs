namespace Suftnet.Cos.Web.Command
{ 
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;

    using System;
 

    public class EditMyProfileCommand : IRegistrationCommand
    {       
        private readonly IAddress _address;
        private readonly IMember _member;
        private readonly IFamily _family;

        public EditMyProfileCommand(IAddress address, IMember member, IFamily family)
        {            
            _member = member;
            _address = address;
            _family = family;
        }
         
        public MemberDto EntityToCreate { get; set; }    
        public string CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }

        public void Execute()
        {
            this.PrepareMyProfileForEdit();
        }

        #region private function

        private void PrepareMyProfileForEdit()
        {
            EntityToCreate.Id = EntityToCreate.MemberExternalId.ToDecrypt().ToInt();
                         
           _member.UpdateMyProfile(EntityToCreate);
           _address.UpdateByAddressId(EntityToCreate);
        }       
      

        #endregion

    }
}
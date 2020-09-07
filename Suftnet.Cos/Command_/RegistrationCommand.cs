namespace Suftnet.Cos.Web.Command
{
    using Common;
    using Core;
    using Cos.Services;    
    using Model;
    using Services.Implementation;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using System.Web;
    using System;   
    using System.Text;
    using Microsoft.AspNet.Identity.Owin;
    using DataAccess.Identity;

    public class RegistrationCommand : IRegistrationCommand
    {      
        private readonly IAddress _address;
        private readonly IMember _member;       
        private readonly IFamily _family;

        public RegistrationCommand(IAddress address,
            IMember member,   IFamily family)
        {          
            _member = member;
            _address = address;    
            _family = family;
        }      
  
        public MemberDto EntityToCreate { get; set; }     
        public string CreatedBy { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDt { get; set; }

        public void Execute()
        {
            this.Create();
        }

        #region private function

        private void Create()
        {               
            EntityToCreate.AddressId = _address.Insert(EntityToCreate);
            EntityToCreate.Id = _member.Insert(EntityToCreate);

            if (!string.IsNullOrEmpty(EntityToCreate.FamilyExternalId))
            {
                EntityToCreate.FamilyId = EntityToCreate.FamilyExternalId.ToDecrypt().ToInt();
                MapFamily(EntityToCreate);
            }           
           
        }    

        private void MapFamily(MemberDto entityToCreate)
        {
            var family = new FamilyDto()
            {
                Approved = true,
                Memberid = (int)entityToCreate.FamilyId,
                RelationId = entityToCreate.Id,
                Flag = true,

                CreatedDT = entityToCreate.CreatedDT,
                CreatedBy = entityToCreate.CreatedBy

            };

            _family.Insert(family);
        }
        
       
        #endregion

    }
}
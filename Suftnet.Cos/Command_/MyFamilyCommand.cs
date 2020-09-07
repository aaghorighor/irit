namespace Suftnet.Cos.Web.Command
{
    using Core;
    using ImageResizer;

    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;

    public class MyFamilyCommand : IRegistrationCommand
    {
        private readonly ITenant _tenant;
        private readonly IAddress _address;
        private readonly IMember _member;           
        private readonly IFamily _family;

        public MyFamilyCommand(ITenant tenant, IAddress address, IFamily family,
            IMember member)
        {
            _family = family;
            _member = member;
            _address = address;        
            _tenant = tenant;               
        }
         
        public MemberDto EntityToCreate { get; set; }    
        public string CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }

        public void Execute()
        {
            this.PrepareRegistration();
        }

        #region private function

        private void PrepareRegistration()
        {
            EntityToCreate.FamilyId = 0;
            EntityToCreate.TenantId = EntityToCreate.ExternalId.ToDecrypt().ToInt();
            EntityToCreate.CreatedDT = DateTime.UtcNow;
            EntityToCreate.CreatedBy = EntityToCreate.FirstName;

            try
            {
                EntityToCreate.FileName = PrepareImageUrl(EntityToCreate.FileName);
            }
            catch (Exception ex)
            { GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>().LogError(ex); }

            if (!string.IsNullOrEmpty(EntityToCreate.FamilyExternalId))
            {
                EntityToCreate.FamilyId = EntityToCreate.FamilyExternalId.ToDecrypt().ToInt();
            }
           
            EntityToCreate.AddressId = _address.Insert(EntityToCreate);
            EntityToCreate.Id = _member.Insert(EntityToCreate);

            if (EntityToCreate.FamilyId > 0)
            {
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
      
        public string PrepareImageUrl(string imageFileName)
        {
            var guid = System.Guid.NewGuid().ToString();
            var path = string.Empty;
            var versions = GetVersions();
         
            string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/Member/");          

            foreach (string suffix in versions.Keys)
            {
                if (!Directory.Exists(uploadFolder + "/" + suffix)) Directory.CreateDirectory(uploadFolder + "/" + suffix);

                string fileName = Path.Combine(uploadFolder + "\\" + suffix, guid);
                Bitmap bitmap = new Bitmap(ImagePath(imageFileName));
                path = ImageBuilder.Current.Build(bitmap, fileName, new ResizeSettings(versions[suffix]), false, true);
            }

            int index1 = path.LastIndexOf('\\');
            if (index1 != -1)
            {
                return path.Substring(index1 + 1);
            }

            return string.Empty;

        }
        private Dictionary<string, string> GetVersions()
        {          
            Dictionary<string, string> versions = new Dictionary<string, string>();
            versions.Add("216X196", "width=216&height=196&crop=auto&format=jpg");
            return versions;
        }
        private string ImagePath(string fileName)
        {
            return System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/imageTemp/" + fileName);
        }

        #endregion

    }
}
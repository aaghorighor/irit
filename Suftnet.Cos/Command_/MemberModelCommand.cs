namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
    using System.Web;
    using Suftnet.Cos.Extension;
    using Microsoft.Web.Helpers;
    using System.Linq;
    using System.IO; 
    using System.Net;
    using System.Collections.Generic;
    using ImageResizer;
    using Core;

    public class MemberModelCommand : ICommand
    {
        private IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        public IList<string> Reason = new List<string>();
        private int MaxContentLength = 1024 * 1024 * 1;
        private string ImageUrl { get; set; }
        public int Flag;
        public HttpStatusCode HttpStatusCode { get; set; }       
        public MemberDto entityToCreate { get; set; }
        public MemberModelCommand()
        {                    
        }      
        public HttpRequest HttpContext { get; set; }
        public string UPloadFolder { get; set; }
             
        public void Execute()
        {
          this.PrepareFileUpload();
          if(this.HttpStatusCode == HttpStatusCode.Created) {

                switch(Flag)
                {
                    case 1:
                        entityToCreate = Create();
                        break;                      
                    case 2:
                        entityToCreate = Edit();
                        break;
                }               
            }     
        }

        #region private function

        private MemberDto Create()
        {
            var model = new MemberDto();

            if (HttpContext.Form.AllKeys.Any())
            {
                var firstName = HttpContext.Form["firstName"];
                var lastName = HttpContext.Form["lastName"];
                var externalId = HttpContext.Form["externalId"];
                var mobile = HttpContext.Form["mobile"];
                var email = HttpContext.Form["email"];               
                var memberTypeId = HttpContext.Form["memberTypeId"];             
                var genderId = HttpContext.Form["genderId"];            
                var familyExternalId = HttpContext.Form["familyExternalId"];
                var dateOfBirth = HttpContext.Form["dateOfBirth"];
                var joinDate = HttpContext.Form["joinDate"];             
                var isEmail = HttpContext.Form["isEmail"];
                var isSms = HttpContext.Form["isSms"];
                var statusId = HttpContext.Form["statusId"];
                var description = HttpContext.Form["description"];
                var addressLine1 = HttpContext.Form["addressLine1"];
                var addressLine2 = HttpContext.Form["addressLine2"];
                var addressLine3 = HttpContext.Form["addressLine3"];
                var county = HttpContext.Form["county"];
                var country = HttpContext.Form["country"];
                var postCode = HttpContext.Form["postCode"];
                var town = HttpContext.Form["town"];
                var longitude = HttpContext.Form["longitude"];
                var latitude = HttpContext.Form["latitude"];
                var flag = HttpContext.Form["flag"];
               
                model = new MemberDto
                {                   
                    FileName = ImageUrl.ToImage(),
                    IsEmail = isEmail.ToBoolean(),
                    IsSms = isSms.ToBoolean(),                
                    MemberTypeId = memberTypeId.ToInt(),                  
                    GenderId = genderId.ToInt(),
                    StatusId = statusId.ToInt(),                
                    DateOfBirth = dateOfBirth == null ? DateTime.UtcNow : Convert.ToDateTime(dateOfBirth),
                    JoinDate = joinDate == null ? DateTime.UtcNow : Convert.ToDateTime(joinDate),
                    FirstName = firstName,
                    LastName = lastName,
                    Mobile = mobile,
                    Email = email,
                    FamilyExternalId = familyExternalId,
                    Description = description,
                    AddressLine1 = addressLine1,
                    AddressLine2 = addressLine2,
                    AddressLine3 = addressLine3,
                    County = county,
                    Country = country,
                    PostCode = postCode,
                    Town = town,
                    Longitude = longitude,
                    Latitude = latitude,
                    flag = flag.ToInt(),
                    IsVisible = true,

                    CreatedBy = email,
                    CreatedDT = DateTime.UtcNow,
                    TenantId = externalId.ToDecrypt().ToInt()
                };              
            }

            return model;
        }
        private MemberDto Edit()
        {
            var model = new MemberDto();

            if (HttpContext.Form.AllKeys.Any())
            {
                var firstName = HttpContext.Form["firstName"];
                var lastName = HttpContext.Form["lastName"];              
                var mobile = HttpContext.Form["mobile"];
                var email = HttpContext.Form["email"];             
                var memberTypeId = HttpContext.Form["memberTypeId"];            
                var genderId = HttpContext.Form["genderId"];             
                var titleId = HttpContext.Form["titleId"];              
                var dateOfBirth = HttpContext.Form["dateOfBirth"];
                var joinDate = HttpContext.Form["joinDate"];
                var isEmail = HttpContext.Form["isEmail"];
                var isSms = HttpContext.Form["isSms"];
                var statusId = HttpContext.Form["statusId"];
                var description = HttpContext.Form["description"];
                var addressLine1 = HttpContext.Form["addressLine1"];
                var addressLine2 = HttpContext.Form["addressLine2"];
                var addressLine3 = HttpContext.Form["addressLine3"];
                var county = HttpContext.Form["county"];
                var country = HttpContext.Form["country"];
                var postCode = HttpContext.Form["postCode"];
                var town = HttpContext.Form["town"];
                var longitude = HttpContext.Form["longitude"];
                var latitude = HttpContext.Form["latitude"];
                var memberExternalId = HttpContext.Form["memberExternalId"];
                var flag = HttpContext.Form["flag"];
                var addressId = HttpContext.Form["addressId"];

                model = new MemberDto
                {
                    FileName = ImageUrl.ToImage(),
                    IsEmail = isEmail.ToBoolean(),
                    IsSms = isSms.ToBoolean(),
                    MemberTypeId = memberTypeId.ToInt(),
                    GenderId = genderId.ToInt(),
                    StatusId = statusId.ToInt(),
                    DateOfBirth = dateOfBirth == null ? DateTime.UtcNow : Convert.ToDateTime(dateOfBirth),
                    JoinDate = joinDate == null ? DateTime.UtcNow : Convert.ToDateTime(joinDate),
                    FirstName = firstName,
                    LastName = lastName,
                    Mobile = mobile,
                    Email = email,
                    Description = description,
                    AddressLine1 = addressLine1,
                    AddressLine2 = addressLine2,
                    AddressLine3 = addressLine3,
                    AddressId = addressId.ToInt(),
                    Country = country,
                    PostCode = postCode,
                    flag = flag.ToInt(),
                    IsVisible = true,

                    CreatedBy = email,
                    CreatedDT = DateTime.UtcNow,
               
                    MemberExternalId = memberExternalId
                };               
            }

            return model;
        }
        private void PrepareFileUpload()
        {
            if (HttpContext.Files.AllKeys.Any())
            {               
                var httpPostedFile = HttpContext.Files["File"];
                if (httpPostedFile != null)
                {
                    FileUpload fileUpload = new FileUpload();
                    var ext = httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();

                    if (!AllowedFileExtensions.Contains(extension))
                    {
                        var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                        Reason.Add(message);
                        HttpStatusCode = HttpStatusCode.BadRequest;
                        return;                    
                    }                    
                    else
                    {
                        try
                        {
                            Upload(httpPostedFile);
                            Reason.Add("Image Uploaded Successfully.");
                            HttpStatusCode = HttpStatusCode.Created;
                        }
                        catch(Exception ex)
                        {
                            Reason.Add("Error occurred while uploading image");
                            HttpStatusCode = HttpStatusCode.NotFound;

                            var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
                            logger.LogError(ex);
                        }
                       
                    }
                }else
                {
                    Reason.Add("Please Upload a image.");
                    HttpStatusCode = HttpStatusCode.NotFound;
                }
            }
        }      
        public void Upload(HttpPostedFile file)
        {           
            var guid = System.Guid.NewGuid().ToString();
            var path = string.Empty;

            var versions = GetVersions();
          
            if (!Directory.Exists(UPloadFolder)) Directory.CreateDirectory(UPloadFolder);

            foreach (string suffix in versions.Keys)
            {
                if (!Directory.Exists(UPloadFolder + "/" + suffix)) Directory.CreateDirectory(UPloadFolder + "/" + suffix);

                string fileName = Path.Combine(UPloadFolder + "\\" + suffix, guid);
                path = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);

                int index = path.LastIndexOf('\\');
                if (index != -1)
                {
                    ImageUrl = path.Substring(index + 1);
                }
            }
        }
        private Dictionary<string, string> GetVersions()
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();
            versions.Add("216X196", "width=216&height=196&crop=auto&format=jpg");
            return versions;
        }

        #endregion
    }
}
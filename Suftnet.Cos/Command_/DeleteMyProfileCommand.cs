namespace Suftnet.Cos.Web.Command
{    
    using Suftnet.Cos.DataAccess;
   
    using System;
    using System.Collections.Generic;
    using System.IO;
 
    public class DeleteMyProfileCommand : ICommand
    {           
        private readonly IMember _member;
        private readonly IAddress _address;
        private readonly IFamily _family;

        public DeleteMyProfileCommand(IMember member, IFamily family, IAddress address)
        {            
            _member = member;
            _address = address;
            _family = family;
        }
         
        public int MemberId { get; set; }
        public int RowId { get; set; }
        public bool Flag { get; set; }      

        public void Execute()
        {
            this.PrepareToDeleteMyProfile();
        }

        #region private function

        private void PrepareToDeleteMyProfile()
        {
            var member = _member.Get(this.MemberId);

            if (member != null)
            {
                _family.Delete(this.RowId);
                _member.DeleteAll(this.MemberId);
                _address.Delete(member.AddressId);
                 DeleteImage(member.FileName);              

                Flag = true;
            }else
            {
                Flag = false;
            }
        }

        private void DeleteImage(string filename)
        {
            var versions = GetVersions();

            string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/Member/");

            foreach (string suffix in versions.Keys)
            {
                string filePath = Path.Combine(uploadFolder + "\\" + suffix, filename);

                if (!System.IO.File.Exists(filePath))
                {
                    continue;
                }

                new List<string>(Directory.GetFiles(uploadFolder + suffix)).ForEach(files =>
                {
                    if (files.IndexOf(filename, StringComparison.OrdinalIgnoreCase) >= 0)
                        System.IO.File.Delete(files);
                });
            }
        }

        private Dictionary<string, string> GetVersions()
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();

            //Define the versions to generate           
            versions.Add("216X196", "width=216&height=196&crop=auto&format=jpg");

            return versions;
        }

        #endregion

    }
}
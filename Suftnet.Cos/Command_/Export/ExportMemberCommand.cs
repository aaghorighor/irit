namespace Suftnet.Cos.Web.Command
{
    using System.Xml.Linq;
    using System.Xml;

    using Suftnet.Cos.DataAccess;
    using Core;
    using System.IO;

    public class ExportMemberCommand : Export, ICommand
    {
        private XDocument m_Document;
        private readonly IMember _member;
        public ExportMemberCommand(IMember member)
        {
            _member = member;
            this.CreateDocument();
        }
        protected override string Name
        {
            get { return "Member"; }
        }
        public override int TenantId { get; set; }
        public MemoryStream Content { get; set; }
        protected override void CreateDocument()
        {
            m_Document = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            var root = new XElement("members");
            m_Document.Add(root);
        }
        public override void Dispose()
        {
            m_Document = null;
        }
        public void Execute()
        {
            var members = _member.GetAll(this.TenantId);

            foreach(var member in members)
            {
                var item = new XElement("member"
                    , new XElement("id", member.Id)                    
                    , new XElement("gender", member.Gender)
                    , new XElement("firstname", member.FirstName)
                    , new XElement("lastname", member.LastName)
                    , new XElement("email", member.Email == null ? string.Empty : member.Email)
                    , new XElement("mobile", member.Mobile == null ? string.Empty: member.Mobile)                 
                    , new XElement("datejoin", member.MembershipDT)
                    , new XElement("membertype", member.MemberType)             
                    , new XElement("status", member.Status)
                    , new XElement("addressline1", member.AddressLine1)
                    , new XElement("addressline2", member.AddressLine2)
                    , new XElement("addressline3", member.AddressLine3)
                    , new XElement("postcode", member.PostCode)
                    , new XElement("country", member.Country)
                   );
              
                m_Document.Root.Add(item);
            }

            this.SaveDocument();
        }

        protected override void SaveDocument()
        {
            var xmlWriterSettings = new XmlWriterSettings()
            {
                CheckCharacters = false,
                Indent = true,
            };

            using (MemoryStream ms = new MemoryStream())
            using (var xmlWriter = XmlWriter.Create(ms, xmlWriterSettings))
            {
                m_Document.Save(xmlWriter);
                xmlWriter.Flush();
                xmlWriter.Close();

                this.Content = ms;
            }
        }
    }

}
namespace Suftnet.Cos.Web.Command
{
    using System.Xml.Linq;
    using System.Xml;

    using Suftnet.Cos.DataAccess;
    using Core;
    using System.IO;

    public class ExportTenantCommand : Export, ICommand
    {
        private XDocument m_Document;
        private readonly ITenant _tenant;
        public ExportTenantCommand(ITenant tenant)
        {
            _tenant = tenant;

            this.CreateDocument();
        }
        public MemoryStream Content { get; set; }
        protected override string Name
        {
            get { return "Tenant"; }
        }
        public override int TenantId { get; set; }
        protected override void CreateDocument()
        {
            m_Document = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            var root = new XElement("tenant");
            m_Document.Add(root);
        }
        public override void Dispose()
        {
            m_Document = null;
        }
        public void Execute()
        {
            var tenant = _tenant.Get(this.TenantId);

            var item = new XElement("tenant"
                    , new XElement("id", tenant.Id)
                    , new XElement("name", tenant.Name)             
                    , new XElement("email", tenant.Email)
                    , new XElement("mobile", tenant.Mobile)
                    , new XElement("telephone", tenant.Telephone == null ? string.Empty: tenant.Telephone)
                    , new XElement("whoweare", tenant.WhoWeAre ==  null ? string.Empty : tenant.WhoWeAre)
                    , new XElement("ourbelieve", tenant.OurBelieve == null ? string.Empty : tenant.OurBelieve)
                    , new XElement("address", tenant.CompleteAddress == null ? string.Empty : tenant.CompleteAddress)
                    , new XElement("town", tenant.Town)
                    , new XElement("county", tenant.County)
                    , new XElement("postcode", tenant.PostCode)
                    , new XElement("country", tenant.Country)
                   );           

            m_Document.Root.Add(item);

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
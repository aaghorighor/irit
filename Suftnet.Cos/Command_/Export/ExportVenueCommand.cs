namespace Suftnet.Cos.Web.Command
{
    using System.Xml.Linq;
    using System.Xml;

    using Suftnet.Cos.DataAccess;
    using Core;
    using System.IO;

    public class ExportVenueCommand : Export, ICommand
    {
        private XDocument m_Document;
        private readonly IVenue _venue;
        public ExportVenueCommand(IVenue venue)
        {
            _venue = venue;

            this.CreateDocument();
        }
        protected override string Name
        {
            get { return "Venue"; }
        }

        public MemoryStream Content { get; set; }
        public override int TenantId { get; set; }
        protected override void CreateDocument()
        {
            m_Document = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            var root = new XElement("Venues");
            m_Document.Add(root);
        }
        public override void Dispose()
        {
            m_Document = null;
        }
        public void Execute()
        {
            var venues = _venue.GetAll(this.TenantId);

            foreach (var venue in venues)
            {
                var item = new XElement("venue"
                    , new XElement("id", venue.Id)
                    , new XElement("location", venue.Company)
                    , new XElement("email", venue.Email)
                    , new XElement("phone", venue.Phone)            
                    , new XElement("addressline1", venue.AddressLine1)
                    , new XElement("addressline2", venue.AddressLine2)
                    , new XElement("addressline3", venue.AddressLine3)
                    , new XElement("postcode", venue.PostCode)
                    , new XElement("country", venue.Country)
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
           
            using(MemoryStream ms = new MemoryStream())
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
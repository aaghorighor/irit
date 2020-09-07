namespace Suftnet.Cos.Web.Command
{
    using System.Xml.Linq;
    using System.Xml;

    using Suftnet.Cos.DataAccess;
    using Core;
    using System.IO;

    public class ExportEventCommand : Export, ICommand
    {
        private XDocument m_Document;
        private readonly IEvent _event;
        public ExportEventCommand(IEvent @event)
        {
            _event = @event;
            this.CreateDocument();
        }
        protected override string Name
        {
            get { return "Event"; }
        }
        public override int TenantId { get; set; }
        public MemoryStream Content { get; set; }
        protected override void CreateDocument()
        {
            m_Document = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            var root = new XElement("events");
            m_Document.Add(root);
        }
        public override void Dispose()
        {
            m_Document = null;
        }
        public void Execute()
        {
            var events = _event.GetLatest(100, this.TenantId);

            foreach (var _event in events)
            {
                var item = new XElement("event"
                    , new XElement("id", _event.Id)
                    , new XElement("title", _event.Title)
                    , new XElement("eventtype", _event.EventType)    
                    , new XElement("startdate", _event.StartDt)
                    , new XElement("enddate", _event.EndDt)
                    , new XElement("venue", _event.Venue)
                    , new XElement("status", _event.Status)
                    , new XElement("addressline1", _event.AddressLine1)
                    , new XElement("addressline2", _event.AddressLine2)
                    , new XElement("addressline3", _event.AddressLine3)
                    , new XElement("postcode", _event.PostCode)
                    , new XElement("country", _event.Country)
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
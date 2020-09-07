namespace Suftnet.Cos.Web.Command
{
    using System.Xml.Linq;
    using System.Xml;

    using Suftnet.Cos.DataAccess; 
    using System.IO;

    public class ExportPledgeCommand : Export, ICommand
    {
        private XDocument m_Document;
        private readonly IPledge _pledge;
        public ExportPledgeCommand(IPledge pledge)
        {
            _pledge = pledge;
            this.CreateDocument();
        }
        protected override string Name
        {
            get { return "Pledge"; }
        }
        public override int TenantId { get; set; }
        public MemoryStream Content { get; set; }
        protected override void CreateDocument()
        {
            m_Document = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            var root = new XElement("Pledges");
            m_Document.Add(root);
        }
        public override void Dispose()
        {
            m_Document = null;
        }
        public void Execute()
        {
            var pledges = _pledge.GetAll(this.TenantId);

            foreach (var pledge in pledges)
            {
                var item = new XElement("pledge"
                    , new XElement("id", pledge.Id)
                    , new XElement("date", pledge.CreatedOn)
                    , new XElement("title", pledge.Title)
                    , new XElement("expected", pledge.Expected)                 
                    , new XElement("donated", pledge.Donated)
                    , new XElement("remaning", pledge.Remaning)
                    , new XElement("status", pledge.Status)
                    , new XElement("note", pledge.Note)
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
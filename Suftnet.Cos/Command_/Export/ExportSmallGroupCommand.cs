namespace Suftnet.Cos.Web.Command
{
    using System.Xml.Linq;
    using System.Xml;

    using Suftnet.Cos.DataAccess; 
    using System.IO;

    public class ExportSmallGroupCommand : Export, ICommand
    {
        private XDocument m_Document;
        private readonly ISmallGroup _smallGroup;
        public ExportSmallGroupCommand(ISmallGroup smallGroup)
        {
            _smallGroup = smallGroup;
            this.CreateDocument();
        }
        protected override string Name
        {
            get { return "Small Group"; }
        }
        public override int TenantId { get; set; }
        public MemoryStream Content { get; set; }
        protected override void CreateDocument()
        {
            m_Document = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            var root = new XElement("smallgroups");
            m_Document.Add(root);
        }
        public override void Dispose()
        {
            m_Document = null;
        }
        public void Execute()
        {
            var smallGroups = _smallGroup.GetAll(this.TenantId);

            foreach (var smallGroup in smallGroups)
            {
                //var item = new XElement("smallgroup"
                //    , new XElement("id", smallGroup.Id)
                //    , new XElement("title", smallGroup.Title)    
                //    , new XElement("firstname", smallGroup.FirstName)
                //    , new XElement("lastname", smallGroup.LastName)
                //    , new XElement("email", smallGroup.Email)
                //    , new XElement("phone", smallGroup.Phone)
                //    , new XElement("grouptype", smallGroup.MinistryType)                  
                //    , new XElement("role", smallGroup.Role)
                //    , new XElement("status", smallGroup.Status)                   
                //   );
           
                //m_Document.Root.Add(item);
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
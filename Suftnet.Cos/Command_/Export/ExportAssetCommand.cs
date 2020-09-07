namespace Suftnet.Cos.Web.Command
{
    using System.Xml.Linq;
    using System.Xml;

    using Suftnet.Cos.DataAccess; 
    using System.IO;

    public class ExportAssetCommand : Export, ICommand
    {
        private XDocument m_Document;
        private readonly IAsset _asset;
        public ExportAssetCommand(IAsset asset)
        {
            _asset = asset;
            this.CreateDocument();
        }
        protected override string Name
        {
            get { return "Asset"; }
        }
        public override int TenantId { get; set; }
        public MemoryStream Content { get; set; }
        protected override void CreateDocument()
        {
            m_Document = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            var root = new XElement("Assets");
            m_Document.Add(root);
        }
        public override void Dispose()
        {
            m_Document = null;
        }
        public void Execute()
        {
            var assets = _asset.GetAll(this.TenantId);

            foreach (var asset in assets)
            {
                var item = new XElement("asset"
                    , new XElement("id", asset.Id)
                    , new XElement("date", asset.CreatedOn)
                    , new XElement("assettype", asset.AssetType)
                    , new XElement("name", asset.Name)                 
                    , new XElement("description", asset.Description)
                    , new XElement("Cost", asset.Cost)
                    , new XElement("status", asset.Status)
                    , new XElement("reference", asset.Reference)
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
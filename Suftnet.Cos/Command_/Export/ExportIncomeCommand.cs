namespace Suftnet.Cos.Web.Command
{
    using System.Xml.Linq;
    using System.Xml;

    using Suftnet.Cos.DataAccess;
    using System.IO;

    public class ExportIncomeCommand : Export, ICommand
    {
        private XDocument m_Document;
        private readonly IIncome _income;
        public ExportIncomeCommand(IIncome income)
        {
            _income = income;
            this.CreateDocument();
        }
        protected override string Name
        {
            get { return "Income"; }
        }
        public override int TenantId { get; set; }
        public MemoryStream Content { get; set; }
        protected override void CreateDocument()
        {
            m_Document = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            var root = new XElement("Incomes");
            m_Document.Add(root);
        }
        public override void Dispose()
        {
            m_Document = null;
        }
        public void Execute()
        {
            var incomes = _income.GetAll(this.TenantId);

            foreach (var income in incomes)
            {
                var item = new XElement("income"
                    , new XElement("id", income.Id)
                    , new XElement("date", income.CreatedOn)
                    , new XElement("memberReference", income.MemberReference)
                    , new XElement("stripeReference", income.StripeReference)
                    , new XElement("incometype", income.IncomeType)
                    , new XElement("giftAid", income.GiftAid == true ? "Yes" : "No")
                    , new XElement("amount", income.Amount)                                    
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
namespace Suftnet.Cos.Web.Command
{
    using System.Xml.Linq;
    using System.Xml;

    using Suftnet.Cos.DataAccess;
    using Core;
    using System.IO;

    public class ExportAttendanceCommand : Export, ICommand
    {
        private XDocument m_Document;
        private readonly IAttendance _attendance;
        public ExportAttendanceCommand(IAttendance attendance)
        {
            _attendance = attendance;
            this.CreateDocument();
        }
        protected override string Name
        {
            get { return "Attendance"; }
        }
        public override int TenantId { get; set; }
        public MemoryStream Content { get; set; }
        protected override void CreateDocument()
        {
            m_Document = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            var root = new XElement("Attendances");
            m_Document.Add(root);
        }
        public override void Dispose()
        {
            m_Document = null;
        }
        public void Execute()
        {
            var attendances = _attendance.GetAll(this.TenantId);

            foreach (var attendance in attendances)
            {
                var item = new XElement("attendance"
                    , new XElement("id", attendance.Id)
                    , new XElement("date", attendance.CreatedOn)
                    , new XElement("eventtype", attendance.EventType)                
                    , new XElement("count", attendance.count)                   
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
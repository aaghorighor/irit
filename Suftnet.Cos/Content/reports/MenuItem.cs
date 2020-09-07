using System;
using System.Drawing;

using System.Data;
using Stimulsoft.Controls;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Dialogs;
using Stimulsoft.Report.Components;

namespace Reports
{
    public class Suftnet_Menu : Stimulsoft.Report.StiReport
    {
        public Suftnet_Menu()        {
            this.InitializeComponent();
        }

        #region StiReport Designer generated code - do not modify
        public Stimulsoft.Report.Dictionary.StiDataRelation ParentMc;
        public Stimulsoft.Report.Components.StiPage MenuReport;
        public Stimulsoft.Report.Components.StiReportTitleBand Menus;
        public Stimulsoft.Report.Components.StiText Text1;
        public Stimulsoft.Report.Components.StiText company;
        public Stimulsoft.Report.Components.StiText invoice;
        public Stimulsoft.Report.Components.StiText email;
        public Stimulsoft.Report.Components.StiText mobile;
        public Stimulsoft.Report.Components.StiText address;
        public Stimulsoft.Report.Components.StiDataBand DataBand2;
        public Stimulsoft.Report.Components.StiText Text32;
        public Stimulsoft.Report.Components.StiText Text33;
        public Stimulsoft.Report.Components.StiText Text35;
        public Stimulsoft.Report.Components.StiText Text36;
        public Stimulsoft.Report.Components.StiText Text12;
        public Stimulsoft.Report.Components.StiText Text3;
        public Stimulsoft.Report.Components.StiText Text17;
        public Stimulsoft.Report.Components.StiDataBand DataBand1;
        public Stimulsoft.Report.Components.StiText Text37;
        public Stimulsoft.Report.Components.StiText Text39;
        public Stimulsoft.Report.Components.StiText Text40;
        public Stimulsoft.Report.Components.StiText Text41;
        public Stimulsoft.Report.Components.StiText Text13;
        public Stimulsoft.Report.Components.StiText Text5;
        public Stimulsoft.Report.Components.StiFooterBand FooterBand1;
        public Stimulsoft.Report.Components.StiText Text6;
        public Stimulsoft.Report.Dictionary.StiCountFunctionService Text6_Count;
        public Stimulsoft.Report.Components.StiWatermark MenuReport_Watermark;
        public Stimulsoft.Report.Print.StiPrinterSettings Suftnet_Menu_PrinterSettings;
        public CategoryDataSource Category;
        public MenuDataSource Menu;
        public SettingBusinessObject Setting;
        
        public void Text1__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text1
            e.Value = "Menu Items";
        }
        
        public void company__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text company
            e.Value = ToString(sender, Setting.Company, true);
        }
        
        public void invoice__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text invoice
            e.Value = "Printed : " + ToString(sender, Today.ToString("d"), true);
        }
        
        public void email__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text email
            e.Value = ToString(sender, Setting.Email, true);
        }
        
        public void mobile__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text mobile
            e.Value = ToString(sender, Setting.Mobile, true);
        }
        
        public void address__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text address
            e.Value = ToString(sender, Setting.FullAddress, true);
        }
        
        public void Text32__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text32
            e.Value = "Item Name";
        }
        
        public void Text33__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text33
            e.Value = "Unit";
        }
        
        public void Text35__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text35
            e.Value = "Description";
        }
        
        public void Text36__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text36
            e.Value = "Sn#";
        }
        
        public void Text12__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text12
            e.Value = "Price";
        }
        
        public void Text3__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text3
            e.Value = "Quantity";
        }
        
        public void Text17__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text17
            e.Value = ToString(sender, Category.Name, true);
        }
        
        public void Text37__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text37
            e.Value = ToString(sender, Menu.Name, true);
        }
        
        public void Text39__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text39
            e.Value = ToString(sender, Menu.Unit, true);
        }
        
        public void Text40__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text40
            e.Value = ToString(sender, Setting.CurrencySymbol, true) + ToString(sender, Menu.Price, true);
        }
        
        public void Text41__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text41
            e.Value = ToString(sender, Line, true);
        }
        
        public void Text13__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text13
            e.Value = ToString(sender, Menu.Description, true);
        }
        
        public void Text5__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text5
            e.Value = ToString(sender, Menu.Quantity, true);
        }
        
        public void Text6__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "#%#Count: {Count()}";
            e.StoreToPrinted = true;
        }
        
        public string Text6_GetValue_End(Stimulsoft.Report.Components.StiComponent sender)
        {
            // CheckerInfo: Text Text6
            return "Count: " + ToString(sender, ((long)(StiReport.ChangeType(this.Text6_Count.GetValue(), typeof(long), true))), true);
        }
        
        public void DataBand1__BeginRender(object sender, System.EventArgs e)
        {
            this.Text6_Count.Init();
            this.Text6.TextValue = "";
        }
        
        public void DataBand1__EndRender(object sender, System.EventArgs e)
        {
            this.Text6.SetText(new Stimulsoft.Report.Components.StiGetValue(this.Text6_GetValue_End));
        }
        
        public void DataBand1__Rendering(object sender, System.EventArgs e)
        {
            // CheckerInfo: Text Text6
            try
            {
                this.Text6_Count.CalcItem(null);
            }
            catch (System.Exception ex)
            {
                StiLogService.Write(this.GetType(), "DataBand1 RenderingEvent Text6_Count ...ERROR");
                StiLogService.Write(this.GetType(), ex);
                this.WriteToReportRenderingMessages("Text6_Count " + ex.Message);
            }
        }
        
        private void InitializeComponent()
        {
            this.Setting = new SettingBusinessObject();
            this.Menu = new MenuDataSource();
            this.Category = new CategoryDataSource();
            this.ParentMc = new Stimulsoft.Report.Dictionary.StiDataRelation("MenuCategory", "Mc", "Mc", this.Category, this.Menu, new string[] {
                        "Id"}, new string[] {
                        "CategoryId"});
            this.NeedsCompiling = false;
            this.Text6_Count = new Stimulsoft.Report.Dictionary.StiCountFunctionService();
            this.EngineVersion = Stimulsoft.Report.Engine.StiEngineVersion.EngineV2;
            this.ReferencedAssemblies = new string[] {
                    "System.Dll",
                    "System.Drawing.Dll",
                    "System.Windows.Forms.Dll",
                    "System.Data.Dll",
                    "System.Xml.Dll",
                    "Stimulsoft.Controls.Dll",
                    "Stimulsoft.Base.Dll",
                    "Stimulsoft.Report.Dll"};
            this.ReportAlias = "Suftnet.Menu";
            // 
            // ReportChanged
            // 
            this.ReportChanged = new DateTime(2019, 9, 7, 14, 39, 48, 789);
            // 
            // ReportCreated
            // 
            this.ReportCreated = new DateTime(2013, 4, 12, 23, 56, 34, 0);
            this.ReportFile = "C:\\SUT-DV-200\\Git\\Suftnet.eRestaurant.Package\\Suftnet.eRestaurant.SaaS.1.0.0\\Suftnet.Cos\\Content\\Reports\\MenuItem.mrt";
            this.ReportGuid = "0e0089a19acf44e78a52b7a7ea46c753";
            this.ReportName = "Suftnet.Menu";
            this.ReportUnit = Stimulsoft.Report.StiReportUnitType.Centimeters;
            this.ReportVersion = "2015.1.0";
            this.ScriptLanguage = Stimulsoft.Report.StiReportLanguageType.CSharp;
            // 
            // MenuReport
            // 
            this.MenuReport = new Stimulsoft.Report.Components.StiPage();
            this.MenuReport.Guid = "0127838d9d7f4e7d99a3c20e35eef028";
            this.MenuReport.Name = "MenuReport";
            this.MenuReport.Orientation = Stimulsoft.Report.Components.StiPageOrientation.Landscape;
            this.MenuReport.PageHeight = 21;
            this.MenuReport.PageWidth = 29.7;
            this.MenuReport.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 2, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.MenuReport.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Menus
            // 
            this.Menus = new Stimulsoft.Report.Components.StiReportTitleBand();
            this.Menus.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.4, 27.7, 3.4);
            this.Menus.Name = "Menus";
            this.Menus.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Menus.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text1
            // 
            this.Text1 = new Stimulsoft.Report.Components.StiText();
            this.Text1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 18.2, 0.8);
            this.Text1.Name = "Text1";
            this.Text1.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text1__GetValue);
            this.Text1.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Bottom, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text1.Font = new System.Drawing.Font("Arial", 19F);
            this.Text1.Guid = null;
            this.Text1.Indicator = null;
            this.Text1.Interaction = null;
            this.Text1.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text1.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 127, 127, 127));
            this.Text1.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // company
            // 
            this.company = new Stimulsoft.Report.Components.StiText();
            this.company.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(18.2, 0, 9.5, 0.8);
            this.company.Guid = "e2abc029760d432491d93d8cf77d4581";
            this.company.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.company.Name = "company";
            this.company.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.company__GetValue);
            this.company.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.company.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Bottom, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.company.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.company.Font = new System.Drawing.Font("Arial", 19F);
            this.company.Indicator = null;
            this.company.Interaction = null;
            this.company.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.company.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 127, 127, 127));
            this.company.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // invoice
            // 
            this.invoice = new Stimulsoft.Report.Components.StiText();
            this.invoice.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1.2, 6.4, 0.6);
            this.invoice.Guid = "e536091acbaa46aba152c0ec76661775";
            this.invoice.Name = "invoice";
            this.invoice.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.invoice__GetValue);
            this.invoice.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.invoice.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.invoice.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.invoice.Font = new System.Drawing.Font("Arial", 12F);
            this.invoice.Indicator = null;
            this.invoice.Interaction = null;
            this.invoice.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.invoice.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 127, 127, 127));
            this.invoice.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // email
            // 
            this.email = new Stimulsoft.Report.Components.StiText();
            this.email.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(21.8, 1.8, 5.9, 0.6);
            this.email.Guid = "326d64ba4045485489dad197a9031c72";
            this.email.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.email.Name = "email";
            this.email.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.email__GetValue);
            this.email.Type = Stimulsoft.Report.Components.StiSystemTextType.DataColumn;
            this.email.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.email.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.email.Font = new System.Drawing.Font("Arial", 12F);
            this.email.Indicator = null;
            this.email.Interaction = null;
            this.email.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.email.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 127, 127, 127));
            this.email.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // mobile
            // 
            this.mobile = new Stimulsoft.Report.Components.StiText();
            this.mobile.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(21.8, 1.2, 5.9, 0.6);
            this.mobile.Guid = "8bbd725a8ee240bd8dc7c13b2b7aeb2d";
            this.mobile.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.mobile.Name = "mobile";
            this.mobile.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.mobile__GetValue);
            this.mobile.Type = Stimulsoft.Report.Components.StiSystemTextType.DataColumn;
            this.mobile.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.mobile.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.mobile.Font = new System.Drawing.Font("Arial", 12F);
            this.mobile.Indicator = null;
            this.mobile.Interaction = null;
            this.mobile.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.mobile.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 127, 127, 127));
            this.mobile.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // address
            // 
            this.address = new Stimulsoft.Report.Components.StiText();
            this.address.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 2.4, 27.7, 0.6);
            this.address.Guid = "13cea5914a5b41078c61eb1a2de85abe";
            this.address.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.address.Name = "address";
            this.address.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.address__GetValue);
            this.address.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.address.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Bottom, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.address.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.address.Font = new System.Drawing.Font("Arial", 12F);
            this.address.Indicator = null;
            this.address.Interaction = null;
            this.address.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.address.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 127, 127, 127));
            this.address.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.Menus.Guid = null;
            this.Menus.Interaction = null;
            // 
            // DataBand2
            // 
            this.DataBand2 = new Stimulsoft.Report.Components.StiDataBand();
            this.DataBand2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 4.6, 27.7, 1.6);
            this.DataBand2.DataSourceName = "Category";
            this.DataBand2.Name = "DataBand2";
            this.DataBand2.Sort = new string[0];
            this.DataBand2.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.DataBand2.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.DataBand2.BusinessObjectGuid = null;
            // 
            // Text32
            // 
            this.Text32 = new Stimulsoft.Report.Components.StiText();
            this.Text32.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1, 0.8, 5, 0.6);
            this.Text32.Guid = "6c6bfff008f244bf86350b41bac86f5e";
            this.Text32.Name = "Text32";
            this.Text32.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text32__GetValue);
            this.Text32.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text32.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text32.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 38, 38, 38), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text32.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 216, 216, 216));
            this.Text32.Font = new System.Drawing.Font("Arial", 12F);
            this.Text32.Indicator = null;
            this.Text32.Interaction = null;
            this.Text32.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text32.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text32.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text33
            // 
            this.Text33 = new Stimulsoft.Report.Components.StiText();
            this.Text33.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(21.2, 0.8, 2.8, 0.6);
            this.Text33.Guid = "bbe4a4c9672d488fa0c7edee106c553c";
            this.Text33.Name = "Text33";
            this.Text33.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text33__GetValue);
            this.Text33.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text33.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text33.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 38, 38, 38), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text33.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 216, 216, 216));
            this.Text33.Font = new System.Drawing.Font("Arial", 12F);
            this.Text33.Indicator = null;
            this.Text33.Interaction = null;
            this.Text33.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text33.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text33.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text35
            // 
            this.Text35 = new Stimulsoft.Report.Components.StiText();
            this.Text35.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(6, 0.8, 12.4, 0.6);
            this.Text35.Guid = "8e439f698057454285e4be9a702df0f5";
            this.Text35.Name = "Text35";
            this.Text35.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text35__GetValue);
            this.Text35.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text35.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text35.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 38, 38, 38), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text35.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 216, 216, 216));
            this.Text35.Font = new System.Drawing.Font("Arial", 12F);
            this.Text35.Indicator = null;
            this.Text35.Interaction = null;
            this.Text35.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text35.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text35.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text36
            // 
            this.Text36 = new Stimulsoft.Report.Components.StiText();
            this.Text36.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.8, 1, 0.6);
            this.Text36.Guid = "d162f29a53bb49979c855ce90e17e710";
            this.Text36.Name = "Text36";
            this.Text36.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text36__GetValue);
            this.Text36.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text36.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text36.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 38, 38, 38), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text36.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 216, 216, 216));
            this.Text36.Font = new System.Drawing.Font("Arial", 11F);
            this.Text36.Indicator = null;
            this.Text36.Interaction = null;
            this.Text36.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text36.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text36.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text12
            // 
            this.Text12 = new Stimulsoft.Report.Components.StiText();
            this.Text12.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(24, 0.8, 3.7, 0.6);
            this.Text12.Guid = "b5f5c50ab86849f0a7a1be2d8bb183d4";
            this.Text12.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Text12.Name = "Text12";
            this.Text12.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text12__GetValue);
            this.Text12.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text12.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text12.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 38, 38, 38), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text12.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 216, 216, 216));
            this.Text12.Font = new System.Drawing.Font("Arial", 12F);
            this.Text12.Indicator = null;
            this.Text12.Interaction = null;
            this.Text12.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text12.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text12.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text3
            // 
            this.Text3 = new Stimulsoft.Report.Components.StiText();
            this.Text3.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(18.4, 0.8, 2.8, 0.6);
            this.Text3.Guid = "dbed115fac33414eba2ee2db5e42d658";
            this.Text3.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Text3.Name = "Text3";
            this.Text3.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text3__GetValue);
            this.Text3.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text3.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text3.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 38, 38, 38), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text3.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 216, 216, 216));
            this.Text3.Font = new System.Drawing.Font("Arial", 10F);
            this.Text3.Indicator = null;
            this.Text3.Interaction = null;
            this.Text3.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text3.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text3.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text17
            // 
            this.Text17 = new Stimulsoft.Report.Components.StiText();
            this.Text17.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 19, 0.6);
            this.Text17.ComponentStyle = "Header2";
            this.Text17.Guid = "d3ce59ee9c8b4074b1599f44a4308cd7";
            this.Text17.Name = "Text17";
            this.Text17.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text17__GetValue);
            this.Text17.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text17.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text17.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.FromArgb(255, 140, 140, 140), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text17.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text17.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.Text17.Indicator = null;
            this.Text17.Interaction = null;
            this.Text17.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text17.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 193, 152, 89));
            this.Text17.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.DataBand2.DataRelationName = null;
            this.DataBand2.Guid = null;
            this.DataBand2.Interaction = null;
            this.DataBand2.MasterComponent = null;
            // 
            // DataBand1
            // 
            this.DataBand1 = new Stimulsoft.Report.Components.StiDataBand();
            this.DataBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 7, 27.7, 0.6);
            this.DataBand1.DataRelationName = "MenuCategory";
            this.DataBand1.DataSourceName = "Menu";
            this.DataBand1.Name = "DataBand1";
            this.DataBand1.Sort = new string[0];
            this.DataBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.DataBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text37
            // 
            this.Text37 = new Stimulsoft.Report.Components.StiText();
            this.Text37.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1, 0, 5, 0.6);
            this.Text37.Guid = "51479a52b49e4b8fb6c936d07ba13b29";
            this.Text37.Name = "Text37";
            this.Text37.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text37__GetValue);
            this.Text37.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text37.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text37.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Dot, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text37.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.White);
            this.Text37.Font = new System.Drawing.Font("Arial", 12F);
            this.Text37.Indicator = null;
            this.Text37.Interaction = null;
            this.Text37.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text37.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text37.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text39
            // 
            this.Text39 = new Stimulsoft.Report.Components.StiText();
            this.Text39.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(21.2, 0, 2.8, 0.6);
            this.Text39.Guid = "6b57797da1b1425383b5bebc1b73360c";
            this.Text39.Name = "Text39";
            this.Text39.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text39__GetValue);
            this.Text39.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text39.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text39.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Dot, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text39.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.White);
            this.Text39.Font = new System.Drawing.Font("Arial", 12F);
            this.Text39.Indicator = null;
            this.Text39.Interaction = null;
            this.Text39.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text39.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text39.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text40
            // 
            this.Text40 = new Stimulsoft.Report.Components.StiText();
            this.Text40.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(24, 0, 3.7, 0.6);
            this.Text40.Guid = "10c4ff4bc5c14a87b73ced5f39265daf";
            this.Text40.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Text40.Name = "Text40";
            this.Text40.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text40__GetValue);
            this.Text40.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text40.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text40.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Dot, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text40.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.White);
            this.Text40.Font = new System.Drawing.Font("Arial", 12F);
            this.Text40.Indicator = null;
            this.Text40.Interaction = null;
            this.Text40.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text40.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text40.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text41
            // 
            this.Text41 = new Stimulsoft.Report.Components.StiText();
            this.Text41.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 1, 0.6);
            this.Text41.Guid = "3eb72e8d025a4c40a1f7a104ffd06e62";
            this.Text41.Name = "Text41";
            this.Text41.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text41__GetValue);
            this.Text41.Type = Stimulsoft.Report.Components.StiSystemTextType.SystemVariables;
            this.Text41.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text41.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text41.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.White);
            this.Text41.Font = new System.Drawing.Font("Arial", 12F);
            this.Text41.Indicator = null;
            this.Text41.Interaction = null;
            this.Text41.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text41.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text41.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text13
            // 
            this.Text13 = new Stimulsoft.Report.Components.StiText();
            this.Text13.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(6, 0, 12.4, 0.6);
            this.Text13.Guid = "9da38958bc884e2faa3b76308c3223ab";
            this.Text13.Name = "Text13";
            this.Text13.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text13__GetValue);
            this.Text13.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text13.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text13.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Dot, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text13.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.White);
            this.Text13.Font = new System.Drawing.Font("Arial", 11F);
            this.Text13.Indicator = null;
            this.Text13.Interaction = null;
            this.Text13.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text13.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text13.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text5
            // 
            this.Text5 = new Stimulsoft.Report.Components.StiText();
            this.Text5.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(18.4, 0, 2.9, 0.6);
            this.Text5.Guid = "51db3cd6075b4adda143c7f6c3779273";
            this.Text5.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Text5.Name = "Text5";
            this.Text5.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text5__GetValue);
            this.Text5.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text5.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text5.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Dot, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text5.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.White);
            this.Text5.Font = new System.Drawing.Font("Arial", 12F);
            this.Text5.Indicator = null;
            this.Text5.Interaction = null;
            this.Text5.Margins = new Stimulsoft.Report.Components.StiMargins(4, 4, 4, 4);
            this.Text5.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text5.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.DataBand1.Guid = null;
            this.DataBand1.Interaction = null;
            // 
            // FooterBand1
            // 
            this.FooterBand1 = new Stimulsoft.Report.Components.StiFooterBand();
            this.FooterBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 8.4, 27.7, 1);
            this.FooterBand1.Name = "FooterBand1";
            this.FooterBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.FooterBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text6
            // 
            this.Text6 = new Stimulsoft.Report.Components.StiText();
            this.Text6.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(24.4, 0.2, 3.2, 0.6);
            this.Text6.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Text6.Name = "Text6";
            // 
            // Text6_Count
            // 
            this.Text6.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text6__GetValue);
            this.Text6.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text6.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.White, 1, Stimulsoft.Base.Drawing.StiPenStyle.Dot, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text6.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.White);
            this.Text6.Font = new System.Drawing.Font("Arial", 12F);
            this.Text6.Guid = null;
            this.Text6.Indicator = null;
            this.Text6.Interaction = null;
            this.Text6.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text6.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 89, 89, 89));
            this.Text6.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.FooterBand1.Guid = null;
            this.FooterBand1.Interaction = null;
            this.MenuReport.ExcelSheetValue = null;
            this.MenuReport.Interaction = null;
            this.MenuReport.Margins = new Stimulsoft.Report.Components.StiMargins(1, 1, 1, 1);
            this.MenuReport_Watermark = new Stimulsoft.Report.Components.StiWatermark();
            this.MenuReport_Watermark.Font = new System.Drawing.Font("Arial", 100F);
            this.MenuReport_Watermark.Image = null;
            this.MenuReport_Watermark.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 0));
            this.Suftnet_Menu_PrinterSettings = new Stimulsoft.Report.Print.StiPrinterSettings();
            this.PrinterSettings = this.Suftnet_Menu_PrinterSettings;
            this.MenuReport.Report = this;
            this.MenuReport.Watermark = this.MenuReport_Watermark;
            this.Menus.Page = this.MenuReport;
            this.Menus.Parent = this.MenuReport;
            this.Text1.Page = this.MenuReport;
            this.Text1.Parent = this.Menus;
            this.company.Page = this.MenuReport;
            this.company.Parent = this.Menus;
            this.invoice.Page = this.MenuReport;
            this.invoice.Parent = this.Menus;
            this.email.Page = this.MenuReport;
            this.email.Parent = this.Menus;
            this.mobile.Page = this.MenuReport;
            this.mobile.Parent = this.Menus;
            this.address.Page = this.MenuReport;
            this.address.Parent = this.Menus;
            this.DataBand2.Page = this.MenuReport;
            this.DataBand2.Parent = this.MenuReport;
            this.Text32.Page = this.MenuReport;
            this.Text32.Parent = this.DataBand2;
            this.Text33.Page = this.MenuReport;
            this.Text33.Parent = this.DataBand2;
            this.Text35.Page = this.MenuReport;
            this.Text35.Parent = this.DataBand2;
            this.Text36.Page = this.MenuReport;
            this.Text36.Parent = this.DataBand2;
            this.Text12.Page = this.MenuReport;
            this.Text12.Parent = this.DataBand2;
            this.Text3.Page = this.MenuReport;
            this.Text3.Parent = this.DataBand2;
            this.Text17.Page = this.MenuReport;
            this.Text17.Parent = this.DataBand2;
            this.DataBand1.MasterComponent = this.DataBand2;
            this.DataBand1.Page = this.MenuReport;
            this.DataBand1.Parent = this.MenuReport;
            this.Text37.Page = this.MenuReport;
            this.Text37.Parent = this.DataBand1;
            this.Text39.Page = this.MenuReport;
            this.Text39.Parent = this.DataBand1;
            this.Text40.Page = this.MenuReport;
            this.Text40.Parent = this.DataBand1;
            this.Text41.Page = this.MenuReport;
            this.Text41.Parent = this.DataBand1;
            this.Text13.Page = this.MenuReport;
            this.Text13.Parent = this.DataBand1;
            this.Text5.Page = this.MenuReport;
            this.Text5.Parent = this.DataBand1;
            this.FooterBand1.Page = this.MenuReport;
            this.FooterBand1.Parent = this.MenuReport;
            this.Text6.Page = this.MenuReport;
            this.Text6.Parent = this.FooterBand1;
            this.DataBand1.BeginRender += new System.EventHandler(this.DataBand1__BeginRender);
            this.DataBand1.EndRender += new System.EventHandler(this.DataBand1__EndRender);
            this.DataBand1.Rendering += new System.EventHandler(this.DataBand1__Rendering);
            this.AggregateFunctions = new object[] {
                    this.Text6_Count};
            // 
            // Add to Menus.Components
            // 
            this.Menus.Components.Clear();
            this.Menus.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text1,
                        this.company,
                        this.invoice,
                        this.email,
                        this.mobile,
                        this.address});
            // 
            // Add to DataBand2.Components
            // 
            this.DataBand2.Components.Clear();
            this.DataBand2.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text32,
                        this.Text33,
                        this.Text35,
                        this.Text36,
                        this.Text12,
                        this.Text3,
                        this.Text17});
            // 
            // Add to DataBand1.Components
            // 
            this.DataBand1.Components.Clear();
            this.DataBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text37,
                        this.Text39,
                        this.Text40,
                        this.Text41,
                        this.Text13,
                        this.Text5});
            // 
            // Add to FooterBand1.Components
            // 
            this.FooterBand1.Components.Clear();
            this.FooterBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text6});
            // 
            // Add to MenuReport.Components
            // 
            this.MenuReport.Components.Clear();
            this.MenuReport.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Menus,
                        this.DataBand2,
                        this.DataBand1,
                        this.FooterBand1});
            // 
            // Add to Pages
            // 
            this.Pages.Clear();
            this.Pages.AddRange(new Stimulsoft.Report.Components.StiPage[] {
                        this.MenuReport});
            this.Dictionary.Relations.Add(this.ParentMc);
            this.Category.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flag", "flag", "flag", typeof(sbyte), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("TenantId", "TenantId", "TenantId", typeof(sbyte), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CreatedDT", "CreatedDT", "CreatedDT", typeof(DateTime), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Id", "Id", "Id", typeof(short), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Name", "Name", "Name", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Status", "Status", "Status", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("IndexNo", "IndexNo", "IndexNo", typeof(sbyte), null)});
            this.DataSources.Add(this.Category);
            this.Menu.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flag", "flag", "flag", typeof(sbyte), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("TenantId", "TenantId", "TenantId", typeof(sbyte), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CreatedDT", "CreatedDT", "CreatedDT", typeof(DateTime), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CreatedOn", "CreatedOn", "CreatedOn", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CreatedBy", "CreatedBy", "CreatedBy", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Id", "Id", "Id", typeof(short), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Name", "Name", "Name", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Description", "Description", "Description", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CategoryId", "CategoryId", "CategoryId", typeof(short), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("DiscountId", "DiscountId", "DiscountId", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Category", "Category", "Category", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("UnitId", "UnitId", "UnitId", typeof(short), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Unit", "Unit", "Unit", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Price", "Price", "Price", typeof(float), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Quantity", "Quantity", "Quantity", typeof(sbyte), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("TaxId", "TaxId", "TaxId", typeof(sbyte), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("SubStractId", "SubStractId", "SubStractId", typeof(short), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CutOff", "CutOff", "CutOff", typeof(sbyte), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("ImageUrl", "ImageUrl", "ImageUrl", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Active", "Active", "Active", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("IsKitchen", "IsKitchen", "IsKitchen", typeof(string), null)});
            this.DataSources.Add(this.Menu);
            this.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiXmlDatabase("MenuConnection", "", "", null, Stimulsoft.Report.StiXmlType.Xml));
            this.Setting.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Active", "Active", "Active", typeof(bool), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("AddressId", "AddressId", "AddressId", typeof(int), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("AddressLine", "AddressLine", "AddressLine", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("AddressLine1", "AddressLine1", "AddressLine1", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("AddressLine2", "AddressLine2", "AddressLine2", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("AddressLine3", "AddressLine3", "AddressLine3", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Company", "Company", "Company", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CompleteAddress", "CompleteAddress", "CompleteAddress", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Country", "Country", "Country", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("County", "County", "County", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CreatedBy", "CreatedBy", "CreatedBy", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CreatedDT", "CreatedDT", "CreatedDT", typeof(DateTime), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CreatedOn", "CreatedOn", "CreatedOn", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CurrencyId", "CurrencyId", "CurrencyId", typeof(int?), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CurrencySymbol", "CurrencySymbol", "CurrencySymbol", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("DateTimeFormat", "DateTimeFormat", "DateTimeFormat", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Description", "Description", "Description", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Email", "Email", "Email", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("ExternalId", "ExternalId", "ExternalId", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("ExternalTenantId", "ExternalTenantId", "ExternalTenantId", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flag", "flag", "flag", typeof(int), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("FullAddress", "FullAddress", "FullAddress", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Id", "Id", "Id", typeof(int), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Latitude", "Latitude", "Latitude", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Logitude", "Logitude", "Logitude", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Mobile", "Mobile", "Mobile", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Password", "Password", "Password", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Port", "Port", "Port", typeof(int?), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("PostCode", "PostCode", "PostCode", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Server", "Server", "Server", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("ServerEmail", "ServerEmail", "ServerEmail", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("TaxRate", "TaxRate", "TaxRate", typeof(decimal?), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Telephone", "Telephone", "Telephone", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("TenantId", "TenantId", "TenantId", typeof(int), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Town", "Town", "Town", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("UserName", "UserName", "UserName", typeof(string), null)});
            this.Dictionary.BusinessObjects.Add(this.Setting);
        }
        
        #region Relation ParentMc
        public class ParentMcRelation : Stimulsoft.Report.Dictionary.StiDataRow
        {
            
            public ParentMcRelation(Stimulsoft.Report.Dictionary.StiDataRow dataRow) : 
                    base(dataRow)
            {
            }
            
            public virtual sbyte flag
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["flag"], typeof(sbyte), true)));
                }
            }
            
            public virtual sbyte TenantId
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["TenantId"], typeof(sbyte), true)));
                }
            }
            
            public virtual DateTime CreatedDT
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["CreatedDT"], typeof(DateTime), true)));
                }
            }
            
            public virtual short Id
            {
                get
                {
                    return ((short)(StiReport.ChangeType(this["Id"], typeof(short), true)));
                }
            }
            
            public virtual string Name
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Name"], typeof(string), true)));
                }
            }
            
            public virtual string Status
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Status"], typeof(string), true)));
                }
            }
            
            public virtual sbyte IndexNo
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["IndexNo"], typeof(sbyte), true)));
                }
            }
        }
        #endregion Relation ParentMc
        
        #region DataSource Category
        public class CategoryDataSource : Stimulsoft.Report.Dictionary.StiDataTableSource
        {
            
            public CategoryDataSource() : 
                    base("MenuConnection.Category", "Category", "Category", null)
            {
            }
            
            public virtual sbyte flag
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["flag"], typeof(sbyte), true)));
                }
            }
            
            public virtual sbyte TenantId
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["TenantId"], typeof(sbyte), true)));
                }
            }
            
            public virtual DateTime CreatedDT
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["CreatedDT"], typeof(DateTime), true)));
                }
            }
            
            public virtual short Id
            {
                get
                {
                    return ((short)(StiReport.ChangeType(this["Id"], typeof(short), true)));
                }
            }
            
            public new virtual string Name
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Name"], typeof(string), true)));
                }
            }
            
            public virtual string Status
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Status"], typeof(string), true)));
                }
            }
            
            public virtual sbyte IndexNo
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["IndexNo"], typeof(sbyte), true)));
                }
            }
        }
        #endregion DataSource Category
        
        #region DataSource Menu
        public class MenuDataSource : Stimulsoft.Report.Dictionary.StiDataTableSource
        {
            
            public MenuDataSource() : 
                    base("MenuConnection.Menu", "Menu", "Menu", null)
            {
            }
            
            public virtual sbyte flag
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["flag"], typeof(sbyte), true)));
                }
            }
            
            public virtual sbyte TenantId
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["TenantId"], typeof(sbyte), true)));
                }
            }
            
            public virtual DateTime CreatedDT
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["CreatedDT"], typeof(DateTime), true)));
                }
            }
            
            public virtual string CreatedOn
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["CreatedOn"], typeof(string), true)));
                }
            }
            
            public virtual string CreatedBy
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["CreatedBy"], typeof(string), true)));
                }
            }
            
            public virtual short Id
            {
                get
                {
                    return ((short)(StiReport.ChangeType(this["Id"], typeof(short), true)));
                }
            }
            
            public new virtual string Name
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Name"], typeof(string), true)));
                }
            }
            
            public virtual string Description
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Description"], typeof(string), true)));
                }
            }
            
            public virtual short CategoryId
            {
                get
                {
                    return ((short)(StiReport.ChangeType(this["CategoryId"], typeof(short), true)));
                }
            }
            
            public virtual string DiscountId
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["DiscountId"], typeof(string), true)));
                }
            }
            
            public virtual string Category
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Category"], typeof(string), true)));
                }
            }
            
            public virtual short UnitId
            {
                get
                {
                    return ((short)(StiReport.ChangeType(this["UnitId"], typeof(short), true)));
                }
            }
            
            public virtual string Unit
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Unit"], typeof(string), true)));
                }
            }
            
            public virtual float Price
            {
                get
                {
                    return ((float)(StiReport.ChangeType(this["Price"], typeof(float), true)));
                }
            }
            
            public virtual sbyte Quantity
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["Quantity"], typeof(sbyte), true)));
                }
            }
            
            public virtual sbyte TaxId
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["TaxId"], typeof(sbyte), true)));
                }
            }
            
            public virtual short SubStractId
            {
                get
                {
                    return ((short)(StiReport.ChangeType(this["SubStractId"], typeof(short), true)));
                }
            }
            
            public virtual sbyte CutOff
            {
                get
                {
                    return ((sbyte)(StiReport.ChangeType(this["CutOff"], typeof(sbyte), true)));
                }
            }
            
            public virtual string ImageUrl
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["ImageUrl"], typeof(string), true)));
                }
            }
            
            public virtual string Active
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Active"], typeof(string), true)));
                }
            }
            
            public virtual string IsKitchen
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["IsKitchen"], typeof(string), true)));
                }
            }
            
            public virtual ParentMcRelation Mc
            {
                get
                {
                    return new ParentMcRelation(this.GetParentData("MenuCategory"));
                }
            }
        }
        #endregion DataSource Menu
        
        #region BusinessObject Setting
        public class SettingBusinessObject : Stimulsoft.Report.Dictionary.StiBusinessObject
        {
            
            public SettingBusinessObject() : 
                    base("Settings", "Setting", "Setting", "be44dfe141b347d9a4f11eeb30f15ca4", null)
            {
            }
            
            public virtual bool Active
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["Active"], typeof(bool), true)));
                }
            }
            
            public virtual int AddressId
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["AddressId"], typeof(int), true)));
                }
            }
            
            public virtual string AddressLine
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["AddressLine"], typeof(string), true)));
                }
            }
            
            public virtual string AddressLine1
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["AddressLine1"], typeof(string), true)));
                }
            }
            
            public virtual string AddressLine2
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["AddressLine2"], typeof(string), true)));
                }
            }
            
            public virtual string AddressLine3
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["AddressLine3"], typeof(string), true)));
                }
            }
            
            public virtual string Company
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Company"], typeof(string), true)));
                }
            }
            
            public virtual string CompleteAddress
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["CompleteAddress"], typeof(string), true)));
                }
            }
            
            public virtual string Country
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Country"], typeof(string), true)));
                }
            }
            
            public virtual string County
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["County"], typeof(string), true)));
                }
            }
            
            public virtual string CreatedBy
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["CreatedBy"], typeof(string), true)));
                }
            }
            
            public virtual DateTime CreatedDT
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["CreatedDT"], typeof(DateTime), true)));
                }
            }
            
            public virtual string CreatedOn
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["CreatedOn"], typeof(string), true)));
                }
            }
            
            public virtual int? CurrencyId
            {
                get
                {
                    return ((int?)(StiReport.ChangeType(this["CurrencyId"], typeof(int?), true)));
                }
            }
            
            public virtual string CurrencySymbol
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["CurrencySymbol"], typeof(string), true)));
                }
            }
            
            public virtual string DateTimeFormat
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["DateTimeFormat"], typeof(string), true)));
                }
            }
            
            public virtual string Description
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Description"], typeof(string), true)));
                }
            }
            
            public virtual string Email
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Email"], typeof(string), true)));
                }
            }
            
            public virtual string ExternalId
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["ExternalId"], typeof(string), true)));
                }
            }
            
            public virtual string ExternalTenantId
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["ExternalTenantId"], typeof(string), true)));
                }
            }
            
            public virtual int flag
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["flag"], typeof(int), true)));
                }
            }
            
            public virtual string FullAddress
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["FullAddress"], typeof(string), true)));
                }
            }
            
            public virtual int Id
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["Id"], typeof(int), true)));
                }
            }
            
            public virtual string Latitude
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Latitude"], typeof(string), true)));
                }
            }
            
            public virtual string Logitude
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Logitude"], typeof(string), true)));
                }
            }
            
            public virtual string Mobile
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Mobile"], typeof(string), true)));
                }
            }
            
            public virtual string Password
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Password"], typeof(string), true)));
                }
            }
            
            public virtual int? Port
            {
                get
                {
                    return ((int?)(StiReport.ChangeType(this["Port"], typeof(int?), true)));
                }
            }
            
            public virtual string PostCode
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["PostCode"], typeof(string), true)));
                }
            }
            
            public virtual string Server
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Server"], typeof(string), true)));
                }
            }
            
            public virtual string ServerEmail
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["ServerEmail"], typeof(string), true)));
                }
            }
            
            public virtual decimal? TaxRate
            {
                get
                {
                    return ((decimal?)(StiReport.ChangeType(this["TaxRate"], typeof(decimal?), true)));
                }
            }
            
            public virtual string Telephone
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Telephone"], typeof(string), true)));
                }
            }
            
            public virtual int TenantId
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["TenantId"], typeof(int), true)));
                }
            }
            
            public virtual string Town
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Town"], typeof(string), true)));
                }
            }
            
            public virtual string UserName
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["UserName"], typeof(string), true)));
                }
            }
        }
        #endregion BusinessObject Setting
        #endregion StiReport Designer generated code - do not modify
    }
}

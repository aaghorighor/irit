namespace Suftnet.Cos.Extension
{
    using Stripe;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web;
    using Suftnet.Cos.Web.Mapper;

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Text;
    using System.Text.RegularExpressions;

    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Mvc.Html;

    using Web.ViewModel;
    using System.IO;

    using System.Linq;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Web.Services.Interface;

    public static partial class ViewExtensions
    {       
        public static GlobalDto Settings(this ViewUserControl control)
        {
            return GeneralConfiguration.Configuration.Settings.General;
        }
      
        public static string CurrentUserName(this UrlHelper helper)
        {
            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.FullName).Select(x => x.Value).SingleOrDefault();
            }

            return "UnKnown User";
        }
        public static bool IsCurrentUserAdmin(this UrlHelper helper)
        {
            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var user = test.Claims.Where(x => x.Type == Identity.AreaId).Select(x => x.Value).SingleOrDefault();

                if (user == ((int)eArea.BackOffice).ToString())
                {
                    return true;
                }
            }

            return false;
        }
        
        public static string FullName(this Controller helper)
        {
            var test = ((ClaimsIdentity)helper.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.FullName).Select(x => x.Value).SingleOrDefault();
            }

            return "UnKnown User";
        }
     
        public static string TenantName(this UrlHelper helper)
        {
            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.TenantName).Select(x => x.Value).SingleOrDefault();
            }

            return "UnKnown Tenant";
        }
        public static string CurrentUser(this Controller helper)
        {
            var test = ((ClaimsIdentity)helper.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.FullName).Select(x => x.Value).SingleOrDefault();
            }

            return "UnKnown User";
        }
        public static string CurrentUser(this ActionExecutingContext helper)
        {
            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.FullName).Select(x => x.Value).SingleOrDefault();
            }

            return "UnKnown User";
        }

        public static string CompleteAddress(this UrlHelper helper)
        {
            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.CompleteAddress).Select(x => x.Value).SingleOrDefault();
            }

            return "UnKnown Tenant";
        }

        public static string DeliveryRate(this UrlHelper helper)
        {
            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.DeliveryRate).Select(x => x.Value).SingleOrDefault();
            }

            return "0";
        }

        public static string IsFlatRate(this UrlHelper helper)
        {
            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.IsFlatRate).Select(x => x.Value).SingleOrDefault();
            }

            return "0";
        }

        public static string DeliveryNote(this UrlHelper helper)
        {
            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.DeliveryNote).Select(x => x.Value).SingleOrDefault();
            }

            return "";
        }

        public static string FlatRate(this UrlHelper helper)
        {
            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.FlatRate).Select(x => x.Value).SingleOrDefault();
            }

            return "0";
        }

        public static string DeliveryUnit(this UrlHelper helper)
        {
            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                return test.Claims.Where(x => x.Type == Identity.DeliveryUnit).Select(x => x.Value).SingleOrDefault();
            }

            return "0";
        }

        public static int ToInt(this string value)
        {
            int output = 0;

            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }

            if (int.TryParse(value, out output)) 
            {
                return output;
            }

            return 0;
        }

        public static bool ToBoolean(this string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return false;
            }

            switch (value.ToLower())
            {
                case "true":
                    return true;
                case "t":
                    return true;
                case "1":
                    return true;
                case "0":
                    return false;
                case "false":
                    return false;
                case "f":
                    return false;
                default:
                    throw new InvalidCastException("You can't cast that value to a bool!");
            }
        }
        public static DateTime ToDateTime(this string datetime, char dateSpliter = '-', char timeSpliter = ':', char millisecondSpliter = ',')
        {
            try
            {
                if (string.IsNullOrEmpty(datetime))
                {
                    return DateTime.UtcNow;
                }

                datetime = datetime.Trim();
                datetime = datetime.Replace("  ", " ");
                string[] body = datetime.Split(' ');
                string[] date = body[0].Split(dateSpliter);
                int year = date[0].ToInt();
                int month = date[1].ToInt();
                int day = date[2].ToInt();
                int hour = 0, minute = 0, second = 0, millisecond = 0;
                if (body.Length == 2)
                {
                    string[] tpart = body[1].Split(millisecondSpliter);
                    string[] time = tpart[0].Split(timeSpliter);
                    hour = time[0].ToInt();
                    minute = time[1].ToInt();
                    if (time.Length == 3) second = time[2].ToInt();
                    if (tpart.Length == 2) millisecond = tpart[1].ToInt();
                }
                return new DateTime(year, month, day, hour, minute, second, millisecond);
            }
            catch
            {
                return new DateTime();
            }
        }

        public static DateTime ToDate(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DateTime.UtcNow;
            }

            return DateTime.Parse(value);
        }             
              
        public static TenantDto ToTenantSettings(this Controller helper, string currencySymbol)
        {
            var service = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var tenant = new TenantDto();
            var test = ((ClaimsIdentity)helper.HttpContext.User.Identity);

            if (test != null)
            {
                var tenantId = test.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                tenant = service.Get(tenantId.ToGuid());

                if (tenant != null)
                {                   
                    tenant.CurrencySymbol = currencySymbol;
                }
            }
                                  
            return tenant;
        }    

        public static MvcHtmlString AreaDropdown(this HtmlHelper helper, string elementId, string cssClassName)
        {
            var iCommon = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();
            var items = iCommon.GetAll((int)Suftnet.Cos.Common.eSettings.Area);

            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            foreach (var item in items)
            {
                if (!item.Active)
                {
                    continue;
                }

                if (item.code == null)
                {
                    continue;
                }

                if (item.code != "1")
                {
                    continue;
                }

                TagBuilder option = new TagBuilder("option");
                option.InnerHtml = item.Title;
                option.MergeAttribute("value", item.Id.ToString());

                if (!string.IsNullOrEmpty(item.code))
                {
                    option.MergeAttribute("code", item.code);
                }

                select.InnerHtml += option.ToString();
            }

            return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString AdminAreaDropdown(this HtmlHelper helper, string elementId, string cssClassName)
        {
            var iCommon = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();
            var items = iCommon.GetAll((int)Suftnet.Cos.Common.eSettings.Area);

            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            foreach (var item in items)
            {
                if (!item.Active)
                {
                    continue;
                }                              

                TagBuilder option = new TagBuilder("option");
                option.InnerHtml = item.Title;
                option.MergeAttribute("value", item.Id.ToString());

                if (!string.IsNullOrEmpty(item.code))
                {
                    option.MergeAttribute("code", item.code);
                }

                select.InnerHtml += option.ToString();
            }

            return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString AddonTypeDropdown(this HtmlHelper helper, string elementId, string cssClassName)
        {
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            var services = GeneralConfiguration.Configuration.DependencyResolver.GetService<IAddOnType>();
            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var tenantId = test.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                var items = services.GetAll(tenantId.ToGuid());

                foreach (var item in items)
                {
                    if (!item.Active)
                    {
                        continue;
                    }

                    TagBuilder option = new TagBuilder("option");
                    option.InnerHtml = item.Name;
                    option.MergeAttribute("value", item.Id.ToString());
                                     
                    select.InnerHtml += option.ToString();
                }


                return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
            }     

            return new MvcHtmlString("");
        }
        public static MvcHtmlString UnitDropdown(this HtmlHelper helper, string elementId, string cssClassName)
        {
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            var services = GeneralConfiguration.Configuration.DependencyResolver.GetService<IUnit>();
            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var tenantId = test.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                var items = services.GetAll(tenantId.ToGuid());

                foreach (var item in items)
                {
                    if (!item.Active)
                    {
                        continue;
                    }

                    TagBuilder option = new TagBuilder("option");
                    option.InnerHtml = item.Name;
                    option.MergeAttribute("value", item.Id.ToString());

                    select.InnerHtml += option.ToString();
                }


                return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
            }

            return new MvcHtmlString("");
        }
        public static MvcHtmlString TableDropdown(this HtmlHelper helper, string elementId, string cssClassName)
        {
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            var services = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITable>();
            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var tenantId = test.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                var items = services.GetAll(tenantId.ToGuid());

                foreach (var item in items)
                {
                    if (!item.Active)
                    {
                        continue;
                    }

                    TagBuilder option = new TagBuilder("option");
                    option.InnerHtml = item.Number;
                    option.MergeAttribute("value", item.Id.ToString());

                    select.InnerHtml += option.ToString();
                }

                return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
            }

            return new MvcHtmlString("");
        }

        public static MvcHtmlString CategoryDropdown(this HtmlHelper helper, string elementId, string cssClassName)
        {
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            var services = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICategory>();
            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var tenantId = test.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                var items = services.GetAll(tenantId.ToGuid());

                foreach (var item in items)
                {
                    if (!item.Status)
                    {
                        continue;
                    }

                    TagBuilder option = new TagBuilder("option");
                    option.InnerHtml = item.Name;
                    option.MergeAttribute("value", item.Id.ToString());

                    select.InnerHtml += option.ToString();
                }


                return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
            }

            return new MvcHtmlString("");
        }
        public static MvcHtmlString TaxDropdown(this HtmlHelper helper, string elementId, string cssClassName)
        {
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            var services = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITax>();
            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var tenantId = test.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                var items = services.GetAll(tenantId.ToUpper().ToGuid());

                foreach (var item in items)
                {
                    if (!item.Active)
                    {
                        continue;
                    }

                    TagBuilder option = new TagBuilder("option");
                    option.InnerHtml = item.Name;
                    option.MergeAttribute("value", item.Id.ToString());
                    option.MergeAttribute("rate", item.Rate.ToString());

                    select.InnerHtml += option.ToString();
                }


                return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
            }

            return new MvcHtmlString("");
        }
        public static MvcHtmlString DiscountDropdown(this HtmlHelper helper, string elementId, string cssClassName)
        {
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            var services = GeneralConfiguration.Configuration.DependencyResolver.GetService<IDiscount>();
            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var tenantId = test.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                var items = services.GetAll(tenantId.ToUpper().ToGuid());

                foreach (var item in items)
                {
                    if (!item.Active)
                    {
                        continue;
                    }

                    TagBuilder option = new TagBuilder("option");
                    option.InnerHtml = item.Name;
                    option.MergeAttribute("value", item.Id.ToString());
                    option.MergeAttribute("rate", item.Rate.ToString());

                    select.InnerHtml += option.ToString();
                }


                return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
            }

            return new MvcHtmlString("");
        }
        public static MvcHtmlString OrderStatusDropdown(this HtmlHelper helper, string elementId, string cssClassName)
        {
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            var services = GeneralConfiguration.Configuration.DependencyResolver.GetService<IOrderStatus>();
            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var tenantId = test.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                var items = services.GetAll();

                foreach (var item in items)
                {
                    if (!item.Active)
                    {
                        continue;
                    }

                    TagBuilder option = new TagBuilder("option");
                    option.InnerHtml = item.Name;
                    option.MergeAttribute("value", item.Id.ToString());

                    select.InnerHtml += option.ToString();
                }


                return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
            }

            return new MvcHtmlString("");
        }

        public static MvcHtmlString UserDropdown(this HtmlHelper helper, string elementId, string cssClassName)
        {
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            var services = GeneralConfiguration.Configuration.DependencyResolver.GetService<IUserAccount>();
            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var tenantId = test.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                var items = services.GetById(tenantId.ToGuid());

                foreach (var item in items)
                {
                    if (!item.Active)
                    {
                        continue;
                    }

                    TagBuilder option = new TagBuilder("option");
                    option.InnerHtml = item.FirstName +" " + item.LastName;
                    option.MergeAttribute("value", item.Id.ToString());

                    select.InnerHtml += option.ToString();
                }


                return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
            }

            return new MvcHtmlString("");
        }
        public static MvcHtmlString Dropdown(this HtmlHelper helper, string elementId, string cssClassName, int lookId)
        {
            var iCommon = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();
            var items = iCommon.GetAll(lookId);        

            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            foreach (var item in items) 
            {
                if (!item.Active)
                {
                    continue;
                }               

                TagBuilder option = new TagBuilder("option");
                option.InnerHtml = item.Title;
                option.MergeAttribute("value", item.Id.ToString());

                if (!string.IsNullOrEmpty(item.code))
                {
                    option.MergeAttribute("code", item.code);
                }
              
                select.InnerHtml += option.ToString();
            }

            return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
        }     
        public static MvcHtmlString CurrencyDropdown(this HtmlHelper helper, string elementId, string cssClassName, int lookId)
        {
            var iCommon = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();
            var items = iCommon.GetAll(lookId);

            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", elementId);
            select.MergeAttribute("name", elementId);
            select.AddCssClass(cssClassName);

            TagBuilder headerOption = new TagBuilder("option");
            headerOption.MergeAttribute("value", "");
            headerOption.InnerHtml = "-- SELECT --";
            select.InnerHtml += headerOption.ToString();

            foreach (var item in items) 
            {
                if (!item.Active)
                {
                    continue;
                }

                TagBuilder option = new TagBuilder("option");
                option.InnerHtml = item.code;
                option.MergeAttribute("code", item.code);
                option.MergeAttribute("value", item.Id.ToString());
                select.InnerHtml += option.ToString();
            }

            return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
        }      
        public static string StripePublicKey(this UrlHelper helper)
        {
            return GeneralConfiguration.Configuration.Settings.StripePublishableKey;
        }
        public static MvcHtmlString Content(this HtmlHelper helper, int contentId)
        {
            var template = GeneralConfiguration.Configuration.DependencyResolver.GetService<IEditor>();
            var contents = template.Get(contentId);

            TagBuilder div = new TagBuilder("div");

            if (contents != null)
            {
                TagBuilder header = new TagBuilder("h3");
                header.InnerHtml = contents.Title;

                TagBuilder content = new TagBuilder("p");
                content.InnerHtml = contents.Contents;

                div.InnerHtml = header.ToString(TagRenderMode.Normal) + "" + content.ToString(TagRenderMode.Normal);

                return new MvcHtmlString(div.ToString(TagRenderMode.Normal));
            }

            return new MvcHtmlString(div.ToString(TagRenderMode.Normal));
        }                   
        public static EditorDTO Cms(this object value, int contentId)
        {
            var iEditor = GeneralConfiguration.Configuration.DependencyResolver.GetService<IEditor>();
            return iEditor.Get(contentId);
        }
        public static string GetDateTimeformat(this object value)
        {
            var iGlobal = GeneralConfiguration.Configuration.DependencyResolver.GetService<IGlobal>();
            var iCommon = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();

            return iCommon.Get(Convert.ToInt32(iGlobal.Get().DateTimeFormat)).Title;
        }
        public static bool isValidEmail(this string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }        
        public static string UniqueId(this string uniqueId)
        {
            DateTime date = DateTime.Now;
            string uniqueID = String.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
            return uniqueID.Substring(1, 12);
        }              
        public static Task<int> TenantCount(this UrlHelper helper)
        {
            var iTenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();

            return System.Threading.Tasks.Task.Run(() => iTenant.Count(false));           
        }
        public static string StripHTML(this string HTMLText)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(HTMLText, "");
        }
        public static string ToEncrypt(this string key)
        {           
            return RsaService.Encrypt(key);
        }
        public static string ToDecrypt(this string key)
        {           
            return RsaService.Decrypt(key);
        }
        public static string RandomPassword(this object value, int count = 10)
        {
            var random = new Random();
            var result = string.Empty;
            for (int i = 2; i < count - 1; i++)
            {
                var number = random.Next(9);
                result += number.ToString();
            }
            return result;
        }
        public static string RandomCode(this object value)
        {
            var random = new Random();
            var result = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                var number = random.Next(9);
                result += number.ToString();
            }
            return result;
        }      
        public static IEnumerable<SliderDto> Slides(this UrlHelper helper)
        {
            var iSlider = GeneralConfiguration.Configuration.DependencyResolver.GetService<ISlider>();
            return iSlider.LoadSlides();
        }
        public static List<CommonDto> CommonList(this UrlHelper helper, int lookupId)
        {
            var iCommon = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();
            return iCommon.GetAll(lookupId);
        }
        public static CommonDto CommonDto(this UrlHelper helper, int lookupId)
        {
            var iCommon = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();
            return iCommon.Get(lookupId);
        }        
      
        public static byte[] ReadToEnd(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static string ArrayToString(this string[] array)
        {
            if(array == null)
            {
                return string.Empty;
            }
            
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(',');
            }
            return builder.ToString();
        }

        public static string ToShortDescription(this string description)
        {
            if(string.IsNullOrEmpty(description))
            {
                return string.Empty;
            }

            if (description.Length < 100)
            {
                return description + " ...";
            }

            if (description.Length > 100)
            {
                return description.Substring(0,100) + " ...";
            }

            return string.Empty;
        }

        public static string ToImage(this string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return string.Empty;
            }

            return imageUrl;
        }

        public static Guid ToGuid(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new Guid();
            }
           
            return new Guid(value);
        }

        public static string ReCAPTCHA(this HtmlHelper helper)
        {
            return GeneralConfiguration.Configuration.Settings.CaptchaSiteKey;
        }

        // frontoffice assets
        public static MvcHtmlString Css(this UrlHelper helper, string filePath)
        {
            TagBuilder builder = new TagBuilder("link");
            builder.MergeAttribute("rel", "stylesheet");
            builder.MergeAttribute("href", helper.Content("~/Content/frontoffice/css/" + filePath));

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));           
        }
        public static MvcHtmlString Js(this UrlHelper helper, string filePath)
        {
            TagBuilder builder = new TagBuilder("script");          
            builder.MergeAttribute("src", helper.Content("~/Content/frontoffice/js/" + filePath));

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));       
        }
        public static MvcHtmlString JsAssets(this UrlHelper helper, string filePath)
        {
            TagBuilder builder = new TagBuilder("script");
            builder.MergeAttribute("src", helper.Content("~/Content/frontoffice/assets/" + filePath));

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString Assets(this UrlHelper helper, string filePath)
        {
            TagBuilder builder = new TagBuilder("link");
            builder.MergeAttribute("rel", "stylesheet");
            builder.MergeAttribute("href", helper.Content("~/Content/frontoffice/assets/" + filePath));

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }
        public static string HomeImage(this UrlHelper helper, string filePath)
        {
            return helper.Content("~/Content/photo/home/" + filePath);
        }
        public static string Image(this UrlHelper helper, string filePath)
        {
            return helper.Content("~/Content/frontoffice/images/" + filePath);
        }
        public static string Image812x400(this UrlHelper urlHelper, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return urlHelper.Content("~/Content/frontoffice/images/404-bg.png");
            }

            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/Setting/812x400/{0}", imageUrl))))
            {
                return urlHelper.Content("~/Content/frontoffice/images/404-bg.png");
            }
            else
            {
                return urlHelper.Content($"~/Content/Photo/Setting/812x400/{imageUrl}");
            }
        }
        public static string Image100x100(this UrlHelper urlHelper, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return urlHelper.Content("~/Content/frontoffice/images/404-bg.png");
            }

            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/Setting/100x100/{0}", imageUrl))))
            {
                return urlHelper.Content("~/Content/frontoffice/images/404-bg.png");
            }
            else
            {
                return urlHelper.Content($"~/Content/Photo/Setting/100x100/{imageUrl}");
            }
        }
        public static string SlimImage(this UrlHelper helper, string imageUrl)
        {
            return helper.Content("~/Content/slim/img/" + imageUrl);
        }
        public static string FamilyImage(this UrlHelper urlHelper, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return urlHelper.Content("~/Content/Photo/Blank.png");
            }

            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/Member/216X196/{0}", imageUrl))))
            {
                return urlHelper.Content("~/Content/Photo/Blank.png");
            }
            else
            {
                return urlHelper.Content($"~/Content/Photo/Member/216X196/{imageUrl}");
            }
        }

        public static string Image630x455(this UrlHelper urlHelper, string imageUrl)
        {
            return urlHelper.Content($"~/content/photo/settings/_630x455/{imageUrl}");
        }
        public static string TopicImage630x455(this UrlHelper urlHelper, string imageUrl)
        {
            return urlHelper.Content($"~/content/photo/support/_630x455/{imageUrl}");
        }

        public static string Image177x266(this UrlHelper urlHelper, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return urlHelper.Content("~/Content/frontoffice/images/event.png");
            }

            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/photo/events/_177X266/{0}", imageUrl))))
            {
                return urlHelper.Content("~/Content/frontoffice/images/event.png");
            }
            else
            {
                return urlHelper.Content($"~/Content/photo/events/_177X266/{imageUrl}");
            }
        }
        public static string SupportImage60x60(this UrlHelper urlHelper, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return urlHelper.Content("~/Content/frontoffice/images/info-ico.png");
            }

            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/photo/common/_60x60/{0}", imageUrl))))
            {
                return urlHelper.Content("~/Content/frontoffice/images/info-ico.png");
            }
            else
            {
                return urlHelper.Content($"~/Content/photo/common/_60x60/{imageUrl}");
            }
        }
        public static string SupportImage630x455(this UrlHelper urlHelper, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return urlHelper.Content("~/Content/frontoffice/images/awesome-design-img.png");
            }

            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/photo/common/_630x455/{0}", imageUrl))))
            {
                return urlHelper.Content("~/Content/frontoffice/images/awesome-design-img.png");
            }
            else
            {
                return urlHelper.Content($"~/Content/photo/common/_630x455/{imageUrl}");
            }
        }
        public static string Image563x376(this UrlHelper urlHelper, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return urlHelper.Content("~/Content/slim/img/500x500.png");
            }

            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/photo/events/_563x376/{0}", imageUrl))))
            {
                return urlHelper.Content("~/Content/slim/img/500x500.png");
            }
            else
            {
                return urlHelper.Content($"~/Content/photo/events/_563x376/{imageUrl}");
            }
        }
        public static string CmsImage(this UrlHelper urlHelper, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return urlHelper.Content("~/Content/frontoffice/images/404-bg.png");
            }

            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/cms/_1921x987/{0}", imageUrl))))
            {
                return urlHelper.Content("~/Content/frontoffice/images/404-bg.png");
            }
            else
            {
                return urlHelper.Content($"~/Content/Photo/cms/_1921x987/{imageUrl}");
            }
        }
        public static string Slider(this UrlHelper urlHelper, string imageUrl, int slideTypeId)
        {
            if(slideTypeId == Suftnet.Cos.Common.Slider.Type1)
            {
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return urlHelper.Content("~/Content/frontoffice/images/banner-img2.png");
                }

                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/Slider/_1921x987/{0}", imageUrl))))
                {
                    return urlHelper.Content("~/Content/frontoffice/images/banner-img2.png");
                }
                else
                {
                    return urlHelper.Content($"~/Content/Photo/Slider/_1921x987/{imageUrl}");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return urlHelper.Content("~/Content/frontoffice/images/banner-slide2.png");
                }

                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/Slider/_1921x987/{0}", imageUrl))))
                {
                    return urlHelper.Content("~/Content/frontoffice/images/banner-slide2.png");
                }
                else
                {
                    return urlHelper.Content($"~/Content/Photo/Slider/_1921x987/{imageUrl}");
                }
            }          

        }
        public static string MapImageUrl(this UrlHelper urlHelper, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return urlHelper.Content("~/Content/frontoffice/images/info-ico.png");
            }

            if (!File.Exists(HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/Common/_60x60/{0}", imageUrl))))
            {
                return urlHelper.Content("~/Content/frontoffice/images/info-ico.png");
            }
            else
            {
                return urlHelper.Content($"~/Content/Photo/Common/_60x60/{imageUrl}");
            }
        }
        public static string Tour(this UrlHelper urlHelper, string imageUrl, int tourTypeId)
        {
            switch (tourTypeId)
            {
                case Suftnet.Cos.Common.Tour.Type1:

                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        return urlHelper.Content("~/Content/frontoffice/images/how-it-works-img-1.png");
                    }

                    if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/tour/_1921x987/{0}", imageUrl))))
                    {
                        return urlHelper.Content("~/Content/frontoffice/images/how-it-works-img-1.png");
                    }
                    else
                    {
                        return urlHelper.Content($"~/Content/Photo/tour/_1921x987/{imageUrl}");
                    }

                case Suftnet.Cos.Common.Tour.Type2:

                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        return urlHelper.Content("~/Content/frontoffice/images/how-it-works-img-2.png");
                    }

                    if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/tour/_1921x987/{0}", imageUrl))))
                    {
                        return urlHelper.Content("~/Content/frontoffice/images/how-it-works-img-2.png");
                    }
                    else
                    {
                        return urlHelper.Content($"~/Content/Photo/tour/_1921x987/{imageUrl}");
                    }

                case Suftnet.Cos.Common.Tour.Type3:

                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        return urlHelper.Content("~/Content/frontoffice/images/how-it-works-img-3.png");
                    }

                    if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/tour/_1921x987/{0}", imageUrl))))
                    {
                        return urlHelper.Content("~/Content/frontoffice/images/how-it-works-img-3.png");
                    }
                    else
                    {
                        return urlHelper.Content($"~/Content/Photo/tour/_1921x987/{imageUrl}");
                    }
                case Suftnet.Cos.Common.Tour.Type4:

                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        return urlHelper.Content("~/Content/frontoffice/images/how-it-works-img-4.png");
                    }

                    if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/Photo/tour/_1921x987/{0}", imageUrl))))
                    {
                        return urlHelper.Content("~/Content/frontoffice/images/how-it-works-img-4.png");
                    }
                    else
                    {
                        return urlHelper.Content($"~/Content/Photo/tour/_1921x987/{imageUrl}");
                    }

                default:
                      return urlHelper.Content("~/Content/frontoffice/images/banner-img2.png");
            }          
           
        }
        public static MvcHtmlString MaviaJs(this UrlHelper helper, string filePath)
        {
            TagBuilder builder = new TagBuilder("script");
            builder.MergeAttribute("src", helper.Content("~/Content/mavia/js/" + filePath));

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString MaviaAssets(this UrlHelper helper, string filePath)
        {
            TagBuilder builder = new TagBuilder("link");
            builder.MergeAttribute("rel", "stylesheet");
            builder.MergeAttribute("href", helper.Content("~/Content/mavia/assets/" + filePath));

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }
        public static string MaviaImage(this UrlHelper helper, string filePath)
        {
            return helper.Content("~/Content/mavia/img/" + filePath);
        }
        public static MvcHtmlString MaviaCss(this UrlHelper helper, string filePath)
        {
            TagBuilder builder = new TagBuilder("link");
            builder.MergeAttribute("rel", "stylesheet");
            builder.MergeAttribute("href", helper.Content("~/Content/mavia/css/" + filePath));

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }
        public static string AssetShortCut(this UrlHelper helper, string fileName)
        {
            var path = helper.Content("~/Content/zice-OneChurch/images/icon/shortcut/" + fileName);
            return path;
        }
        public static string SpriteUrl(this UrlHelper helper, string fileName)
        {
            var path = helper.Content("~/Content/Loading-Spinner-Sprite/img/" + fileName);
            return path;
        }
        public static string Asset(this UrlHelper helper, string fileName)
        {
            var path = helper.Content("~/Content/zice-OneChurch/images/icon/color_18/" + fileName);
            return path;
        }
        public static string HeaderAsset(this UrlHelper helper, string fileName)
        {
            var path = helper.Content("~/Content/zice-OneChurch/images/icon/color_18/" + fileName);          
            return path;
        }
        public static string CleanURL(this UrlHelper helper, string originalUrl)
        {
            string cleanUrl = Regex.Replace(originalUrl, @"[^a-z0-9\s-]", "_"); // invalid chars
            cleanUrl = Regex.Replace(originalUrl, @"\s+", "_").Trim(); // convert multiple spaces into one
            return cleanUrl;
        }
        public static IHtmlString ActionMenu(this UrlHelper helper, string innerText, string actionName, string controllerName, object rauteValues)
        {
            string currentControllerName = (string)helper.RequestContext.RouteData.Values["controller"];
            string currentActionName = (string)helper.RequestContext.RouteData.Values["action"];

            var url = new UrlHelper(helper.RequestContext);
            var tag = new TagBuilder("a");
            var ultag = new TagBuilder("ul");
            string strHtml = string.Empty;

            tag.MergeAttribute("href", url.Action(actionName, controllerName, rauteValues));
            tag.InnerHtml = innerText;

            // Add selected class
            if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) && currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase))
            {

                strHtml = tag.ToString(TagRenderMode.Normal);
                return new MvcHtmlString("<li class=\"limenu select\">" + strHtml + "</li>");
                ////return MvcHtmlString.Create(strHtml);

            }
            strHtml = tag.ToString(TagRenderMode.Normal);
            return new MvcHtmlString("<li class=\"limenu\">" + strHtml + "</li>");
        }
        public static MvcHtmlString ImageActionLink(this HtmlHelper helper, string imagePath, string actionName, string controllerName, object routeValues, object imageAttributes, object linkAttributes)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);

            var aBuilder = new TagBuilder("a");
            aBuilder.MergeAttributes(new RouteValueDictionary(linkAttributes));
            aBuilder.MergeAttribute("href", urlHelper.Action(actionName, controllerName, routeValues));

            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttributes(new RouteValueDictionary(imageAttributes));
            imgBuilder.MergeAttribute("src", imagePath);

            aBuilder.InnerHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            return new MvcHtmlString(aBuilder.ToString(TagRenderMode.Normal));
        }
        public static IHtmlString MenuItem(this HtmlHelper helper, string linkText, string actionName, string controllerName, object rauteValues, object htmlattribute)
        {
            string currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            string currentActionName = (string)helper.ViewContext.RouteData.Values["action"];
            // Add selected class
            if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) && currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase))
                return MvcHtmlString.Create(string.Concat("<li class=\"mega mega-active\">", helper.ActionLink(linkText, actionName, controllerName, rauteValues, htmlattribute), "</li>"));

            /// Add link
            return MvcHtmlString.Create(string.Concat("<li class=\"mega\">", helper.ActionLink(linkText, actionName, controllerName, rauteValues, htmlattribute), "</li>"));
        }

        // frontoffice assets
          
        public static MvcHtmlString FrontOfficeCssAssets(this UrlHelper helper, string fileName)
        {
            TagBuilder builder = new TagBuilder("link");
            builder.MergeAttribute("rel", "stylesheet");
            builder.MergeAttribute("href", helper.Content("~/Content/frontoffice-2/css/" + fileName));

            var model = new MvcHtmlString(builder.ToString(TagRenderMode.Normal));

            return model;
        }
        public static MvcHtmlString FrontOfficeJsAssets(this UrlHelper helper, string fileName)
        {
            TagBuilder builder = new TagBuilder("script");
            builder.MergeAttribute("src", helper.Content("~/Content/frontoffice-2/js/" + fileName));

            var model = new MvcHtmlString(builder.ToString(TagRenderMode.Normal));

            return model;
        }
        public static MvcHtmlString FrontOfficeJsPluginsAssets(this UrlHelper helper, string fileName)
        {
            TagBuilder builder = new TagBuilder("script");
            builder.MergeAttribute("src", helper.Content("~/Content/frontoffice-2/js/plugins/" + fileName));

            var model = new MvcHtmlString(builder.ToString(TagRenderMode.Normal));

            return model;
        }
        public static MvcHtmlString FrontOfficeImageAssets(this UrlHelper helper, string fileName)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.MergeAttribute("src", helper.Content("~/Content/frontoffice-2/" + fileName));

            var model = new MvcHtmlString(builder.ToString(TagRenderMode.Normal));

            return model;
        }
        public static string FrontOfficeImagePath(this UrlHelper helper, string fileName)
        {
            return helper.Content("~/Content/frontoffice-2/" + fileName);
        }
    }
}
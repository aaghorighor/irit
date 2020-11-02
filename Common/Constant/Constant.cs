using System;

namespace Suftnet.Cos.Common
{
    public static class Constant
    {
        public static string DuplicateTopicNotAllowed = "Duplicate topic is not allowed";
        public static string InvalidCredentials = "UserName or Password cannot be null or empty";
        public static string UserNotRegister = "User not found, Please contact your administrator";
        public static string ErrorMessage = "Please contact your adminstrator";
        public static string SubscriptionError = "Cannot Processing your subscription request.";
        public static string ValidationErrorMessage = "Please Change some few things and try submitting again.";
        public static string UserRoleAlreadyExist = "User already in role";
        public static string OperationIsNotAllowed = "This operation is not allowed";
        public static int DefaultCurrency = 265;
        public static string DefaultDateTimeFormat = "mm/dd/yy";
        public static string DefaultYoutubeUserName = "Tester";
        public static string ProductName = "Jerur";
        public static string CurrencySymbol = "£";
        public static string DefaultHexCurrencySymbol = "&#163;";
        public static string UpgradeSubscription = "Your Subscription could not be upgraded at this time, Please try later.";
        public static string CancelSubscription = "Your Subscription could not be cancel at this time, Please try later.";
        public static string Success = "Success";
        public static string ApiKeyNotValid = "Api Key is not valid";
        public static string ApiKeyRequired = "Api Key is required";
        public static string TenantCountOfflimit = "Your subscription has exceeded it's member limit, Please upgrate your plan to add more members.";
        public static string LoginUrl = "www.jerur.com/login";
        public const string ApiKeySchemeName = "ApiKey";
        public const string UserNameAlreadyExist = "Email address is in used, Please enter different email address and try again";
        public const string UserRoleNotSelected = "Please select at least one user role";
        public const string InValidEventStartDate = "Event Start Date can't be in the past, please amend and try again";
        public const string DefaultImageUrl = "191868dc-a767-4e70-a24a-fc67d71eeb6a.jpg";
        public const string DefaultMemberImageUrl = "f9497298-9dcb-43bc-b5aa-dbaeb49ecaee.jpg";
        public const string InvalidPhotoUrl = "Error, No photo url found, plase upload photo and try again";
        public const string InvalidEmailReasonPhrase = "Your email address already exist, please try entry another one";
        public const string MobilePermissionAddedAlready = "Mobile Permission added already for this user";
        public const string CacheTenantFormat = "Tenant.{0}";
        public const string CacheUserPermissionFormat = "Permission.{0}";
        public const string Format = "User.{0}";
        public const string AdminOnly = "2033";
        public const string BackOfficeOnly = "2033,2034";
        public const string FrontOfficeOnly = "2033,5219,2034";
        public const string SiteAdminOnly = "2033,6241";
        public const string EntityValidationErrors = "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:";
        public const string ValidationErrors = "- Property: \"{0}\", Error: \"{1}\"";
        public const string DefaultPassword = "win830_!EMlg%";
        public const string CreateOrEditAdminAccount = "Only site Admin or Admin account can be created or edited";
        public const string AccountNotCreated = "Member created, but login not created as member is part of a family group";
        public const string AccountCouldNotCreated = "Member created, but login could not created. Member should contact admin for login account creation";
        public const string AccountCreatedAutoLoginNotEnable = "Member created, login created, but auto login by member is disabled";
        public const string AccountCreatedAutoLoginEnabled = "Member created, login created, but auto login by member is enabled";
        public const string TopicExist = "Topic exist already";
        public const string SuccessCode = "1000";
        public const string DangerCode = "2000";
        public const string CompletedOrder = "This order is closed and cannot be modify any more";
        public const string DEMO_TENANTID = "207910E5-F78C-4A9C-ACDB-FA69B26B6AC1";
        public const string DEMO_LASTNAME = "Demo";
        public const string DEMO_FIRSTNAME = "Manager";
        public const string SUBSCRIPTION_EXPIRED = "Your Subscription has expired, please go to Jerur Online and Subscribe to one of our Plan.Thanks";
        public const string TRIAL_EXPIRED = "Your Trial has expired, please go to Jerur Online and Subscribe to one of our Plan.Thanks";
        public static string DeleteUserError = "This user can not be deleted at this time";

    }

    public static class Identity
    {
        public const string UserId = "UserId";
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string Roles = "Roles";
        public const string UserName = "UserName";
        public const string AreaId = "AreaId";
        public const string Email = "Email";
        public const string FullName = "FullName";   
        public const string TenantId = "TenantId";
        public const string TenantName = "TenantName";     
        public const string ExpirationDate = "ExpirationDate";
        public const string IsExpired = "IsExpired";
        public const string CurrencyCode = "CurrencyCode";
        public const string DeliveryRate = "DeliveryRate";
        public const string DeliveryUnit = "DeliveryUnit";
        public const string IsFlatRate = "IsFlatRate";
        public const string DeliveryNote = "DeliveryNote";
        public const string FlatRate = "FlatRate";
        public const string CompleteAddress = "CompleteAddress";
        public const string TenantEmail = "TenantEmail";
        public const string TenantMobile = "TenantMobile";      
    }

    public class PlanType
    {
        public const string Trial = "1015_1461";
        public const string Basic = "price_1HiMX9J9QQF7JMlNTPeIAz81";
        public const string Premium = "price_1HiMX9J9QQF7JMlNqSot0VCU";
        public const string PremiumPlus = "price_1HiMX9J9QQF7JMlNkDdQsDtR";       
    }

    public static class PlanNameType
    {
        public const string Trial = "Trial";
        public const string Basic = "Basic";
        public const string Premium = "Premium";
        public const string PremiumPlus = "Premium Plus";
    }

    public static class SubscriptionBillingCycleType
    {
        public const int TrialDays =2;
        public const int Basic = 31;  
        public const int Premium = 181;
        public const int PremiumPlus = 366;
    }

    public static class PlanRateType
    {
        public const int Trial = 0;
        public const int Basic = 20;
        public const int Premium = 100;
        public const int PremiumPlus = 200;
    }

   
    public static class CutOff
    {
        public const int Trial = 10;
        public const int Basic = 200;
        public const int Premium = 400;
        public const int PremiumPlus = 800;
    }
  
    public static class Permission
    {
        public const int Disable = 5130;
        public const int Enable = 5129;      
    }

    public static class PermissionType
    {
        public const int Custom = 5130;
        public const int GetAll = 5147;
        public const int Get = 5146;
        public const int Remove = 5145;
        public const int Edit = 5144;
        public const int Create = 5143;
    }     
  
        
    public static class Slider
    {
        public const int Type1 = 5189;
        public const int Type2 = 5190;
        public const int Type3 = 5191;
    }

    public static class Tour
    {
        public const int Type1 = 5192;
        public const int Type2 = 5193;
        public const int Type3 = 5194;
        public const int Type4 = 5196;
    }
    public static class FaqType
    {
        public const int GeneralEnquiries = 5188;
        public const int Features = 5187;
        public const int PermissionPrivacy = 5195;
        public const int PricingPlans = 5186;
    }
       
    public static class CmsType
    {
        public const int About = 33;
        public const int Features = 34;
        public const int Howitworks = 35;
        public const int YourSuccess = 36;
        public const int TermsofService = 38;
        public const int PrivacyPolicy = 39;
        public const int CancelSubscription = 40;
        public const int CancelTrial = 41;
        public const int WelcomeToJerur = 42;
        public const int SubscriptionExpired = 43;
        public const int TrialExpired = 44;
    }
    public static class CommonType
    {
        public const int Support = 2305;    
    }

    public static class NotiticationType
    {
        public const int Email = 5181;
        public const int Sms = 5180;
        public const int PushNotification = 6289;
    }     

    public static class NotiticationStatus
    {
        public const int SendNow = 5182;
        public const int SendLater = 5183;      
    }

    public static class SubStract
    {
        public const int Yes = 2033;
        public const int No = 6241;
    }

    public static class BackOfficeViews
    {
        public const int Member = 5131;
        public const int Events = 5132;     
        public const int Case = 5135;
        public const int Reports = 5136;
        public const int Venue = 5137;
        public const int Assest = 5138;
        public const int Settings = 5139;
        public const int User = 5140;
        public const int SmallGroup = 5141;
        public const int Attendance = 5142;          
        public const int Fellowship = 7332;
        public const int Notification = 5161;
        public const int Give = 5159;   
    }

    public static class FrontOfficeViews
    {
        public const int Slider = 7331;
        public const int ServiceTimeLine = 7330;
        public const int ServiceTime = 7329;
        public const int PrayerRequest = 7328;
        public const int Prayer = 7327;
        public const int Campaign = 7326;
        public const int Media = 7324;
        public const int Lookup = 7323;
        public const int Gallery = 7322;
        public const int Devotion = 7321;
        public const int Contact = 7320;
        public const int BibleVerse = 7319;
        public const int Article = 7318;
        public const int Album = 7317;
    }

    public static class SiteAdminOfficeViews
    {
        public const int Tutorial = 7340;
        public const int Tour = 7339;
        public const int Ticket = 7338;
        public const int Support = 7337;
        public const int Slider = 7336;
        public const int Campaign = 7326;
        public const int PageHeader = 7335;
        public const int Feature = 7334;
        public const int Faq = 7333;        
    }

    public static class ChargeCurrency
    {
        public const string Pound = "gbp";
        public const string Dollar = "usb";
    }

    public static class Settings
    {
        public const string Events = "284";
        public const string TimeLine = "3337";
        public const string Asset = "279";
        public const string Giving = "277";
        public const string SERVICETIME = "2312";
        public const string SERVICETIMELINE = "2317";
        public const string PRAYERTYPE = "294";
    }

    public static class eOrderType
    {
        public const string Delivery = "ED9EB336-D246-4747-ADB1-42FD95D98E4C";
        public const string Reservation = "FFA01FE4-8B49-41E9-A630-70FD7E756ECC";       
        public const string DineIn = "BFF4A1B2-8D64-4919-A91D-4E96E61E1A5B";             
    }

    public static class eOrderStatus
    {
        public const string Pending = "DCDC8A0D-E38A-43B5-8091-F167B001F0B5";
        public const string Ready = "E4E6975E-4881-459D-BB2D-2AD841FBA835";
        public const string Processing = "85616F94-1826-43B1-ACFF-819B37F028E4";
        public const string Reserved = "54F834F8-8F7B-42C0-8331-E1FE5AE50C83";
        public const string Occupied = "C84F0A40-4C93-4200-9531-E6AB0D8FF5D7";
        public const string Completed = "58EE00D9-D449-4EBF-B4E8-769F51FE7EFE";
    }

    public static class ePaymentMethod
    {
        public const string CARD = "FD55F6B4-30C2-48C7-B331-79A505D8F8C2";
        public const string CASH = "E0DF7037-BE71-4D90-871D-04AD8E4AD960";       
    }
       
    public static class PaymentStatus
    {
        public const string Paid = "284";
        public const string Pending = "3337";
        public const string Cancelled = "279";        
    }
    public static class SubscriptionStatus
    {
        public const string Active = "207910E5-F78C-4A9C-ACDB-FA69B26B6AC1";
        public const string Expired = "5AF7A965-FB5D-4E66-A137-549E5FC9C825";
        public const string Suspended = "5337780F-041E-4E1F-86B2-9D0B0A81C430";
        public const string Cancelled = "86F72BE4-29B4-41A9-AC22-588E8F3D5E67";
        public const string Trial = "C402ED8E-3162-46D5-99A0-0E3655B44879";
    }

    public static class ReportType 
    {
        public const int Payment = 5179;
        public const int VAT = 3115;
        public const int Sales = 277;
        public const int Menu = 276;
        public const int BestSeller = 2664;
        public const int Order = 2661;
        public const int Delivery = 2663;
        public const int Reservation = 2662;    
        public const int Invoice = 418;
        public const int OutOfStock = 419;
    }

    public static class EmailViewType
    {
        public const string OTP = "OTP";
    }

    public static class ExecutingContext
    {
        public const string LIVE = "2";
        public const string TEST = "1";
    }

    public static class DeliveryUnit
    {
        public const string Kilometer = "2663";
        public const string Miles = "2664";
    }

}
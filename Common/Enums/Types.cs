using System;

namespace Suftnet.Cos.Common
{  
    public enum eSettings : ushort {      
                     
      Editor =190, 
      Unit = 200,         
      ReportType = 239,       
      Class  = 226,     
      Currency = 236,     
      DateTimeformat = 238,     
      AddressType = 191,             
      Role = 215,     
      Position = 262,      
      Template = 267,
      Tax = 209,     
      NotificationStatus = 281,     
      SuspscriptionPlan =2294,
      SubscriptionStatus = 2295,
      TransactionStatus = 2297,
      UserStatus = 2296,     
      Area = 2304,
      ForgotPassword =1081,
      Tutorial = 2305,
      TutorialAction =2306,    
      Status = 2309,    
      Notification = 2311,     
      Month = 2315,     
      PlanFeature = 3320,
      PlanPrice = 2320,
      PlanStatus = 3321,    
      Permission = 3322,
      View = 3323,      
      Faq = 3331,
      SliderType = 3332,
      TourType = 3333,
      TaskType =2313,     
      MediaFormat = 3338,
      MediaType = 291,
      SupportSectionType = 3339,
      SupportType = 3340,
      Feature = 3342,
      PageHeader = 3341,    
      ResetPassword = 48,     
      MobileDeliveryOptions = 4347,   
      MobilePermission = 4346,             
      Region = 2319,
      Backofficepages = 3323,
      FrontOfficepages = 4351,
      SiteAdminOffice = 4352,
      SupportTopic = 4354
    }

    public enum EventLogSeverity
    {        
        Debug,
        Error,
        Fatal,        
        Information,
        None,       
        Warning
    }

    public enum EmailSendingType
    {       
        To,       
        CC,      
        BCC,       
        ReplyTo,
    }
   
    public enum TopQuery
    {
        Top5 = 5,
        Top10 = 10,
        Top15 = 15,
        Top20 = 20,
        Top25 = 25
    }

    public enum DeliveryOptions
    {
        Manual = 7298,
        Auto = 7297
    }
       
    public enum eStatus : ushort
    {
        Open = 3069,
        Close = 3070
    }
        
        
    public enum flag : ushort
    {
        Add = 1,
        Update = 2,
        Refresh = 3,
    }      
  
    public enum eNotification : ushort
    {
        Send = 443,
        NotSend = 444
    }
   
    public enum eMessageType : ushort
    {
        UserAccount = 81,
        Renewal = 484,
        AccountExpired = 690
    }
    public enum eEditor : ushort
    {
        Event = 77,
        UserAccount = 1080,
        PaymentRenewal = 1003,
        SubscriptionExpired = 2,
        SubscriptionTrialPeriodEnd = 9,
        SubscriptionTrialPeriod = 3,
        MySubscription = 4,
        SubscriptionTips = 6,
        InvoiceTipe = 7,
        CreditCardTips = 8,
        SubscriptionPolicy = 10,
        SubscriptionCreated = 15,
        SubscriptionUpdated = 16,
        SubscriptionDeleted = 17,
        ChargeSucceeded = 19,
        ChargeFailed = 23,
        ChargeRefunded = 18,
        CheckOut = 22,
        SubscriptionTrialConfirmation =32,
        SubscriptionConfirmation =31,
        MemberRegistration = 37,
        PrivacyPolicy =39,
        TermsOfUsed = 38
   
    }
    public enum eArea : ushort
    {
        SiteAdmin = 6241,
        Admin = 2033,
        BackOffice = 2034,
        FrontOffice = 5219,
        Customer = 5218,
        DeliveryOffice = 6239
    }
    public enum eExecutingContext : ushort
    {
        Test = 1,
        Staging = 2,
        Preproduction = 3,
        Production= 4
    }

    public enum eTenantStatus : ushort
    {
        Open = 2005
    }          
     
    public enum ePlan : ushort
    {
        Trial = 1461,
        Basic = 1460,
        PremiumPlus = 1459,
        Premium = 1458    
    }

    public enum ePlanExpirationDate : ushort
    {
        Trial = 15,
        Basic = 31,
        PremiumPlus = 367,
        Premium = 181
    }   

    public enum eClass : ushort
    {
        System = 259,
        Application = 258
    }       

}
namespace Suftnet.Cos.Web.Mapper
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;

    using System;
    using System.Collections.Generic;

    using ViewModel;   
    using Suftnet.Cos.Services;
    using Common;

    public static class Map
    {
        public static TenantDto From(TenantModel model)
        {
            var _model = new TenantDto();

            _model.CompleteAddress = model.CompleteAddress;
            _model.Name = model.Name;
            _model.Mobile = model.Mobile;
            _model.Telephone = model.Telephone;
            _model.Email = model.Email;          
            _model.Description = model.Description;
            _model.StripePublishableKey = model.StripePublishableKey;
            _model.StripeSecretKey = model.StripeSecretKey;
            _model.WebsiteUrl = model.WebsiteUrl;           
            _model.CurrencyId = model.CurrencyId;          
            _model.Id = model.TenantId;
            _model.LogoUrl = model.LogoUrl;
            _model.Publish = model.Publish;            
            _model.CurrencyCode = model.CurrencyCode;
            _model.AddressId = model.AddressId;
            _model.DeliveryUnitId = model.DeliveryUnitId;
            _model.DeliveryLimitNote = model.DeliveryLimitNote;
            _model.IsFlatRate = model.IsFlatRate == null ? false : model.IsFlatRate;
            _model.DeliveryRate = model.DeliveryRate;
            _model.FlatRate = model.FlatRate;
            _model.StatusId = model.StatusId;

            _model.CreatedDT = model.CreatedDT;
            _model.CreatedBy = model.CreatedBy;

            return _model;
        }        
            
        public static List<TenantShortModel> From(IEnumerable<TenantShortDto> model)
        {
            var _model = new List<TenantShortModel>();

            foreach (var item in model)
            {
                var tenantShortDto = new TenantShortModel
                {
                    Name = item.Name,                    
                    Denomination = item.Denomination,
                    Address = item.Address,
                    ExternalId = item.Id.ToString().ToEncrypt(),                  
                    LogoUrl = item.LogoUrl                   
                };

                _model.Add(tenantShortDto);
            }

            return _model;
        }

        public static TenantViewModel From(TenantDto model)
        {
            var _model = new TenantViewModel
            {
                Name = model.Name == null ? "" : model.Name,
                Telephone = model.Telephone == null ? "" : model.Telephone,
                Email = model.Email == null ? "" : model.Email,
                CompleteAddress = model.CompleteAddress,              
                ExternalId = model.TenantId.ToString().ToEncrypt(),          
                LogoUrl = model.LogoUrl == null ? "" : model.LogoUrl,
                Description = model.Description == null ?"" : HtmlToText.ConvertHtml(model.Description),          
                Latitude = model.Latitude == null ? "" : model.Latitude,
                Longitude = model.Longitude == null ? "" : model.Longitude,     
                Mobile = model.Mobile == null ? "" : model.Mobile,
                WebsiteUrl =  model.WebsiteUrl == null ? "" : model.WebsiteUrl,           
                StripePublishableKey = model.StripePublishableKey == null ? "" : model.StripePublishableKey,              
                BackgroundUrl =  model.BackgroundUrl == null ? "" : model.BackgroundUrl,               
              
            };

            return _model;
        }          

        public static IEnumerable<DeviceModel> From(IEnumerable<DeviceDto> model)
        {
            var _deviceModel = new List<DeviceModel>();

            foreach (var item in model)
            {
                var deviceModel = new DeviceModel
                {
                    AppVersion = item.AppVersion,
                    DeviceId = item.AppVersion,
                    DeviceName   = item.DeviceName,
                    ExternalId   = item.ExternalId,
                    OsVersion  = item.OsVersion,
                    Serial = item.Serial
                };

                _deviceModel.Add(deviceModel);
            }

            return _deviceModel;
        }

        public static DeviceDto From(DeviceModel model)
        {
            var deviceModel = new DeviceDto
            {
                AppVersion = model.AppVersion,
                DeviceId = model.DeviceId,
                DeviceName = model.DeviceName,
                TenantId = new Guid(model.ExternalId.ToDecrypt()),
                OsVersion = model.OsVersion,
                Serial = model.Serial,

                CreatedBy = Environment.UserName,
                CreatedDT = DateTime.UtcNow                 
            };

            return deviceModel;
        }      

    }
}
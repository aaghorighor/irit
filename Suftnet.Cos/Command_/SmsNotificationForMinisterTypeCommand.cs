namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using Core;
    using System.Collections.Generic;
    using ViewModel;
    using Cos.Services;
    using Extension;

    public class SmsNotificationForMinisterTypeCommand : ICommand
    {
        private readonly ISmallGroup _smallGroup;

        public SmsNotificationForMinisterTypeCommand(ISmallGroup smallGroup)
        {
            _smallGroup = smallGroup;
        }
               
        public MinisterTypeNotifyModel MinisterTypeNotifyModel { get; set; }
       
        public async void Execute()
        {
          await System.Threading.Tasks.Task.Run(() => this.NotifyAsync());
        }

        #region private function
        public void NotifyAsync()
        {
            var sms = GeneralConfiguration.Configuration.DependencyResolver.GetService<ISms>();
            var recipients = PrepareRecipients();

            foreach (var recipient in recipients)
            {
                sms.SendMessage(recipient.Mobile, MinisterTypeNotifyModel.Body);
            }
        }      

        private List<RecipientModel> PrepareRecipients()
        {
            //var recipients = new List<RecipientModel>();

            //var members = _smallGroup.GetByMinistryTypeId(MinisterTypeNotifyModel.ExternalId.ToDecrypt().ToInt(), MinisterTypeNotifyModel.MinistryTypeId);

            //foreach (var item in members)
            //{
            //    if (!(bool)item.IsSms)
            //    {
            //        continue;
            //    }

            //    if (string.IsNullOrEmpty(item.Mobile))
            //    {
            //        continue;
            //    }

            //    var recipientModel = new RecipientModel()
            //    {
            //        Mobile = item.Mobile,
            //        Recipient = item.FirstName + " " + item.LastName
            //    };

            //    recipients.Add(recipientModel);
            //}

            //return recipients;

           return new List<RecipientModel>();
        }       

        #endregion

    }
}
namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using Core;
    using System.Collections.Generic;
    using ViewModel;
    using Cos.Services;
    using Common;

    public class SmsNotificationCommand : ICommand
    {
        private readonly IMember _member;

        public SmsNotificationCommand(IMember member)
        {
            _member = member;
        }
               
        public string Title { get; set; }
        public string Body { get; set; }         
        public int TenantId { get; set; }

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
                sms.SendMessage(recipient.Mobile, Body);
            }
        }      

        private List<RecipientModel> PrepareRecipients()
        {           
            var recipients = new List<RecipientModel>();

            var members = _member.GetMembersForSms((int)MemberType.Adult, this.TenantId);

            foreach (var item in members)
            {
                if (!(bool)item.IsSms)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(item.Mobile))
                {
                    continue;
                }

                var recipientModel = new RecipientModel()
                {
                    Mobile = item.Mobile,
                    Recipient = item.FirstName + " " + item.LastName
                };

                recipients.Add(recipientModel);
            }

            return recipients;
        }       

        #endregion

    }
}
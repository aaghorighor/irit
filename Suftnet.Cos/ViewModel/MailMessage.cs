namespace Suftnet.Cos.Model
{
    using Suftnet.Cos.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using Suftnet.Cos.Model;

   public class MailMessage
	{
		public MailMessage()
			: this(new System.Net.Mail.MailMessage())
		{

		}

		public MailMessage(System.Net.Mail.MailMessage mailMessage)
		{
			this.Subject = mailMessage.Subject;
			this.Body = mailMessage.Body;
			this.Recipients = new List<MailAddress>();
            this.Attachments = mailMessage.Attachments; 

			if (mailMessage.To != null)
			{
				foreach (var to in mailMessage.To)
				{
					Recipients.Add(new MailAddress()
					{
						Address = to.Address,
						DisplayName = to.DisplayName,
						SendingType = EmailSendingType.To,
					});
				}
			}
			if (mailMessage.CC != null)
			{
				foreach (var to in mailMessage.CC)
				{
					Recipients.Add(new MailAddress()
					{
						Address = to.Address,
						DisplayName = to.DisplayName,
						SendingType = EmailSendingType.CC,
					});
				}
			}
			if (mailMessage.Bcc != null)
			{
				foreach (var to in mailMessage.Bcc)
				{
					Recipients.Add(new MailAddress()
					{
						Address = to.Address,
						DisplayName = to.DisplayName,
						SendingType = EmailSendingType.BCC,
					});
				}
			}			

			if (mailMessage.From != null)
			{
				From = new MailAddress()
				{
					Address = mailMessage.From.Address,
					DisplayName = mailMessage.From.DisplayName,
				};

			}

			Headers = new List<MailMessageHeader>();
		}
		
		public string Subject { get; set; }
		
		public string Body { get; set; }

        public AttachmentCollection Attachments { get; set; }
		
		public List<MailAddress> Recipients { get; set; }
	
		public MailAddress From { get; set; }

		public List<MailMessageHeader> Headers { get; set; }
	
		public DateTime? StartDate { get; set; }

		public static explicit operator System.Net.Mail.MailMessage(MailMessage template)
		{
			var mailMessage = new System.Net.Mail.MailMessage();
			foreach (var email in template.Recipients)
			{
				switch (email.SendingType)
				{
					case EmailSendingType.To:
						mailMessage.To.Add(new System.Net.Mail.MailAddress(email.Address, email.DisplayName));		 
						break;
					case EmailSendingType.CC:
						mailMessage.CC.Add(new System.Net.Mail.MailAddress(email.Address, email.DisplayName));		 
						break;
					case EmailSendingType.BCC:
						mailMessage.Bcc.Add(new System.Net.Mail.MailAddress(email.Address, email.DisplayName));		 
						break;							 
						
					default:
						break;
				}
			}

			try
			{
                mailMessage.Subject = template.Subject ?? "(Unknown)";
				mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;                
			}
			catch
			{
				mailMessage.Subject = "(Unknown)";
				template.Body = "subject :" + template.Subject + Environment.NewLine + template.Body;
			}

			mailMessage.Body = template.Body;
			mailMessage.IsBodyHtml = true;
			mailMessage.From = new System.Net.Mail.MailAddress(template.From.Address, template.From.DisplayName);

            if (template.Attachments.Count > 0)
            {
                foreach (var attachments in template.Attachments)
                {
                    mailMessage.Attachments.Add(attachments);
                }               
            }                   

			return mailMessage;
		}
	}
}


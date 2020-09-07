namespace Suftnet.Cos.DataAccess
{ 
    using System.ComponentModel.DataAnnotations;

    public class NotificationDto : BaseDto
    {                     
        [Required()]
        [StringLength(500)]
        public string Subject { get; set; }      
        [Required()]
        [StringLength(2000)]
        public string Body { get; set; }
        public string MessageType { get; set; }
        public int MessageTypeId { get; set; }
        public int? EventId { get; set; }
        public string RecipientGroup { get; set; }
        public int? RecipientGroupId { get; set; }
        public int? WhatsAppGroupId { get; set; }
        public string[] SelectedRecipient { get; set; }
        public string Recipient { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public bool NotifyParticipants { get; set; }
        public bool NotifyMinisters { get; set; }
        public int? MemberId { get; set; }
        public bool? IsIndividual { get; set; }
    }
    
}

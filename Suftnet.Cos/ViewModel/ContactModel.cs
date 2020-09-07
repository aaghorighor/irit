namespace Suftnet.Cos.ViewModel
{
    public class ContactModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public int? flag { get; set; }

        public string Subject
        {
            get { return "Feedback from vistor"; }
            private set { }
        }
           
    }
}
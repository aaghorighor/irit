namespace Suftnet.Cos.DataAccess
{  
    public class FilterModel
    {             
        public string County { get; set; }
        public string Country { get; set; }             
        public string Town { get; set; }
        public string Denomination { get; set; }
        public string ExternalId { get; set; }
        public int DenominationId { get; set; }
        public int? Page { get; set; }
    }
}

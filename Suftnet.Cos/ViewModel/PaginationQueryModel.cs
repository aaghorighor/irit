namespace Suftnet.Cos.Web.ViewModel
{   
    using System.ComponentModel.DataAnnotations;   
    public class PaginationQueryModelShort
    {
        [Required()]
        public int Page { get; set; }
        [Required()]
        public int Count { get; set; }
        public string Query { get; set; }                
    }

    public class PaginationQueryModel
    {
        [Required()]
        public int Page { get; set; }
        [Required()]
        public int Count { get; set; }
        public int? Query { get; set; }
        [Required()]
        public string ExternalId { get; set; }
    }

    public class AttendanceQueryModel
    {
        [Required()]
        public int AttendanceId { get; set; }       
    }

    public class ServiceTimeQueryModel
    {
        [Required()]
        public int ServiceTimeId { get; set; }
    }

    public class MyTimeLineModel
    {
        [Required()]
        public string ExternalId { get; set; }
    }
}
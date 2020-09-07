namespace Suftnet.Cos.Web
{
    using System.Collections.Generic;

    public class PagerModel<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int? Category{ get; set; }
    }
}
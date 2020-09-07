namespace Suftnet.Cos.Web.ViewModel
{
    using DataAccess;

    public class FilterAdapter
    {
        public FilterModel Filter { get; set; }
        public PagerModel<TenantModel> PagerModel { get; set; }
    }
}
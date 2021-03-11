namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using ViewModel;    
    using System;
    using System.Threading.Tasks;
  
    public class ItemCommand : IItemCommand
    {      
        private readonly ICategory _category;
        private readonly IMenu _menu;
        private readonly IAddon _addon;
        private readonly ITax _tax;
        private readonly IDiscount _discount;

        public ItemCommand(
            ICategory category, IAddon addon,
             IDiscount discount, ITax tax,
            IMenu menu)
        {
            _category = category;
            _menu = menu;
            _addon = addon;
            _tax = tax;
            _discount = discount;
        }               
              
        public Guid TenantId { get; set; }     

        public async Task<BoostrapModel> Execute()
        {
            var task = await Task.Factory.StartNew(() => {

                var model = new BoostrapModel
                {
                    Menus = _menu.GetBy(TenantId),
                    Categories = _category.GetBy(TenantId),
                    Addons = _addon.GetBy(TenantId)                    
                };

                return model;
            });

            return task;
        }     

    }
}
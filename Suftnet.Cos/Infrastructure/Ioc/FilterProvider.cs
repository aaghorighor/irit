namespace Suftnet.Cos.Web
{
    using StructureMap;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class StructureMapFilterProvider : FilterAttributeFilterProvider
    {
        private IContainer m_Container;

        public StructureMapFilterProvider(IContainer container)
        {
            m_Container = container;
        }

        #region IFilterProvider Membres

        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var list = base.GetFilters(controllerContext, actionDescriptor);

            if (list != null)
            {
                foreach (var item in list)
                {
                    m_Container.BuildUp(item.Instance);
                }
            }
            return list;
        }

        #endregion
    }
}
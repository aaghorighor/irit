namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("MenuCategoryWrapper")]
    [Serializable]
    public class MenuCategoryWrapper
    {
        public MenuCategoryWrapper()
        {
            Category = new List<CategoryDto>();
            Menu = new List<MenuDto>();
        }

        [XmlElement("Category")]
        public List<CategoryDto> Category { get; set; }
        [XmlElement("Menu")]
        public List<MenuDto> Menu { get; set; }
    }
}

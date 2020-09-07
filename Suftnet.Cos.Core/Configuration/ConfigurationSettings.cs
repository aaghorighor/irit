namespace Suftnet.Cos.Core
{
    using System.Collections.Specialized;

    public static class ConfigurationSettings
    {
        public const string SECTION_NAME = "oncChurch/oncChurchSettings";      
        private static NameValueCollection m_AppSettings = null;      

        public static NameValueCollection AppSettings
        {
            get
            {
                if (m_AppSettings == null)
                {
                    m_AppSettings = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection(SECTION_NAME);
                    if (m_AppSettings == null)
                    {
                        throw new System.Configuration.ConfigurationErrorsException(string.Format("section {0} does not exists", SECTION_NAME));
                    }
                }
                return m_AppSettings;
            }        
        
        }
    }
}

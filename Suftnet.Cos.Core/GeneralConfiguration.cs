namespace Suftnet.Cos.Core
{    
    public class GeneralConfiguration
    {
        private static object llock = new object();
        private static CoreConfiguration configuration;

        public static CoreConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    lock (llock)
                    {
                        if (configuration == null)
                        {
                            configuration = new CoreConfiguration();                                                                    
                        }
                    }
                }

                return configuration;
            }
        }
    }
}

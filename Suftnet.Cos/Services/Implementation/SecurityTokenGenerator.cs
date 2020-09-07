namespace Suftnet.Cos.Web.Services
{
    using System;

    public static class SecurityTokenGenerator
    {
        static SecurityTokenGenerator()
        {
            SecretKey = "856FECBA3B06519C8DDDBC80BB080553";
        }

        public static string SecretKey { get; }
    }
}
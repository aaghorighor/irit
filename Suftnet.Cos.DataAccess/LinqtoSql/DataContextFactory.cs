namespace Suftnet.DataFactory.LinqToSql
{
    using Cos.DataAccess.Action;
    using System.Configuration;
    public static class DataContextFactory
    {
        private static readonly string _connectionString;
        static DataContextFactory()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
        }

        public static DataContext CreateContext()
        {
            return new DataContext(_connectionString);
        }
    }
}

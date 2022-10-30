using System;

namespace Shops.Api.Configuration
{
    public static class AppConfiguration
    {
        /* Default values */
        private static string _dbConnectionString = "Server=LAPTOP-NMF8R5Q0;Database=Shops;Trusted_Connection=True;";

        static AppConfiguration()
        {
            if (Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") != null)
                _dbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            
        }

        public static string DbConnectionString => _dbConnectionString;
    }
}
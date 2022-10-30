using System;

namespace Shops.Api.Configuration
{
    public static class AppConfiguration
    {
        /* Default values */
        private static string _dbConnectionString = "Server=LAPTOP-NMF8R5Q0;Database=Shops;Trusted_Connection=True;";
        private static string _apiAccessToken = "bdb477f2-679e-4b9b-aa81-a52ddfe23ca3";
        
        static AppConfiguration()
        {
            if (Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") != null)
                _dbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            
            if (Environment.GetEnvironmentVariable("API_ACCESS_TOKEN") != null)
                _apiAccessToken = Environment.GetEnvironmentVariable("API_ACCESS_TOKEN");
            
        }

        public static string DbConnectionString => _dbConnectionString;
        public static string ApiAccessToken => _apiAccessToken;
    }
}
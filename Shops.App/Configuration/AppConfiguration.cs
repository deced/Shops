using System;

namespace Shops.App.Configuration
{
    public static class AppConfiguration
    {
        /* Default values */
        private static string _apiAccessToken = "bdb477f2-679e-4b9b-aa81-a52ddfe23ca3";
        private static string _apiUrl = "https://localhost:5001/";

       static AppConfiguration()
        {
            if (Environment.GetEnvironmentVariable("API_ACCESS_TOKEN") != null)
                _apiAccessToken = Environment.GetEnvironmentVariable("API_ACCESS_TOKEN");
            
            if (Environment.GetEnvironmentVariable("API_URL") != null)
                _apiUrl = Environment.GetEnvironmentVariable("API_URL");
        }

        public static string ApiAccessToken => _apiAccessToken;

        public static string ApiUrl => _apiUrl;
    }
}
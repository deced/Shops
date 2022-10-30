using System;

namespace Shops.Api.Configuration
{
    public interface IDatabaseConfiguration
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
    public class DatabaseConfiguration : IDatabaseConfiguration
    {   
        public DatabaseConfiguration(){
            _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            _databaseName = Environment.GetEnvironmentVariable("DATABASE");  
        }
        private string _databaseName {get; set;}
        private string _connectionString {get; set;}
        
        public string DatabaseName 
        { 
            get => _databaseName;
            set => _databaseName ??= value;
        }
        public string ConnectionString 
        { 
            get => _connectionString;
            set => _connectionString ??= value;
        }
    }
}
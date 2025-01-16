﻿namespace API.Config
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsersCollectionName { get; set; }
        public string RecipiesCollectionName { get; set; }
    }
}

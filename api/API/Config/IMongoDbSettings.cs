namespace API.Config
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string UsersCollectionName { get; set; }
        string RecipiesCollectionName { get; set; }
    }
}

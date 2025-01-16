using API.Config;
using API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserModel> _usersCollection;

        public UserService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<UserModel>(mongoDbSettings.Value.UsersCollectionName);
        }

        public async Task<UserModel> VerifyUserAsync(string username, string password)
        {
            return await _usersCollection.Find(u => u.Name == username && u.Password == password).FirstOrDefaultAsync();
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _usersCollection.Find(_ => true).ToListAsync();
        }

        public async Task CreateUserAsync(UserModel user)
        {
            var existingUser = await _usersCollection.Find(u => u.Name == user.Name).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            // Insert the new user
            await _usersCollection.InsertOneAsync(user);
        }
    }
}

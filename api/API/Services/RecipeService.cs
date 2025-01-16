using API.Config;
using API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API.Services
{
    public class RecipeService
    {
        private readonly IMongoCollection<RecipeModel> _recipiesCollection;

        public RecipeService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _recipiesCollection = mongoDatabase.GetCollection<RecipeModel>(mongoDbSettings.Value.RecipiesCollectionName);
        }

        // Add a new recipe
        public async Task AddRecipeAsync(RecipeModel recipe)
        {
            if (string.IsNullOrEmpty(recipe.Id))
            {
                recipe.Id = ObjectId.GenerateNewId().ToString(); // Generate a new ObjectId
            }
            await _recipiesCollection.InsertOneAsync(recipe);
        }

        // Get all recipes
        public async Task<List<RecipeModel>> GetAllRecipesAsync()
        {
            return await _recipiesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<RecipeModel?> GetRecipeByIdAsync(string id)
        {
            try
            {
                return await _recipiesCollection.Find(r => r.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the recipe.", ex);
            }
        }
    }
}

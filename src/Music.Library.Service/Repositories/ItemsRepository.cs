using MongoDB.Driver;
using Music.Library.Service.Entities;

namespace Music.Library.Service.Repositories
{
    public class ItemsRepository
    {
        private const string collectionName = "albums";
        private readonly IMongoCollection<Album> dbCollection;
        private readonly FilterDefinitionBuilder<Album> filterBuilder = Builders<Album>.Filter;

        public ItemsRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Music");
            dbCollection = database.GetCollection<Album>(collectionName);
        }

        public async Task<IReadOnlyCollection<Album>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Album> GetAsync(Guid id) {
            FilterDefinition<Album> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Album entity) {
            if (entity == null) {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Album entity) {
            if (entity == null) {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<Album> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);

        }

        public async Task RemoveAsync(Guid id) {
            FilterDefinition<Album> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);

        }
    }
}
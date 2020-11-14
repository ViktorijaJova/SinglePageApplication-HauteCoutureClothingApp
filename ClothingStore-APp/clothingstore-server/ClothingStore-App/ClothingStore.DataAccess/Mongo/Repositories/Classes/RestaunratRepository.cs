using ClothingStore.DataAccess.Mongo.Repositories.Interfaces;
using ClothingStore.DomainModels.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.DataAccess.Mongo.Repositories.Classes
{
    public class RestaunratRepository : MongoDbRepository<Store>, IRestaurantRepository
    {
        public RestaunratRepository(string connectionString, string databaseName)
            : base(connectionString, databaseName) { }

        protected override string GetCollectionName()
        {
            return typeof(Store).Name + "s";
        }

        public async Task InsertRestaurantAsync(Store restaurant)
        {
            await InsertAsync(restaurant);
        }

        public async Task<Store> GetRestaurantByIdAsync(string id)
        {
            return await MongoCollection.Find(Builders<Store>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        public async Task<List<Store>> GetRestaurantsAsync(Expression<Func<Store, bool>> filter)
        {
            return await MongoCollection.Find(filter).ToListAsync();
        }

        public async Task UpdateRestaurantAsync(Store restaurant)
        {
            FilterDefinition<Store> obj = MapFilter(restaurant.Id);

            var updateDefinition = Builders<Store>.Update
                                    .Set(r => r.Id, restaurant.Id)
                                    .Set(r => r.Name, restaurant.Name)
                                    .Set(r => r.Address, restaurant.Address)
                                    .Set(r => r.Municipality, restaurant.Municipality)
                                    .Set(r => r.Collections, restaurant.Collections);

            await UpdateOneAsync(obj, updateDefinition);
        }

        public async Task DeleteRestaurantByIdAsync(string id)
        {
            FilterDefinition<Store> obj = MapFilter(id);
            await DeleteManyAsync(obj);
        }

        //map
        private FilterDefinition<Store> MapFilter(string id)
        {
            FilterDefinition<Store> filter = Builders<Store>.Filter.Empty;
            return filter & Builders<Store>.Filter.Where(item => item.Id == id);
        }
    }
}

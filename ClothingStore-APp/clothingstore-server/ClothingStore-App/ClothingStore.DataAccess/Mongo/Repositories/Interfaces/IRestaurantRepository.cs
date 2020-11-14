using ClothingStore.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClothingStore.DataAccess.Mongo.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        Task InsertRestaurantAsync(Store restaurant);
        Task<Store> GetRestaurantByIdAsync(string id);
        Task<List<Store>> GetRestaurantsAsync(Expression<Func<Store, bool>> filter);
        Task UpdateRestaurantAsync(Store restaurant);
        Task DeleteRestaurantByIdAsync(string id);
    }
}

using ClothingStore.DomainModels.Models;
using ClothingStore.RequestModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Services.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task CreateNewRestaurantAsync(StoreRequestModel model);
        Task<List<StoreRequestModel>> GetRestaurantsAsync(StoreRequestModel requestModel);
        Task DeleteRestaurantByIdAsync(string id);
        Task UpdateRestaurantAsync(UpdateRestaunratRequestModel requestModel);
        Task UpdateRestaurantMenuAsync(UpdateStoreItemRequestModel requestModel);
        Task<Store> GetRestaurantByIdAsync(string id);
        Task DeleteRestaurantMenuItemAsync(Store restaurant, string menuItemId);
    }
}

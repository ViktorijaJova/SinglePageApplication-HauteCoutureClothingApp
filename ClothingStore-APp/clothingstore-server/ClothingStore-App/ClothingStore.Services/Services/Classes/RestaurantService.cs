using ClothingStore.DataAccess.Mongo.Repositories.Interfaces;
using ClothingStore.DomainModels.Enums;
using ClothingStore.DomainModels.Models;
using ClothingStore.RequestModels.Models;
using ClothingStore.Services.Helpers;
using ClothingStore.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Services.Services.Classes
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task CreateNewRestaurantAsync(StoreRequestModel model)
        {
            var dtoRestaurant = new Store()
            {
                Name = model.Name,
                Address = model.Address,
                Municipality = (Municipality)model.Municipality,
                Collections = new List<StoreItem>()
            };

            await _restaurantRepository.InsertRestaurantAsync(dtoRestaurant);
        }

        public async Task<Store> GetRestaurantByIdAsync(string id)
        {
            return await _restaurantRepository.GetRestaurantByIdAsync(id);
        }

        public async Task<List<StoreRequestModel>> GetRestaurantsAsync(StoreRequestModel requestModel)
        {
            Expression<Func<Store, bool>> filter = f => true;

            if (!string.IsNullOrEmpty(requestModel.Name))
            {
                filter = filter.AndAlso(x => x.Name.ToLower().Contains(requestModel.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(requestModel.Address))
            {
                filter = filter.AndAlso(x => x.Address.ToLower().Contains(requestModel.Address.ToLower()));
            }

            if (requestModel.Municipality.HasValue)
            {
                filter = filter.AndAlso(x => x.Municipality == requestModel.Municipality);
            }

            var restaurantList = await _restaurantRepository.GetRestaurantsAsync(filter);

            var mapToRestaurantRequestModel = new List<StoreRequestModel>();

            foreach (var restaurant in restaurantList)
            {
                var tempModel = new StoreRequestModel()
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Address = restaurant.Address,
                    Municipality = restaurant.Municipality,
                    Collections= restaurant.Collections
                };

                mapToRestaurantRequestModel.Add(tempModel);
            }

            return mapToRestaurantRequestModel;
        }

        public async Task DeleteRestaurantByIdAsync(string id)
        {
            await _restaurantRepository.DeleteRestaurantByIdAsync(id);
        }

        public async Task UpdateRestaurantAsync(UpdateRestaunratRequestModel requestModel)
        {
            var restaurant = await GetRestaurantByIdAsync(requestModel.Id);
            restaurant.Name = requestModel.Name;
            restaurant.Address = requestModel.Address;
            restaurant.Municipality = requestModel.Municipality;

            await _restaurantRepository.UpdateRestaurantAsync(restaurant);
        }

        public async Task UpdateRestaurantMenuAsync(UpdateStoreItemRequestModel requestModel)
        {
            var restaurant = await GetRestaurantByIdAsync(requestModel.Id);
            var menuItem = requestModel;

            if (string.IsNullOrEmpty(menuItem.Id))   
            {
                var dtoMenuItem = new StoreItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = menuItem.Name,
                    Material = menuItem.Material,
                    IsAnimalCrueltyFree = menuItem.IsAnimalCrueltyFree,
                    Price = menuItem.Price,
                    ItemType = menuItem.ItemType
                };

                restaurant.Collections.Add(dtoMenuItem);
            }
            else
            {
                for (int i = 0; i < restaurant.Collections.Count; i++)
                {
                    if (restaurant.Collections[i].Id == menuItem.Id)
                    {
                        restaurant.Collections[i].Id = menuItem.Id;
                        restaurant.Collections[i].Name = menuItem.Name;
                        restaurant.Collections[i].Material = menuItem.Material;
                        restaurant.Collections[i].Price = menuItem.Price;
                        restaurant.Collections[i].IsAnimalCrueltyFree = menuItem.IsAnimalCrueltyFree;
                        restaurant.Collections[i].ItemType = menuItem.ItemType;
                        break;
                    }
                }
            }

            await _restaurantRepository.UpdateRestaurantAsync(restaurant);
        }

        public async Task DeleteRestaurantMenuItemAsync(Store restaurant, string menuItemId)
        {
            var menuItem = restaurant.Collections.FirstOrDefault(x => x.Id == menuItemId);
            restaurant.Collections.Remove(menuItem);
            await _restaurantRepository.UpdateRestaurantAsync(restaurant);
        }
    }
}

using ClothingStore.DomainModels.Enums;
using ClothingStore.RequestModels.Models;
using ClothingStore.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IRestaurantService _storeService;
        public StoreController(IRestaurantService storeService)
        {
            _storeService = storeService;
        }

        //api/Store/AddStore
        [HttpPost("AddStore")]
        public async Task<IActionResult> AddStoreAsync([FromBody] StoreRequestModel model)
        {
            await _storeService.CreateNewRestaurantAsync(model);
            return Ok();
        }

        //api/Store/GetStores?queryParameter (ex: name)
        [HttpGet("GetStores")]
        public async Task<IActionResult> GetStoresAsync([FromQuery] string name,
                                                             [FromQuery] string address,
                                                             [FromQuery] Municipality? municipality)
        {
            var requestModel = new StoreRequestModel()
            {
                Name = name,
                Address = address,
                Municipality = municipality
            };

            var response = await _storeService.GetRestaurantsAsync(requestModel);
            return Ok(response);
        }

        //api/Store/UpdateStore
        [HttpPut("UpdateStore")]
        public async Task<IActionResult> UpdateStoreAsync([FromBody] UpdateRestaunratRequestModel requestModel)
        {
            await _storeService.UpdateRestaurantAsync(requestModel);
            return Ok();
        }

        //api/Store/DeleteStore
        [HttpDelete("DeleteStore")]
        public async Task<IActionResult> DeleteStoreAsync([FromQuery] string id)
        {
            await _storeService.DeleteRestaurantByIdAsync(id);
            return Ok();
        }

        //api/Store/UpdateStoreCollection
        [HttpPut("UpdateStoreCollection")]
        public async Task<IActionResult> UpdateStoreCollectionAsync([FromBody] UpdateStoreItemRequestModel requestModel)
        {
            await _storeService.UpdateRestaurantMenuAsync(requestModel);
            return Ok();
        }

        //api/Restaurants/GetRestaurantMenuItems queries are optional ex: ?name=testrestaurant
        [HttpGet("GetStoreCollectionItems")]
        public async Task<IActionResult> GetStoreCollectionItemsAsync([FromQuery] string storeId,
                                                                     [FromQuery] string name)
        {
            var restaurant = await _storeService.GetRestaurantByIdAsync(storeId);
            var menuItems = restaurant.Collections;

            if (!string.IsNullOrEmpty(name))
            {
                menuItems = restaurant.Collections.FindAll(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return Ok(menuItems);
        }

        //api/Store/DeleteCollectionItem
        [HttpDelete("DeleteCollectionItem")]
        public async Task<IActionResult> DeleteCollectionItemAsync([FromQuery] string storeId,
                                                             [FromQuery] string collectionItemId)
        {
            var store = await _storeService.GetRestaurantByIdAsync(storeId);
            await _storeService.DeleteRestaurantMenuItemAsync(store, collectionItemId);
            return Ok();
        }

    }
}

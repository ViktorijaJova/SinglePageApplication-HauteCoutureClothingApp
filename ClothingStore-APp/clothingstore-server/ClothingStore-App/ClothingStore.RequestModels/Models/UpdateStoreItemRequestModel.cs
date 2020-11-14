using ClothingStore.DomainModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.RequestModels.Models
{
  public  class UpdateStoreItemRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Material { get; set; }
        public bool IsAnimalCrueltyFree { get; set; }
        public ItemType ItemType { get; set; }
    }
}

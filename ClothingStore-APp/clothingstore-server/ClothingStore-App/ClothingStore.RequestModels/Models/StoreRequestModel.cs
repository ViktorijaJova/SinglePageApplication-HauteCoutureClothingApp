using ClothingStore.DomainModels.Enums;
using ClothingStore.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.RequestModels.Models
{
  public  class StoreRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Municipality? Municipality { get; set; }
        public List<StoreItem> Collections { get; set; }
    }
}

using ClothingStore.DomainModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.RequestModels.Models
{
   public class UpdateRestaunratRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Municipality Municipality { get; set; }
        public StoreItemRequestModel StoreItem { get; set; }

    }
}

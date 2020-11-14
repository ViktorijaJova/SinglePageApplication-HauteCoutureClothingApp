using System;
using ClothingStore.DomainModels.Enums;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace ClothingStore.DomainModels.Models
{
    public class Store
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Municipality Municipality { get; set; }
        public List<StoreItem> Collections { get; set; }

    }
}

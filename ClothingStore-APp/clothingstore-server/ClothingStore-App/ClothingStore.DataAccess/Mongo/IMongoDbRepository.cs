using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.DataAccess.Mongo
{
    public interface IMongoDbRepository<T>
    {
        IMongoQueryable<T> Collection { get; }

    }
}

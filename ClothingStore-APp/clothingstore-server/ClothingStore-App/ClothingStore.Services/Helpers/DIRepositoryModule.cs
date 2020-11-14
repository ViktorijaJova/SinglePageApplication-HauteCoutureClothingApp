using ClothingStore.DataAccess.Mongo.Repositories.Classes;
using ClothingStore.DataAccess.Mongo.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Services.Helpers
{
    public static class DIRepositoryModule
    {
        public static IServiceCollection RegisterRepositories(IServiceCollection services,
            string mongoConnectionString,
            string mongoDatabase)
        {
            //register mongodb repositories
            services
           .AddScoped<IRestaurantRepository, RestaunratRepository>(provider =>
           new RestaunratRepository(mongoConnectionString, mongoDatabase));

            return services;


        }
    }
}

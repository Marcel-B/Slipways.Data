﻿using Microsoft.Extensions.DependencyInjection;
using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Repositories;

namespace com.b_velop.Slipways.Data.Extensions
{
    public static class InitExtensions
    {
        public static IServiceCollection AddSlipwaysData(
            this IServiceCollection services,
            string cacheName = "cache")
        {
            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheName;
                options.InstanceName = "Slipways";
            });
            services.AddScoped<IExtraRepository, ExtraRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IManufacturerServicesRepository, ManufacturerServicesRepository>();
            services.AddScoped<IPortRepository, PortRepository>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISlipwayExtraRepository, SlipwayExtraRepository>();
            services.AddScoped<ISlipwayRepository, SlipwayRepository>();
            services.AddScoped<IStationRepository, StationRepository>();
            services.AddScoped<IWaterRepository, WaterRepository>();
            return services;
        }
    }
}
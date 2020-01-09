﻿using Microsoft.Extensions.DependencyInjection;
using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Slipways.Data.Extensions
{
    public static class InitExtensions
    {
        public static IServiceCollection AddSlipwaysData(
            this IServiceCollection services,
            string connectionString = null,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            services.AddMemoryCache();

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

            if (string.IsNullOrWhiteSpace(connectionString))
                return services;

            services.AddDbContext<SlipwaysContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.EnableDetailedErrors(true);
                options.EnableSensitiveDataLogging(true);
            }, serviceLifetime);

            return services;
        }
    }
}

using com.b_velop.Slipways.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Slipways.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ILogger<RepositoryWrapper> _logger;

        public IWaterRepository Water { get; }
        public IStationRepository Station { get; }
        public ISlipwayRepository Slipway { get; }
        public IExtraRepository Extra { get; }
        public IServiceRepository Service { get; }
        public IManufacturerRepository Manufacturer { get; }
        public IPortRepository Port { get; }
        public ISlipwayExtraRepository SlipwayExtra { get; }
        public IManufacturerServicesRepository ManufacturerServices { get; }
        public ISlipwayPortRepository SlipwayPort { get; }

        public RepositoryWrapper(
            IWaterRepository waterRepository,
            IStationRepository stationRepository,
            ISlipwayRepository slipwayRepository,
            IExtraRepository extraRepository,
            IServiceRepository serviceRepository,
            IManufacturerRepository manufacturerRepository,
            IPortRepository portRepository,
            ISlipwayExtraRepository slipwayExtraRepository,
            IManufacturerServicesRepository manufacturerServicesRepository,
            ISlipwayPortRepository slipwayPortRepository,
            ILogger<RepositoryWrapper> logger)
        {
            Water = waterRepository;
            Station = stationRepository;
            Slipway = slipwayRepository;
            Extra = extraRepository;
            Service = serviceRepository;
            Manufacturer = manufacturerRepository;
            Port = portRepository;
            SlipwayExtra = slipwayExtraRepository;
            ManufacturerServices = manufacturerServicesRepository;
            SlipwayPort = slipwayPortRepository;
            _logger = logger;
        }
    }
}

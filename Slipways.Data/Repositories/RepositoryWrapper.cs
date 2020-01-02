using com.b_velop.Slipways.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Slipways.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        //private readonly SlipwaysContext _context;
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

        public RepositoryWrapper(
            //SlipwaysContext context,
            IWaterRepository waterRepository,
            IStationRepository stationRepository,
            ISlipwayRepository slipwayRepository,
            IExtraRepository extraRepository,
            IServiceRepository serviceRepository,
            IManufacturerRepository manufacturerRepository,
            IPortRepository portRepository,
            ISlipwayExtraRepository slipwayExtraRepository,
            IManufacturerServicesRepository manufacturerServicesRepository,
            ILogger<RepositoryWrapper> logger)
        {
            //_context = context;
            Water = waterRepository;
            Station = stationRepository;
            Slipway = slipwayRepository;
            Extra = extraRepository;
            Service = serviceRepository;
            Manufacturer = manufacturerRepository;
            Port = portRepository;
            SlipwayExtra = slipwayExtraRepository;
            ManufacturerServices = manufacturerServicesRepository;
            _logger = logger;
        }
    }
}

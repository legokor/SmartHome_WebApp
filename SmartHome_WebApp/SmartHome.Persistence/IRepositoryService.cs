using SmartHome.Model;
using SmartHome.Persistence.Repositories;

namespace SmartHome.Persistence
{
    public interface IRepositoryService
    {
        IRepository<DataSample> DataSamples { get; set; }

        IRepository<Design> Desings { get; set; }

        IRepository<BuildingBlock> BuildingBlocks { get; set; }

        IRepository<MasterUnit> MasterUnits { get; set; }
    }
}
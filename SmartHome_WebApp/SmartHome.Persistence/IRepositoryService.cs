using SmartHome.Persistence.Repositories;

namespace SmartHome.Persistence
{
    public interface IRepositoryService
    {
        DataSampleRepository DataSamples { get; set; }

        DesignRepository Desings { get; set; }

        BuildingBlockRepository BuildingBlocks { get; set; }

        MasterUnitRepository MasterUnits { get; set; }

    }
}
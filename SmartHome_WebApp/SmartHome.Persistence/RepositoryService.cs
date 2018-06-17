using SmartHome.Model;
using SmartHome.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Persistence
{
    public class RepositoryService: IRepositoryService
    {

        public RepositoryService()
        {
            DataSamples = new DataSampleRepository();
            Desings = new DesignRepository();
            BuildingBlocks = new BuildingBlockRepository();
            MasterUnits = new MasterUnitRepository();
        }

        public IRepository<DataSample> DataSamples { get; set; }

        public IRepository<Design> Desings { get; set; }

        public IRepository<BuildingBlock> BuildingBlocks { get; set; }

        public IRepository<MasterUnit> MasterUnits { get; set; }

    }
}

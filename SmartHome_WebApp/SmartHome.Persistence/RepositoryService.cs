using SmartHome.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Persistence
{
    public class RepositoryService
    {

        public RepositoryService()
        {
            DataSamples = new DataSampleRepository();
            Desings = new DesignRepository();
            BuildingBlocks = new BuildingBlockRepository();
            MasterUnits = new MasterUnitRepository();
        }

        public DataSampleRepository DataSamples { get; set; }

        public DesignRepository Desings { get; set; }

        public BuildingBlockRepository BuildingBlocks { get; set; }

        public MasterUnitRepository MasterUnits { get; set; }

    }
}

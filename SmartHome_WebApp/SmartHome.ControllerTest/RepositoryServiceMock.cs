using SmartHome.Model;
using SmartHome.Persistence;
using SmartHome.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHome.ControllerTest
{
    public class RepositoryServiceMock : IRepositoryService
    {

        public RepositoryServiceMock(IRepository<DataSample> dataSamples,
            IRepository<Design> designs,
            IRepository<BuildingBlock> buildingBlocks,
            IRepository<MasterUnit> masterUnits)
        {
            DataSamples = dataSamples;
            Desings = designs;
            BuildingBlocks = buildingBlocks;
            MasterUnits = masterUnits;
        }

        public IRepository<DataSample> DataSamples { get; set; }

        public IRepository<Design> Desings { get; set; }

        public IRepository<BuildingBlock> BuildingBlocks { get; set; }

        public IRepository<MasterUnit> MasterUnits { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using SmartHome_WebApp.Data.Repositories;
using SmartHomeWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome_WebApp.Data
{
    public class RepositoryService
    {

        public RepositoryService()
        {
            DataSamples = new DataSampleRepository();
            Desings = new DesignRepository();
            BuildingBlocks = new BuildingBlockRepository();
        }

        public DataSampleRepository DataSamples { get; set; }

        public DesignRepository Desings { get; set; }

        public BuildingBlockRepository BuildingBlocks { get; set; }

    }
}

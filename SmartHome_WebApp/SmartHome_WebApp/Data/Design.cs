using SmartHomeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHomeWebApp.Data
{
    public class Design
    {
        public Guid Id { get; set; }

        public List<BuildingBlock> Sequence { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}

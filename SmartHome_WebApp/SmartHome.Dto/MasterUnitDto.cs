using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartHome.Dto
{
    public class MasterUnitDto
    {
        public Guid Id { get; set; }

        public string CustomName { get; set; }

        public bool IsOn { get; set; }

        public Guid OwnerId { get; set; }

    }
}

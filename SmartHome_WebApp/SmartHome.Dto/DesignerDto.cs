using SmartHome.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartHome.Dto
{
    public class DesignerDto
    {
        public Guid Id { get; set; }

        [Required]
        public List<BuildingBlock> Sequence { get; set; }
    }
}

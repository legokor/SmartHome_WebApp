using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartHome.Model
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User: IdentityUser<Guid> 
    {
        public List<Design> UserCreatedDesigns { get; set; }

        public List<MasterUnit> MasterUnits { get; set; }
    }
}

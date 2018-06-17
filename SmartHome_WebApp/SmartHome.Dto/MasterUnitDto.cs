using SmartHome.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartHome.Dto
{
    public class MasterUnitDto
    {
        public Guid Id { get; set; }

        public Guid eTag { get; set; }

        public string CustomName { get; set; }

        public bool IsOn { get; set; }

        public Guid OwnerId { get; set; }

        static public MasterUnitDto Mapper(MasterUnit origin)
        {
            return new MasterUnitDto
            {
                Id = origin.Id,
                CustomName = origin.CustomName,
                IsOn = origin.IsOn,
                OwnerId = origin.UserId,
                eTag = origin.ConcurrencyLock
            };
        }
        static public IEnumerable<MasterUnitDto> Mapper(IEnumerable<MasterUnit> origin)
        {
            var resultList = new List<MasterUnitDto>();
            foreach (var item in origin)
            {
                resultList.Add(Mapper(item));
            }
            return resultList;
        }

        static public MasterUnit Mapper(MasterUnitDto origin, User owner)
        {
            return new MasterUnit
            {
                Id = origin.Id,
                CustomName = origin.CustomName,
                IsOn = origin.IsOn,
                User = owner,
                UserId = origin.OwnerId,
                ConcurrencyLock = origin.eTag,
                DataSamples = null,
                Designs = null
            };
        }

    }
}

using System;


namespace CSRTool.Core
{
    public class Territory : ObjectInfo
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        //public TerritoryType TerritoryType { get; set; }
        public Guid TerritoryTypeId { get; set; }

        public static Territory Create(string name, string shortName, TerritoryType territoryType)
        {
            var ret = new Territory{
                Id              = Guid.NewGuid(),
                Name            = name,
                ShortName       = shortName,
                //TerritoryType   = territoryType,
                IsActive        = false
            };

            return ret;
        }
    }
}

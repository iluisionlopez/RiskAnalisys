using System;
using CSRTool.Core;

namespace CSRTool.Core
{
    public class TerritoryType : ObjectInfo
    {
        public string Name { get; set; }

        public TerritoryType Create(string name)
        {
            var ret = new TerritoryType
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            return ret;
        }

    }
}

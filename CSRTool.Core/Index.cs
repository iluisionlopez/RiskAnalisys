using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Core
{
    public class Index : ObjectInfo
    {
        public decimal Value { get; set; }
        public Guid IndexTypeId { get; set; }
        public IndexType IndexType { get; set; }
        public Guid TerritoryId { get; set; }
        public Guid VersionId { get; set; }
    }
}

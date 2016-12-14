using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IIndexService
    {
        List<Index> GetIndexes();
        List<Index> GetIndexes(Guid territoryId, Guid versionId);
  
    }
}
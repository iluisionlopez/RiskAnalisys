using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IIndexRepository
    {
        List<Index> GetIndexes();
        List<Index> GetIndexes(Guid territoryId, Guid versionId);
    }
}
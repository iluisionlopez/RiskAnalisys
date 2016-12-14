using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface ITerritoryRepository
    {
        List<Territory> GetTerritories(bool onlyActive = true);

        bool SaveTerritory(Territory coreTerritory);

    }
}

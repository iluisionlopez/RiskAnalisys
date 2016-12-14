using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class TerritoryService : ITerritoryService
    {
        private readonly ITerritoryRepository _territoryRepository;

        public TerritoryService(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }

        public List<Territory> GetTerritories()
        {
            return _territoryRepository.GetTerritories();
        }
    }
}
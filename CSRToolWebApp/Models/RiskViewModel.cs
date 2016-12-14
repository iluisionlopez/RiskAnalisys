using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Models
{
    public class RiskViewModel
    {
        #region Properties
        public string selectedTerritory { get; set; }
        public string selectedVersion { get; set; }
        public string selectedOfferTypes { get; set; }
        public string selectedSector { get; set; }
        public string selectedWebscanValues { get; set; }
        public string selectedSupplyChain { get; set; }
        public string selectedAuditValue { get; set; }
        public bool isExinting { get; set; }
        public bool hidePeaceIndex { get; set; }
        public IEnumerable<QuestionModel> questions { get; set; }
        #endregion
    }
}
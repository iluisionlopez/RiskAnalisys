using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Core
{
    public class AssessmentSupplier : ObjectInfo
    {
        public string Name { get; set; }
        public Guid AssessmentTypeId { get; set; }
        public Guid AssessorId { get; set; }
        public DateTime Date { get; set; }
        public Guid SupplierId { get; set; }
        public string Site { get; set; }
        public Guid TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public Guid VersionId { get; set; }
        public string VersionName { get; set; }
        public string Commodity { get; set; }
        public Guid CaseTypeId { get; set; }
        public Guid SectorId { get; set; }
        public string Buyer { get; set; }
        public string Auditor { get; set; }
        public DateTime AuditDate { get; set; }
        public double PerformanceRating { get; set; }
        public double RiskRating { get; set; }
        public string RatingComment { get; set; }
        public bool Complete { get; set; }
        public Guid SupplyChainID { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual SupplyChain SupplyChain { get; set; }

        public virtual ICollection<AssessmentSupplierQuestionAnswer> AssessmentSupplierQuestionAnswer { get; set; }

    }
}

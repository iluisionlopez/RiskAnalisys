using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CSRTool.Core
{
    public class AssessmentCustomer : ObjectInfo
    {
        public string Name { get; set; }
        public Guid AssessmentTypeId { get; set; }
        public Guid AssessorId { get; set; }
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public string Site { get; set; }
        public string Project { get; set; }
        public string Segment { get; set; }
        public Guid TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public Guid VersionId { get; set; }
        public string VersionName { get; set; }
        public string ScaniaDepartment { get; set; }
        public Guid CaseTypeId { get; set; }
        public Guid SectorId { get; set; }
        public string Comment { get; set; }
        public bool IsComplete { get; set; }
        public decimal RiskIndication { get; set; }
        public string ResponibleSalesPerson { get; set; }


        public List<object> AssessmentTransactionType { get; set; }
        public List<TransactionType> TransactionTypes { get; set; }
        public List<object> AssessmentOfferType { get; set; }
        public List<OfferType> OfferTypes { get; set; }
        public virtual ICollection<AssessmentCustomerQuestionAnswer> AssessmentQuestions { get; set; }

    }
}
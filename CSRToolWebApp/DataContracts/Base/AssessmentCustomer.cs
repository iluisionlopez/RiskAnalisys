using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "AssessmentCustomer", Namespace = "http://xmlns.scania.com/csrtool/schema/assessmentcustomer/v1")]
    public class AssessmentCustomer
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public Guid AssessmentTypeId { get; set; }

        [DataMember(Order = 2)]
        public Guid AssessorId { get; set; }

        [DataMember(Order = 3)]
        public string Date { get; set; }

        [DataMember(Order = 4)]
        public Guid CustomerId { get; set; }

        [DataMember(Order = 5)]
        public string Site { get; set; }

        [DataMember(Order = 6)]
        public string Project { get; set; }

        [DataMember(Order = 7)]
        public string Segment { get; set; }

        [DataMember(Order = 8)]
        public Guid TerritoryId { get; set; }

        [DataMember(Order = 9)]
        public string TerritoryName { get; set; }

        [DataMember(Order = 10)]
        public Guid VersionId { get; set; }

        [DataMember(Order = 11)]
        public string VersionName { get; set; }

        [DataMember(Order = 12)]
        public string ScaniaDepartment { get; set; }

        [DataMember(Order = 13)]
        public Guid CaseTypeId { get; set; }

        [DataMember(Order = 14)]
        public Guid SectorId { get; set; }

        [DataMember(Order = 15)]
        public string Comment { get; set; }

        [DataMember(Order = 16)]
        public string Created { get; set; }

        [DataMember(Order = 17)]
        public Guid CreatedBy { get; set; }

        [DataMember(Order = 18)]
        public string CreatedByName { get; set; }

        [DataMember(Order = 19)]
        public string Changed { get; set; }

        [DataMember(Order = 20)]
        public Guid ChangedBy { get; set; }

        [DataMember(Order = 21)]
        public string ChangedByName { get; set; }

        [DataMember(Order = 22)]
        public bool IsComplete { get; set; }

        [DataMember(Order = 23)]
        public decimal RiskIndication { get; set; }

        [DataMember(Order = 24)]
        public List<OfferType> OfferTypes { get; set; }

        [DataMember(Order = 25)]
        public List<TransactionType> TransactionTypes { get; set; }

        [DataMember(Order = 26)]
        public string ResponibleSalesPerson { get; set; }

        [DataMember(Order= 27)]
        public virtual ICollection<AssessmentCustomerQuestionAnswer> AssessmentQuestions { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "AssessmentSupplier", Namespace = "http://xmlns.scania.com/csrtool/schema/assessmentsupplier/v1")]
    public class AssessmentSupplier
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public Guid AssessmentTypeId { get; set; }

        [DataMember(Order = 3)]
        public Guid AssessorId { get; set; }

        [DataMember(Order = 4)]
        public string Date { get; set; }

        [DataMember(Order = 5)]
        public Guid SupplierId { get; set; }

        [DataMember(Order = 6)]
        public string Site { get; set; }

        [DataMember(Order = 7)]
        public Guid TerritoryId { get; set; }

        [DataMember(Order = 8)]
        public string TerritoryName { get; set; }

        [DataMember(Order = 9)]
        public Guid VersionId { get; set; }

        [DataMember(Order = 10)]
        public string VersionName { get; set; }

        [DataMember(Order = 11)]
        public string Commodity { get; set; }

        [DataMember(Order = 12)]
        public Guid CaseTypeId { get; set; }

        [DataMember(Order = 13)]
        public Guid SectorId { get; set; }

        [DataMember(Order = 14)]
        public string Buyer { get; set; }

        [DataMember(Order = 15)]
        public string Auditor { get; set; }

        [DataMember(Order = 16)]
        public string AuditDate { get; set; }

        [DataMember(Order = 17)]
        public double PerformanceRating { get; set; }

        [DataMember(Order = 18)]
        public double RiskRating { get; set; }

        [DataMember(Order = 19)]
        public string RatingComment { get; set; }

        [DataMember(Order = 20)]
        public bool Complete { get; set; }

        [DataMember(Order = 21)]
        public Guid SupplyChainID { get; set; }

        [DataMember(Order = 22)]
        public string Created { get; set; }

        [DataMember(Order = 23)]
        public Guid CreatedBy { get; set; }

        [DataMember(Order = 24)]
        public string Changed { get; set; }

        [DataMember(Order = 25)]
        public Guid ChangedBy { get; set; }

        [DataMember(Order = 26)]
        public bool IsActive { get; set; }
    }
}
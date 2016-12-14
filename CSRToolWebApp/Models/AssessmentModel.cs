using CSRToolWebApp.Common;
using CSRToolWebApp.Common.ExtensionMethods;
using CSRToolWebApp.DataContracts.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSRToolWebApp.Models
{
    public class AssessmentModel
    {
        public Guid VersionId { get; set; }
        public string Version { get; set; }
        public Guid TerritoryId { get; set; }
        public string Territory { get; set; }
        public Guid AssessmentID { get; set; }
        public string AssessmnetName { get; set; }
        public string DateCreated { get; set; }
        public string DateChanged { get; set; }
        public string Controller { get; set; }
        public string Status { get; set; }
        public UserModel Assessor { get; set; }
        public IEnumerable<AssessmentModel> AssessmentList { get; set; }

        #region Constructors
        public AssessmentModel()
        {
            AssessmentList = new List<AssessmentModel>();
        }

        public AssessmentModel(AssessmentCustomer assessment)
        {
            Version = assessment.VersionName;
            VersionId = assessment.VersionId;
            Territory = assessment.TerritoryName;
            TerritoryId = assessment.TerritoryId;
            AssessmnetName = assessment.Name;
            AssessmentID = assessment.Id;
            DateCreated = DateTime.Parse(assessment.Created).Date.ToShortDateString();
            DateChanged = DateTime.Parse(assessment.Changed).Date.ToShortDateString();
            Controller = "Sales";
            var status = assessment.IsComplete ? 1 : 0;
            Status = ((AssessmentStatus)status).ToString();
        }

        public AssessmentModel(AssessmentSupplier assessment)
        {
            Version = assessment.VersionName;
            VersionId = assessment.VersionId;
            Territory = assessment.TerritoryName;
            TerritoryId = assessment.TerritoryId;
            AssessmnetName = assessment.Name;
            AssessmentID = assessment.Id;
            DateCreated = DateTime.Parse(assessment.Created).Date.ToShortDateString();
            DateChanged = DateTime.Parse(assessment.Changed).Date.ToShortDateString();
            Controller = "Purchasing";
            var status = assessment.Complete ? 1 : 0;
            Status = ((AssessmentStatus)status).ToString();
        }

        #endregion

        public IEnumerable<AssessmentModel> GetUserAssessmnets()
        {
            var assessments = GetDummyUserAssessments(Assessor);
            return assessments;
        }

        public static IEnumerable<AssessmentModel> GetCustomerAssessmnets(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<AssessmentCustomer>>(json);
            var result = new List<AssessmentModel>();
            foreach (AssessmentCustomer assessment in dataContract)
            {
                var a = new AssessmentModel(assessment);
                result.Add(a);
            }

            return result;
        }

        public static IEnumerable<AssessmentModel> GetSupplierAssessmnets(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<AssessmentSupplier>>(json);
            var result = new List<AssessmentModel>();
            foreach (AssessmentSupplier assessment in dataContract)
            {
                var a = new AssessmentModel(assessment);
                result.Add(a);
            }

            return result;
        }

        public IEnumerable<AssessmentModel> GetDummyUserAssessments(UserModel assessor)
        {
            var assessments = new List<AssessmentModel>();
            int days = 1;
            int months = 1;

            var assessment = new AssessmentModel();

            var terriTorryList = TerritoryModel.GetTerritories(new RestService("territories")).ToList();
            var version = VersionModel.GetAssessmentVersion(new RestService("versions")).First();

            for (int i = 1; i <= 3; i++)
            {
                assessment = new AssessmentModel();
                assessment.Assessor = assessor;
                assessment.DateCreated = ((DateTime.Now).AddMonths(-1 * (months.GetRandomNumber(12))).AddDays(-1 * (days.GetRandomNumber(30)))).ToShortDateString();
                assessment.DateChanged = ((DateTime.Now).AddDays(-1 * (days.GetRandomNumber(30)))).ToShortDateString();
                assessment.TerritoryId = Guid.Parse(terriTorryList[i].Value);
                assessment.Territory = terriTorryList[i].Text;
                assessment.VersionId = Guid.Parse(version.Value);
                assessment.Version = version.Text;
                assessment.AssessmentID = new Guid();
                assessment.AssessmnetName = string.Concat("New Assessment ", assessment.Territory, " ", assessment.DateCreated);

                assessments.Add(assessment);
            }

            return assessments;
        }
    }

}
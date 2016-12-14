using CSRToolWebApp.Common;
using CSRToolWebApp.Common.ExtensionMethods;
using CSRToolWebApp.DataContracts.Base;
using CSRToolWebApp.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CSRToolWebApp.Models
{
    public class SupplierAssessmentModel
    {
        protected const string SearchString = @"environment OR labour OR human rights OR resettlement OR security";

        #region IDs and Relations
        public string UserId { get; set; }
        public string AssessmentID { get; set; }
        public string VersionID { get; set; }
        public string AssessmetTypeID { get; set; }
        #endregion

        #region General Information
        public string AssessmentDate { get; set; }
        public string Buyer { get; set; }
        public string Commodity { get; set; }
        public string Supplier { get; set; }
        public string Site { get; set; }
        public string DUNS { get; set; }
        public string Auditor { get; set; }
        public string AuditDate { get; set; }
        public string WebScan { get; set; }
        #endregion

        #region Selected Values
        public string SelectedCaseID { get; set; }
        public string SelectedSectorID { get; set; }
        public string SelectedCountryID { get; set; }
        public int selectedCommentWebScan { get; set; }
        public int selectedCommentAudit { get; set; }
        public int selectedSupplyChain { get; set; }
        public Tuple<int, int> Result { get; set; }
        public double RiskRaiting { get; set; }
        public string RatingComment { get; set; }
        public double PerformanceRaiting { get; set; }

        #endregion

        #region List Types
        public IEnumerable<CaseModel> CaseTypes { get; set; }
        public IEnumerable<SelectListItem> SupplyChain { get; set; }
        public IEnumerable<SelectListItem> SectorList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<IndexModel> IndexList { get; set; }
        public IEnumerable<QuestionModel> Questions { get; set; }
        public IEnumerable<RiskCategoryModel> Risks { get; set; }
        public IEnumerable<SelectListItem> CommentWebScanList { get; set; }
        public IEnumerable<SelectListItem> CommentAuditList { get; set; }
        #endregion


        public SupplierAssessmentModel()
        {
            Initialize();
        }

        public SupplierAssessmentModel(AssessmentSupplier assessment)
        {
            Initialize();
            UserId = UserModel.GetUserByID(assessment.AssessorId.ToString()).UserName;
            AssessmentID = assessment.Id.ToString();
            VersionID = assessment.VersionId.ToString();
            AssessmetTypeID = assessment.AssessmentTypeId.ToString();
            AssessmentDate = DateTime.Parse(assessment.Date).ToShortDateString();

            if (assessment.SupplierId != Guid.Empty)
            {
                var supplier = SupplierModel.GetSupplier(new RestService(string.Format("supplierbyid/?Id={0}", assessment.SupplierId.ToString())));
                Supplier = supplier.Name;
                DUNS = supplier.DUNS.ToString();
                WebScan = supplier.Name + " " + SearchString;
            }

            Buyer = assessment.Buyer;
            Commodity = assessment.Commodity;
            Site = assessment.Site;
            Auditor = assessment.Auditor;
            RiskRaiting = assessment.RiskRating;
            PerformanceRaiting = assessment.PerformanceRating;
            RatingComment = assessment.RatingComment;

            DateTime audit_date;
            if (DateTime.TryParse(assessment.AuditDate, out audit_date))
                AuditDate = audit_date.ToShortDateString();


            if (assessment.CaseTypeId != Guid.Empty)
            {
                SelectedCaseID = assessment.CaseTypeId.ToString();
                CaseTypes.FirstOrDefault(c => c.CaseID == assessment.CaseTypeId.ToString()).Selected = true;
            }

            if (assessment.TerritoryId != Guid.Empty)
            {
                SelectedCountryID = assessment.TerritoryId.ToString();
                CountryList.FirstOrDefault(cc => cc.Value == assessment.TerritoryId.ToString()).Selected = true;
            }
            if (assessment.SectorId != Guid.Empty)
            {
                SelectedSectorID = assessment.SectorId.ToString();
                SectorList.FirstOrDefault(s => s.Value == assessment.SectorId.ToString()).Selected = true;
            }
            if (assessment.SupplyChainID != Guid.Empty)
            {
                selectedSupplyChain = GetSupplyChainTypes().FirstOrDefault(x => x.Id == assessment.SupplyChainID).Value;
                SupplyChain.FirstOrDefault(s => s.Value == selectedSupplyChain.ToString()).Selected = true;
            }

            var service = new RestService(string.Format("supplierwebscan/?id={0}", AssessmentID));
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var scans = JsonConvert.DeserializeObject<List<WebScan>>(json);

            if (scans.Any(s => s.WebScanTypeId == Guid.Parse("61342231-14fa-4977-82b0-4b3fc931ad39"))) //Webscan1
            {
                var comment = scans.FirstOrDefault(s => s.WebScanTypeId == Guid.Parse("61342231-14fa-4977-82b0-4b3fc931ad39")).Comment;
                foreach (ScanCommentTypes type in Enum.GetValues(typeof(ScanCommentTypes)))
                {
                    if (type.DisplayName() == comment)
                    {
                        selectedCommentWebScan = (int)type;
                    }
                }

            }

            SetSelectedAnswers();

            if (Questions.FirstOrDefault(q => q.Name == "AuditQuestionSupplier").Answers.Any(a => a.Selected))
            {
                int selectedValue;
                if (int.TryParse(Questions.FirstOrDefault(q => q.Name == "AuditQuestionSupplier").Answers.FirstOrDefault(a => a.Selected).Value, out selectedValue))
                    selectedCommentAudit = selectedValue;
            }

            IndexList = IndexModel.GetIndexesByCuontry(assessment.TerritoryId.ToString(), assessment.VersionId.ToString());
            Risks = RiskCategoryModel.GetRiskCategories(new RestService("riskcategories"));
            Risks = Risks.ExcludeCategories(new[] { "Peace Index" });
            Risks.CalculateIndexesAverage(IndexList);
            Risks.NormalizeRisk();

            Result = new Tuple<int, int>(assessment.RiskRating.Performance_Y(), assessment.PerformanceRating.Performance_X());
        }

        internal static SupplierAssessmentModel GetAssessment(RestService restService)
        {
            var json = restService.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<AssessmentSupplier>(json);
            var result = new SupplierAssessmentModel(dataContract);
            return result;
        }

        internal GenericResponse Save(double? riskRaiting, double? performanceRaiting)
        {
            var service = new RestService("assessmentsupplier");
            GenericResponse res;
            if (AssessmentID == null)
            {
                //New Object;
                var newAssessment = NewData();
                if (riskRaiting.HasValue)
                    newAssessment.RiskRating = riskRaiting.Value;
                if (performanceRaiting.HasValue)
                    newAssessment.PerformanceRating = performanceRaiting.Value;
                var response = service.Post(JsonConvert.SerializeObject(newAssessment));
                res = JsonConvert.DeserializeObject<GenericResponse>(response);
                HandleSaveWebScans(res.Id.ToString());
            }
            else
            {
                //Update
                var refAsessment = UpdateDatacontract();
                if (riskRaiting.HasValue)
                    refAsessment.RiskRating = riskRaiting.Value;
                if (performanceRaiting.HasValue)
                    refAsessment.PerformanceRating = performanceRaiting.Value;
                var response = service.Post(JsonConvert.SerializeObject(refAsessment));
                res = JsonConvert.DeserializeObject<GenericResponse>(response);
                HandleSaveWebScans(AssessmentID);
                res.Id = Guid.Parse(AssessmentID);
            }
            if (Questions.Any())
            {
                var selectedAnswers = Questions.Where(q => q.Answers.Any(a => a.Selected));
                if (selectedAnswers.Any())
                {
                    QuestionModel.SavePurchasingQuestions(res.Id, selectedAnswers);
                }
            }
            AssessmentID = res.Id.ToString();
            return res;
        }

        private AssessmentSupplier UpdateDatacontract()
        {
            var restService = new RestService(string.Format("assessmentsupplier/?id={0}", AssessmentID));
            var json = restService.Get(new List<KeyValuePair<string, string>>());
            var refAssessment = JsonConvert.DeserializeObject<AssessmentSupplier>(json);

            var supplier = SupplierModel.GetSupplier(new RestService(string.Format("supplierbyname/?Name={0}", Supplier)));
            if (supplier == null)
                supplier = SupplierModel.Create(Supplier, int.Parse(DUNS));

            refAssessment.SupplierId = Guid.Parse(supplier.Id);
            refAssessment.Name = Supplier + " (" + VersionModel.GetAssessmentVersion(new RestService("versions")).First().Text + ")";
            refAssessment.Buyer = Buyer;
            refAssessment.Site = Site;
            refAssessment.Commodity = Commodity;
            refAssessment.RatingComment = RatingComment;
            refAssessment.Complete = IsAssessmentComplete();

            if (SelectedSectorID != "-1" && refAssessment.SectorId != Guid.Parse(SelectedSectorID))
                refAssessment.SectorId = Guid.Parse(SelectedSectorID);
            if (SelectedCountryID != "-1" && refAssessment.SectorId != Guid.Parse(SelectedCountryID))
                refAssessment.TerritoryId = Guid.Parse(SelectedCountryID);
            if (SelectedCaseID != "-1" && refAssessment.SectorId != Guid.Parse(SelectedCaseID))
                refAssessment.CaseTypeId = Guid.Parse(SelectedCaseID);
            if (selectedSupplyChain > -1)
            {
                var supplyTypes = GetSupplyChainTypes();
                refAssessment.SupplyChainID = supplyTypes.FirstOrDefault(x => x.Value == selectedSupplyChain).Id;
            }
            if (!string.IsNullOrEmpty(Auditor))
                refAssessment.Auditor = Auditor;
            if (!string.IsNullOrEmpty(AuditDate))
                refAssessment.AuditDate = AuditDate;

            return refAssessment;
        }

        private AssessmentSupplier NewData()
        {
            var assessment = new AssessmentSupplier();
            var supplier = SupplierModel.GetSupplier(new RestService(string.Format("supplierbyname/?Name={0}", Supplier)));
            if (supplier == null && !string.IsNullOrWhiteSpace(DUNS))
                supplier = SupplierModel.Create(Supplier, int.Parse(DUNS));
            if (supplier != null)
                assessment.SupplierId = Guid.Parse(supplier.Id);

            assessment.Name = Supplier + " (" + VersionModel.GetAssessmentVersion(new RestService("versions")).First().Text + ")";
            assessment.AssessorId = Guid.Parse(SessionHandler.LoggedInUser.Id);
            assessment.AssessmentTypeId = Guid.Parse(AssessmentTypeModel.GetCaseTypeByName("Supplier").TypeID);
            assessment.Complete = false;

            assessment.Site = Site;
            assessment.Buyer = Buyer;
            assessment.Commodity = Commodity;
            assessment.RatingComment = RatingComment;
            assessment.Complete = IsAssessmentComplete();

            if (!string.IsNullOrEmpty(Auditor))
                assessment.Auditor = Auditor;
            if (!string.IsNullOrEmpty(AuditDate))
                assessment.AuditDate = AuditDate;

            assessment.CreatedBy = Guid.Parse(SessionHandler.LoggedInUser.Id);
            assessment.Date = DateTime.Now.ToString();
            assessment.Created = DateTime.Now.ToString();
            assessment.Changed = DateTime.Now.ToString();
            assessment.ChangedBy = Guid.Parse(SessionHandler.LoggedInUser.Id);
            assessment.VersionId = Guid.Parse(VersionModel.GetAssessmentVersion(new RestService("versions")).First().Value);
            assessment.IsActive = true;

            if (!string.IsNullOrEmpty(SelectedSectorID) && SelectedSectorID != "-1")
                assessment.SectorId = Guid.Parse(SelectedSectorID);
            if (!string.IsNullOrEmpty(SelectedCountryID) && SelectedCountryID != "-1")
                assessment.TerritoryId = Guid.Parse(SelectedCountryID);
            if (!string.IsNullOrEmpty(SelectedCaseID) && SelectedCaseID != "-1")
                assessment.CaseTypeId = Guid.Parse(SelectedCaseID);
            if (selectedSupplyChain > -1)
            {
                var supplyTypes = GetSupplyChainTypes();
                assessment.SupplyChainID = supplyTypes.FirstOrDefault(x => x.Value == selectedSupplyChain).Id;
            }

            return assessment;
        }

        private void HandleSaveWebScans(string assessmenID)
        {
            var service = new RestService("assessmentsupplierwebscan");
            if (selectedCommentWebScan > -1)
            {
                var scan = new WebScan
                {
                    AssessmentId = Guid.Parse(assessmenID),
                    WebScanTypeId = Guid.Parse("61342231-14fa-4977-82b0-4b3fc931ad39"),
                    SearchString = Supplier,
                    Comment = ((ScanCommentTypes)selectedCommentWebScan).DisplayName()

                };
                var response = service.Post(JsonConvert.SerializeObject(scan));
            }
        }

        private void Initialize()
        {
            CaseTypes = new List<CaseModel>();
            SupplyChain = new List<SelectListItem>();
            CountryList = new List<SelectListItem>();
            IndexList = new List<IndexModel>();
            SectorList = new List<SelectListItem>();
            Risks = new List<RiskCategoryModel>();
            Questions = new List<QuestionModel>();
            CommentWebScanList = new List<SelectListItem>();
            CommentAuditList = new List<SelectListItem>();
            AssessmentDate = DateTime.Now.ToShortDateString();

            Result = new Tuple<int, int>(-1, -1);

            //TODO Change it to rigth version
            VersionID = VersionModel.GetAssessmentVersion(new RestService("versions")).First().Value;

            var types = AssessmentTypeModel.GetCaseTypes(new RestService("assessmenttypes"));
            AssessmetTypeID = types.FirstOrDefault(x => x.TypeName == "Supplier").TypeID;

            CaseTypes = CaseModel.GetCaseTypes(new RestService("casetypes"), "Supplier");
            SupplyChain = GetSupplyChainList();
            CountryList = TerritoryModel.GetTerritories(new RestService("territories"));
            IndexList = IndexModel.GetIndexTypes();
            SectorList = SectorModel.GetSectors(new RestService("sectors"));
            Risks = RiskCategoryModel.GetRiskCategories(new RestService("riskcategories"));
            Questions = QuestionModel.GetQuestionsByAssessmentType(AssessmetTypeID);
            CommentAuditList = GetCommentAuditList();
            CommentWebScanList = GetCommentWebScanList();

            selectedCommentWebScan = -1;
            selectedCommentAudit = -1;
        }

        private IEnumerable<SelectListItem> GetCommentAuditList()
        {
            var list = new List<SelectListItem>();
            foreach (var answer in Questions.FirstOrDefault(q => q.Name == "AuditQuestionSupplier").Answers.OrderBy(a => a.SortOrder))
            {
                var item = new SelectListItem()
                {
                    Text = answer.Text,
                    Value = answer.Value,
                    Selected = false
                };
                list.Add(item);
            }
            return list;
        }

        private IEnumerable<SelectListItem> GetSupplyChainList()
        {
            var list = new List<SelectListItem> {
                 new SelectListItem()
                {
                    Selected = true,
                    Text = Resources.SalesController_GetWebScan_Select,
                    Value = "-1"
                }
            };
            var supplyTypes = GetSupplyChainTypes();
            var result = new List<SupplyChain>();
            foreach (var type in supplyTypes)
            {
                var item = new SelectListItem()
                {
                    Text = type.Name,
                    Value = type.Value.ToString(),
                    Selected = false
                };
                list.Add(item);
            }
            return list;
        }

        private static IList<SupplyChain> GetSupplyChainTypes()
        {
            var service = new RestService("supplytypes");
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<SupplyChain>>(json).OrderBy(x => x.SortOrder);
            return dataContract.ToList();
        }

        private IEnumerable<SelectListItem> GetCommentWebScanList()
        {
            var list = new List<SelectListItem> {
                 new SelectListItem()
                {
                    Selected = true,
                    Text = Resources.SalesController_GetWebScan_Select,
                    Value = "-1"
                }
            };
            foreach (ScanCommentTypes type in Enum.GetValues(typeof(ScanCommentTypes)))
            {
                var item = new SelectListItem()
                {
                    Value = ((int)type).ToString(),
                    Text = type.DisplayName(),
                    Selected = false
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questions"></param>
        private void SetSelectedAnswers()
        {
            var service = new RestService(string.Format("supplierassessmentquestionanser/?Id={0}", AssessmentID));
            var response = service.Get(new List<KeyValuePair<string, string>>());
            var qa = JsonConvert.DeserializeObject<List<QuestionAnswer>>(response);
            if (Questions.Any())
            {
                foreach (var par in qa)
                {
                    Questions.FirstOrDefault(q => q.Id == par.QuestionId.ToString()).Answers.FirstOrDefault(a => a.Id == par.AnswerId.ToString()).Selected = true;
                    Questions.FirstOrDefault(q => q.Id == par.QuestionId.ToString()).SelectedValue = Questions.FirstOrDefault(q => q.Id == par.QuestionId.ToString()).Answers.FirstOrDefault(a => a.Id == par.AnswerId.ToString()).Value;
                    Questions.FirstOrDefault(q => q.Id == par.QuestionId.ToString()).ClassName = "bg-color-riskfactor-" + Questions.FirstOrDefault(q => q.Id == par.QuestionId.ToString()).Answers.FirstOrDefault(a => a.Id == par.AnswerId.ToString()).Value;
                    Questions.FirstOrDefault(q => q.Id == par.QuestionId.ToString()).SelectedAnswerID = Questions.FirstOrDefault(q => q.Id == par.QuestionId.ToString()).Answers.FirstOrDefault(a => a.Id == par.AnswerId.ToString()).Id;
                }
            }
        }

        /// <summary>
        /// Exclude Aditional Information
        /// Auditor, AuditDate and WebScan
        /// </summary>
        /// <returns></returns>
        private bool IsAssessmentComplete()
        {
            if (string.IsNullOrEmpty(Buyer) ||
                string.IsNullOrEmpty(Commodity) ||
                string.IsNullOrEmpty(Supplier) ||
                string.IsNullOrEmpty(Site) ||
                string.IsNullOrEmpty(DUNS)
                //string.IsNullOrEmpty(Auditor) ||
                //string.IsNullOrEmpty(AuditDate) 
                )
                return false;
            //if (selectedCommentWebScan == -1)
            //    return false;
            if (string.IsNullOrEmpty(SelectedCaseID) || SelectedCaseID == "-1")
                return false;
            if (string.IsNullOrEmpty(SelectedCountryID) || SelectedCountryID == "-1")
                return false;
            if (string.IsNullOrEmpty(SelectedSectorID) || SelectedSectorID == "-1")
                return false;
            if (selectedSupplyChain == -1)
                return false;
            if (Questions.Count() - 1 != Questions.Where(q => q.Answers.Any(a => a.Selected)).Count())
                return false;

            return true;
        }
    }
}
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
    public class CustomerAssessmentModel
    {
        protected const string SearchString = @"environment OR labour OR human rights OR resettlement OR security";

        #region Properties
        public string UserId { get; set; }
        public string VersionID { get; set; }
        public string AssessmentID { get; set; }
        public string AssessmentDate { get; set; }
        public string Segment { get; set; }
        public string Department { get; set; }
        public string Responsible { get; set; }
        public string AssessmentCustomer { get; set; }
        public string Project { get; set; }
        //public string Site { get; set; }
        public string AssessmetTypeID { get; set; }
        public string SelectedCountryID { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<IndexModel> IndexList { get; set; }
        public string SelectedSectorID { get; set; }
        public IEnumerable<SelectListItem> SectorList { get; set; }
        public string SelectedCaseID { get; set; }
        public IEnumerable<CaseModel> CaseTypes { get; set; }
        public IEnumerable<CheckBoxListItem> OfferTypes { get; set; }
        public IEnumerable<CheckBoxListItem> TransactionTypes { get; set; }
        public IEnumerable<RiskCategoryModel> Risks { get; set; }
        public decimal RiskIndication { get; set; }
        public IEnumerable<QuestionModel> Questions { get; set; }
        public string WebScan1 { get; set; }
        public int selectedCommentWebScan1 { get; set; }
        public string WebScan2 { get; set; }
        public int selectedCommentWebScan2 { get; set; }
        public IEnumerable<SelectListItem> CommentWebScanList { get; set; }
        public UserMessage Recommendation { get; set; }
        public string Comment { get; set; }
        #endregion

        #region Constructors
        public CustomerAssessmentModel()
        {
            Initialize();
        }

        /// <summary>
        /// Create an instance from a Business Entity
        /// </summary>
        /// <param name="assessment">Business Entity</param>
        public CustomerAssessmentModel(AssessmentCustomer assessment)
        {
            Initialize();
            AssessmentID = assessment.Id.ToString();
            AssessmetTypeID = assessment.AssessmentTypeId.ToString();
            UserId = UserModel.GetUserByID(assessment.AssessorId.ToString()).UserName;
            if (assessment.CustomerId != Guid.Empty)
                AssessmentCustomer = CustomerModel.GetCustommer(new RestService(string.Format("customerbyid/?customerId={0}", assessment.CustomerId.ToString()))).Name;
            VersionID = assessment.VersionId.ToString();
            AssessmentDate = DateTime.Parse(assessment.Date).ToShortDateString();
            Segment = assessment.Segment;
            Department = assessment.ScaniaDepartment;
            Project = assessment.Project;
            //Site = assessment.Site;
            Responsible = assessment.ResponibleSalesPerson;
            Comment = assessment.Comment;

            if (!string.IsNullOrEmpty(Project))
            {
                WebScan2 = Project + " " + SearchString;
            }

            if (!string.IsNullOrEmpty(AssessmentCustomer))
            {
                WebScan1 = AssessmentCustomer + " " + SearchString;
            }

            var service = new RestService(string.Format("webscan/?id={0}", AssessmentID));
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var scans = JsonConvert.DeserializeObject<List<WebScan>>(json);

            List<int> scnasValues = new List<int>();

            if (scans.Any(s => s.WebScanTypeId == Guid.Parse("61342231-14fa-4977-82b0-4b3fc931ad39"))) //Webscan1
            {
                var comment = scans.FirstOrDefault(s => s.WebScanTypeId == Guid.Parse("61342231-14fa-4977-82b0-4b3fc931ad39")).Comment;
                foreach (ScanCommentTypes type in Enum.GetValues(typeof(ScanCommentTypes)))
                {
                    if (type.DisplayName() == comment)
                    {
                        selectedCommentWebScan1 = (int)type;
                        scnasValues.Add(selectedCommentWebScan1);
                    }
                }

            }
            if (scans.Any(s => s.WebScanTypeId == Guid.Parse("89c2db94-9e95-4cb4-9441-e5003aaf8b85"))) //Webscan2
            {
                var comment = scans.FirstOrDefault(s => s.WebScanTypeId == Guid.Parse("89c2db94-9e95-4cb4-9441-e5003aaf8b85")).Comment;
                foreach (ScanCommentTypes type in Enum.GetValues(typeof(ScanCommentTypes)))
                {
                    if (type.DisplayName() == comment)
                    {
                        selectedCommentWebScan2 = (int)type;
                        scnasValues.Add(selectedCommentWebScan2);
                    }
                }
            }

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

            var srvOperation = "offertypes/?assessmentcustomerid={0}";
            var selectedOfferTypes = OfferModel.GetOfferTypes(new RestService(string.Format(srvOperation, assessment.Id.ToString())));
            foreach (var offer in selectedOfferTypes)
            {
                OfferTypes.FirstOrDefault(o => o.ID == offer.OfferID).IsChecked = true;
            }

            if (assessment.Id != Guid.Empty)
            {
                //srvOperation = ""
                //var 
            }

            srvOperation = "transactiontypesbyassessment/?assessmentcustomerid={0}";
            var selectedTransactionTypes = TransactionModel.GetCaseTypes(new RestService(string.Format(srvOperation, assessment.Id.ToString())));
            var qParameters = string.Empty;

            bool hidePeaceIndex = true;

            foreach (var transaction in selectedTransactionTypes)
            {
                TransactionTypes.FirstOrDefault(o => o.ID == transaction.TransactionID).IsChecked = true;
                qParameters += transaction.TransactionID + ",";
                if (transaction.TransactionName.StartsWith("Peace"))
                {
                    hidePeaceIndex = false;
                }
            }

            Questions = QuestionModel.GetQuestionsByAssessmentType(assessment.AssessmentTypeId.ToString(), qParameters.TrimEnd(','));
            SetSelectedAnswers();

            IndexList = IndexModel.GetIndexesByCuontry(assessment.TerritoryId.ToString(), assessment.VersionId.ToString());
            Risks = RiskCategoryModel.GetRiskCategories(new RestService("riskcategories"));

            if (hidePeaceIndex)
                Risks = Risks.ExcludeCategories(new[] { "Peace Index" });
            if (assessment.TerritoryId != Guid.Empty)
            {
                Risks.CalculateIndexesAverage(IndexList);
                if (CaseTypes.Any(c => c.Selected) && CaseTypes.FirstOrDefault(c => c.Selected == true).CaseName.Contains("Existing"))
                {
                    Risks.Except(Risks.Where(r => r.RiskCategoryName.Contains("Peace"))).ToList().ForEach(v => v.RiskCategoryValue += 1);
                }

                if (selectedOfferTypes.Any())
                {
                    var factor = selectedOfferTypes.OfferCalculatedFactor();
                    Risks.Except(Risks.Where(r => r.RiskCategoryName.Contains("Peace"))).ToList().ForEach(v => v.RiskCategoryValue = v.RiskCategoryValue * factor);
                }

                //Calculate risk by answered questions
                var answeredQuestion = Questions.Where(q => q.Answers.Any(a => a.Selected == true));
                if (answeredQuestion.Any())
                {
                    Risks.SpecificQuestionsRisk(answeredQuestion);
                }

                if (scnasValues.Any())
                {
                    var factor = scnasValues.Min();
                    var addRisk = (factor - 5) * 0.15;
                    Risks.Except(Risks.Where(r => r.RiskCategoryName.Contains("Peace"))).ToList().ForEach(v => v.RiskCategoryValue = v.RiskCategoryValue + addRisk);

                }

                RiskIndication = decimal.Parse(Risks.CalculateTotalRisk().ToString());

            }
            if (RiskIndication <= 5)
                Recommendation = new UserMessage(UserMessageType.Warning, "Consult your manager", false);

            if (RiskIndication > 5)
                Recommendation = new UserMessage(UserMessageType.Success, "OK", false);
            Risks.NormalizeRisk();
        }


        #endregion

        #region Public Methods

        public static CustomerAssessmentModel NewCustomerAssessment()
        {
            var model = new CustomerAssessmentModel();
            model.UserId = UserModel.GetUserByID(SessionHandler.LoggedInUser.Id).UserName;
            model.AssessmentDate = DateTime.Now.ToString("d", SessionHandler.SelectedLanguage);
            model.VersionID = VersionModel.GetAssessmentVersion(new RestService("versions")).First().Value;

            model.AssessmetTypeID = AssessmentTypeModel.GetCaseTypes(new RestService("assessmenttypes")).FirstOrDefault(x => x.TypeName == "Customer").TypeID;

            model.CountryList = TerritoryModel.GetTerritories(new RestService("territories"));

            model.IndexList = IndexModel.GetIndexTypes();
            model.SectorList = SectorModel.GetSectors(new RestService("sectors"));
            model.CaseTypes = CaseModel.GetCaseTypes(new RestService("casetypes"), "Customer");
            model.OfferTypes = OfferModel.GetOfferTypesList();
            model.TransactionTypes = TransactionModel.GetCaseTypesList();
            model.Risks = RiskCategoryModel.GetRiskCategories(new RestService("riskcategories"));

            model.Questions = QuestionModel.GetQuestionsByAssessmentType(model.AssessmetTypeID); //Customer Assessment
            model.Recommendation = new UserMessage(UserMessageType.Info, "", false);

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restService"></param>
        /// <returns></returns>
        internal static CustomerAssessmentModel GetAssessment(IRestService restService)
        {
            var json = restService.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<AssessmentCustomer>(json);
            var result = new CustomerAssessmentModel(dataContract);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal GenericResponse Save(decimal? totalrisk)
        {
            var service = new RestService("assessmentcustomer");
            GenericResponse res;
            if (AssessmentID == null)
            {
                //New Object;
                var newAssessment = NewData();
                if (totalrisk.HasValue)
                    newAssessment.RiskIndication = totalrisk.Value;
                var response = service.Post(JsonConvert.SerializeObject(newAssessment));
                res = JsonConvert.DeserializeObject<GenericResponse>(response);
                HandleSaveOfferTypes(res.Id.ToString());
                HandleSaveTransactionTypes(res.Id.ToString());
                HandleSaveWebScans(res.Id.ToString());
                AssessmentID = res.Id.ToString();
            }
            else
            {
                //Update
                var customerAssessment = UpdateDatacontract();
                if (totalrisk.HasValue)
                    customerAssessment.RiskIndication = totalrisk.Value;
                var response = service.Post(JsonConvert.SerializeObject(customerAssessment));
                res = JsonConvert.DeserializeObject<GenericResponse>(response);
                HandleSaveOfferTypes(AssessmentID);
                HandleSaveTransactionTypes(AssessmentID);
                HandleSaveWebScans(AssessmentID);
                res.Id = Guid.Parse(AssessmentID);
            }
            if (Questions.Any())
            {
                var selectedAnswers = Questions.Where(q => q.Answers.Any(a => a.Selected));
                if (selectedAnswers.Any())
                {
                    QuestionModel.SaveCustomerQuestions(res.Id, selectedAnswers);
                }
            }
            return res;
        }


        private void HandleSaveWebScans(string assessmenID)
        {
            var service = new RestService("assessmentwebscan");
            if (selectedCommentWebScan1 > -1)
            {
                var scan = new WebScan
                {
                    AssessmentId = Guid.Parse(assessmenID),
                    WebScanTypeId = Guid.Parse("61342231-14fa-4977-82b0-4b3fc931ad39"),
                    SearchString = AssessmentCustomer,
                    Comment = ((ScanCommentTypes)selectedCommentWebScan1).DisplayName()

                };
                var response = service.Post(JsonConvert.SerializeObject(scan));

            }
            if (selectedCommentWebScan2 > -1)
            {
                service = new RestService("assessmentwebscan");
                var scan = new WebScan
                {
                    AssessmentId = Guid.Parse(assessmenID),
                    WebScanTypeId = Guid.Parse("89c2db94-9e95-4cb4-9441-e5003aaf8b85"),
                    SearchString = Project,
                    Comment = ((ScanCommentTypes)selectedCommentWebScan2).DisplayName()

                };
                var response = service.Post(JsonConvert.SerializeObject(scan));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assessmenID"></param>
        private void HandleSaveTransactionTypes(string assessmenID)
        {
            if (TransactionTypes.Any(tt => tt.IsChecked))
            {
                var selectedTransactions = TransactionTypes.Where(tt => tt.IsChecked).Select(x => x.ID);
                TransactionModel.SaveSelectedTransactionTypes(assessmenID, selectedTransactions);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assessmenID"></param>
        private void HandleSaveOfferTypes(string assessmenID)
        {
            if (OfferTypes.Any(ot => ot.IsChecked))
            {
                var selectedOffers = OfferTypes.Where(ot => ot.IsChecked).Select(x => x.ID);
                OfferModel.SaveSelectedOfferTypes(assessmenID, selectedOffers);
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        private void Initialize()
        {
            CountryList = new List<SelectListItem>();
            IndexList = new List<IndexModel>();
            CommentWebScanList = new List<SelectListItem>();
            SectorList = new List<SelectListItem>();
            CaseTypes = new List<CaseModel>();
            Risks = new List<RiskCategoryModel>();
            Questions = new List<QuestionModel>();
            AssessmetTypeID = AssessmentTypeModel.GetCaseTypeByName("Customer").TypeID;

            CountryList = TerritoryModel.GetTerritories(new RestService("territories"));
            IndexList = IndexModel.GetIndexTypes();
            SectorList = SectorModel.GetSectors(new RestService("sectors"));
            CaseTypes = CaseModel.GetCaseTypes(new RestService("casetypes"), "Customer");
            OfferTypes = OfferModel.GetOfferTypesList();
            TransactionTypes = TransactionModel.GetCaseTypesList();
            Risks = RiskCategoryModel.GetRiskCategories(new RestService("riskcategories"));
            Questions = QuestionModel.GetQuestionsByAssessmentType(AssessmetTypeID);
            CommentWebScanList = GetCommentWebScanList();
            selectedCommentWebScan1 = -1;
            selectedCommentWebScan2 = -1;
            Recommendation = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
        private AssessmentCustomer UpdateDatacontract()
        {
            var restService = new RestService(string.Format("assessmentcustomer/?id={0}", AssessmentID));
            var json = restService.Get(new List<KeyValuePair<string, string>>());
            var refAssessment = JsonConvert.DeserializeObject<AssessmentCustomer>(json);
            refAssessment.ResponibleSalesPerson = Responsible;
            refAssessment.Segment = Segment;
            refAssessment.ScaniaDepartment = Department;
            refAssessment.Project = Project;
            refAssessment.Changed = DateTime.Now.ToShortDateString();
            refAssessment.ChangedBy = Guid.Parse(SessionHandler.LoggedInUser.Id);
            //refAssessment.Site = Site;
            refAssessment.Comment = Comment;
            if (!string.IsNullOrEmpty(SelectedSectorID) && refAssessment.SectorId != Guid.Parse(SelectedSectorID))
                refAssessment.SectorId = Guid.Parse(SelectedSectorID);
            if (!string.IsNullOrEmpty(SelectedCountryID) && refAssessment.TerritoryId != Guid.Parse(SelectedCountryID))
                refAssessment.TerritoryId = Guid.Parse(SelectedCountryID);
            if (!string.IsNullOrEmpty(SelectedCaseID) && refAssessment.CaseTypeId != Guid.Parse(SelectedCaseID))
                refAssessment.CaseTypeId = Guid.Parse(SelectedCaseID);
            refAssessment.CustomerId = GetCustomerID();
            refAssessment.IsComplete = IsAssementComplete();
            return refAssessment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private AssessmentCustomer NewData()
        {
            var assessment = new AssessmentCustomer();
            assessment.CustomerId = GetCustomerID();
            assessment.Name = AssessmentCustomer + " (" + VersionModel.GetAssessmentVersion(new RestService("versions")).First().Text + ")";
            assessment.AssessorId = Guid.Parse(SessionHandler.LoggedInUser.Id);
            assessment.ResponibleSalesPerson = Responsible;
            assessment.AssessmentTypeId = Guid.Parse(AssessmentTypeModel.GetCaseTypeByName("Customer").TypeID);
            assessment.Segment = Segment;
            assessment.ScaniaDepartment = Department;
            assessment.Project = Project;
            //assessment.Site = Site;
            assessment.CreatedBy = Guid.Parse(SessionHandler.LoggedInUser.Id);
            assessment.Date = DateTime.Now.ToString();
            assessment.Changed = DateTime.Now.ToString();
            assessment.ChangedBy = Guid.Parse(SessionHandler.LoggedInUser.Id);
            assessment.VersionId = Guid.Parse(VersionModel.GetAssessmentVersion(new RestService("versions")).First().Value);
            assessment.Comment = Comment;
            if (!string.IsNullOrEmpty(SelectedSectorID) && SelectedSectorID != "-1")
                assessment.SectorId = Guid.Parse(SelectedSectorID);
            if (!string.IsNullOrEmpty(SelectedCountryID) && SelectedCountryID != "-1")
                assessment.TerritoryId = Guid.Parse(SelectedCountryID);
            if (!string.IsNullOrEmpty(SelectedCaseID) && SelectedCaseID != "-1")
                assessment.CaseTypeId = Guid.Parse(SelectedCaseID);
            assessment.IsComplete = IsAssementComplete();

            return assessment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Guid GetCustomerID()
        {
            var ID = Guid.Empty;

            var customer = CustomerModel.GetCustommer(new RestService(string.Format("customerbyname/?Name={0}", AssessmentCustomer)));
            if (customer == null)
            {
                var service = new RestService("customer");
                var coreCustomer = new Customer();
                coreCustomer.Name = AssessmentCustomer;
                var response = service.Post(JsonConvert.SerializeObject(coreCustomer));
                ID = JsonConvert.DeserializeObject<GenericResponse>(response).Id;
            }
            else
            {
                ID = Guid.Parse(customer.Id);
            }
            return ID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questions"></param>
        private void SetSelectedAnswers()
        {
            var service = new RestService(string.Format("customerassessmentquestionanser/?Id={0}", AssessmentID));
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
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsAssementComplete()
        {
            var result = true;

            if (string.IsNullOrEmpty(Segment))
                result = false;
            if (string.IsNullOrEmpty(Department))
                result = false;
            if (string.IsNullOrEmpty(Responsible))
                result = false;
            if (string.IsNullOrEmpty(AssessmentCustomer))
                result = false;
            if (string.IsNullOrEmpty(Project))
                result = false;
            //if (string.IsNullOrEmpty(Site))
            //    result = false;
            if (!OfferTypes.Any(ot => ot.IsChecked))
                result = false;
            if (string.IsNullOrEmpty(SelectedCaseID) || SelectedCaseID == "-1")
                result = false;
            if (string.IsNullOrEmpty(SelectedCountryID) || SelectedCountryID == "-1")
                result = false;
            if (string.IsNullOrEmpty(SelectedSectorID) || SelectedSectorID == "-1")
                result = false;
            if (Questions.Count() != Questions.Where(q => q.Answers.Any(a => a.Selected)).Count())
                result = false;
            if (selectedCommentWebScan1 == -1 || selectedCommentWebScan2 == -1)
                result = false;
            if ((selectedCommentWebScan1 == 10 || selectedCommentWebScan2 == 10) && string.IsNullOrEmpty(Comment))
                result = false;

            return result;
        }
        #endregion
    }
}
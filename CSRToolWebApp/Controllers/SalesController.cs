using CSRToolWebApp.Common;
using CSRToolWebApp.Common.ExtensionMethods;
using CSRToolWebApp.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CSRToolWebApp.Controllers
{
    public class SalesController : Controller
    {
        #region Public Methods      
        public ActionResult Default()
        {
            var newAssessment = CustomerAssessmentModel.NewCustomerAssessment();
            if (SessionHandler.LoggedInUser == null)
            {
                SessionHandler.LoggedInUser = UserModel.GetFirstUser();
            }
            newAssessment.UserId = SessionHandler.LoggedInUser.UserName;
            newAssessment.Risks = newAssessment.Risks.ExcludeCategories(new[] { "Peace Index" });
            return View(newAssessment);
        }

        public ActionResult NewAssessment()
        {
            var newAssessment = CustomerAssessmentModel.NewCustomerAssessment();
            if (SessionHandler.LoggedInUser == null)
            {
                SessionHandler.LoggedInUser = UserModel.GetFirstUser();
            }
            newAssessment.UserId = SessionHandler.LoggedInUser.UserName;
            newAssessment.Risks = newAssessment.Risks.ExcludeCategories(new[] { "Peace Index" });
            return View("Default", newAssessment);
        }

        public ActionResult GetAssessment(string assessmentID)
        {
            var srvOperation = "assessmentcustomer/?id={0}";
            CustomerAssessmentModel assessment = new CustomerAssessmentModel();
            try
            {
                assessment = CustomerAssessmentModel.GetAssessment(new RestService(string.Format(srvOperation, assessmentID)));
                TempData["Questions"] = assessment.Questions;
                TempData["TotalRisk"] = assessment.RiskIndication;
                TempData.Keep();
            }
            catch (Exception)
            {
                throw;
            }
            return View("Default", assessment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submittedObject"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerAssessmentModel model)
        {
            var questions = (IEnumerable<QuestionModel>)TempData["Questions"];
            try
            {
                decimal totalRisk = 0;
                if (TempData.ContainsKey("TotalRisk"))
                    totalRisk = decimal.Parse(TempData["TotalRisk"].ToString());
                if (questions != null)
                {
                    model.Questions = questions;
                }
                var response = model.Save(totalRisk);
                Helper.GlobalUserMessages.Append(new UserMessage(UserMessageType.Success, "Form submitted"));
                return RedirectToAction("GetAssessment", new { assessmentID = response.Id.ToString() });
            }
            catch (Exception e)
            {
                Helper.GlobalUserMessages.Append(new UserMessage(UserMessageType.Error, e.Message));
            }

            //  Show the same view with the values of submitted object --> Keep filled values
            return View("Default", model);
        }

        public ActionResult PrintPDF(string assessmentID)
        {
            var srvOperation = "assessmentcustomer/?id={0}";
            CustomerAssessmentModel assessment = new CustomerAssessmentModel();
            try
            {
                assessment = CustomerAssessmentModel.GetAssessment(new RestService(string.Format(srvOperation, assessmentID)));
            }
            catch (Exception)
            {
                throw;
            }
            return new ViewAsPdf("Default", assessment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedTerritory"></param>
        /// <param name="selectedVersion"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult PopulateIndexes(string selectedTerritory, string selectedVersion) // could use an enum for the selectable values
        {
            string message = string.Empty;
            bool success = true;
            string indexResult = string.Empty;
            try
            {
                IEnumerable<IndexModel> indexes = GetTerritoryIndexes(selectedTerritory, selectedVersion);
                indexResult = this.RenderPartialViewAsHtml("_CountryRiskAssessmentPartial", indexes);
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            var jsonResult = new { success = success, message = message, index = indexResult };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedValues"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CalculateRisk(RiskViewModel selectedValues) // could use an enum for the selectable values
        {
            string message = string.Empty;
            bool success = true;
            string result = string.Empty;
            double totalRisk = 0.0;
            UserMessage recommendation = null;

            TempData["Questions"] = selectedValues.questions;
            TempData.Keep();

            try
            {
                IEnumerable<IndexModel> indexes = GetTerritoryIndexes(selectedValues.selectedTerritory, selectedValues.selectedVersion);
                var risks = RiskCategoryModel.GetRiskCategories(new RestService("riskcategories"));
                risks.CalculateIndexesAverage(indexes);

                if (indexes.Any(r => !string.IsNullOrEmpty(r.Value)) && selectedValues.isExinting)
                {
                    risks.Except(risks.Where(r => r.RiskCategoryName.Contains("Peace"))).ToList().ForEach(v => v.RiskCategoryValue += 1);
                }

                var factor = 1f;
                if (!string.IsNullOrEmpty(selectedValues.selectedOfferTypes))
                {
                    factor = CalculateFactorForOffer(selectedValues.selectedOfferTypes);
                    risks.Except(risks.Where(r => r.RiskCategoryName.Contains("Peace"))).ToList().ForEach(v => v.RiskCategoryValue = v.RiskCategoryValue * factor);
                }
                var answeredQuestion = selectedValues.questions.Where(q => q.Answers.Any(a => a.Selected == true));
                if (answeredQuestion.Count() > 0)
                {
                    risks.SpecificQuestionsRisk(answeredQuestion);
                }

                if (!string.IsNullOrEmpty(selectedValues.selectedWebscanValues))
                {
                    var values = selectedValues.selectedWebscanValues.Split(',');
                    if (values.Select(int.Parse).All(x => x > -1))
                    {
                        var lowestRatting = (values.Select(int.Parse)).Except(new List<int> { -1 }).Min();
                        var addRisk = (lowestRatting - 5) * 0.15;
                        risks.Except(risks.Where(r => r.RiskCategoryName.Contains("Peace"))).ToList().ForEach(v => v.RiskCategoryValue = v.RiskCategoryValue + addRisk);
                    }

                }

                totalRisk = risks.CalculateTotalRisk();


                if (totalRisk <= 5)
                    recommendation = new UserMessage(UserMessageType.Warning, "Consult your manager", false);
                if (totalRisk > 5)
                    recommendation = new UserMessage(UserMessageType.Success, "OK", false);

                TempData["TotalRisk"] = totalRisk;
                TempData.Keep();

                risks.NormalizeRisk();

                if (selectedValues.hidePeaceIndex)
                {
                    risks = risks.ExcludeCategories(new[] { "Peace Index" });
                }

                result = this.RenderPartialViewAsHtml("_RiskResultPartial", risks);
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            var jsonResult = new { success = success, message = message, risk = result, riskMessage = this.RenderPartialViewAsHtml("_UserMessage", recommendation) };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Retrieves all questions for transactions
        /// </summary>
        /// <param name="parameters">Transaction types, coma separeted string, cotains all selected transaction types</param>
        /// <returns>HTML code to render all questions</returns>
        [HttpGet]
        public JsonResult GetQuestionByTransaction(string parameters) // could use an enum for the selectable values
        {
            string questionResult = string.Empty;
            string message = string.Empty;
            bool success = true;
            IEnumerable<QuestionModel> Questions = new List<QuestionModel>();
            try
            {
                Questions = QuestionModel.GetQuestionsByAssessmentType("a93fcbeb-ca1d-4ee6-8109-e1d9a3b5aab8", parameters);
                questionResult = this.RenderPartialViewAsHtml("_QuestionsPartial", Questions);
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            var jsonResult = new { success = success, message = message, questions = Questions, view = questionResult };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetQuestion(string parameters) // could use an enum for the selectable values
        {
            string questionResult = string.Empty;
            string message = string.Empty;
            bool success = true;
            var types = AssessmentTypeModel.GetCaseTypes(new RestService("assessmenttypes"));
            IEnumerable<QuestionModel> Questions = new List<QuestionModel>();
            try
            {
                Questions = QuestionModel.GetQuestionsByAssessmentType(types.FirstOrDefault(x => x.TypeName == "Customer").TypeID, parameters);
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            var jsonResult = new { success = success, message = message, questions = Questions };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedTerritory"></param>
        /// <param name="selectedVersion"></param>
        /// <returns></returns>
        private static IEnumerable<IndexModel> GetTerritoryIndexes(string selectedTerritory, string selectedVersion)
        {
            IEnumerable<IndexModel> indexes;
            if (selectedTerritory != "-1")
                indexes = IndexModel.GetIndexesByCuontry(selectedTerritory, selectedVersion);
            else
                indexes = IndexModel.GetIndexTypes();
            return indexes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedOffers"></param>
        /// <returns></returns>
        private float CalculateFactorForOffer(string selectedOffers)
        {
            float factor = 1f;
            var offers = selectedOffers.Split(',');
            var operation = "offertypes";
            var types = OfferModel.GetOfferTypes(new RestService(operation));
            var selectedOffer = types.Where(t => offers.Contains(t.OfferID));
            if (selectedOffer.Any())
            {
                factor = selectedOffer.OfferCalculatedFactor();
            }
            return factor;
        }
        #endregion
    }
}
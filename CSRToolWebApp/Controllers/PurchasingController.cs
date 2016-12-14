using CSRToolWebApp.Common;
using CSRToolWebApp.Common.ExtensionMethods;
using CSRToolWebApp.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace CSRToolWebApp.Controllers
{
    public class PurchasingController : Controller
    {
        public ActionResult Default()
        {
            var newAssessment = new SupplierAssessmentModel();
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
            var newAssessment = new SupplierAssessmentModel();
            if (SessionHandler.LoggedInUser == null)
            {
                SessionHandler.LoggedInUser = UserModel.GetFirstUser();
            }
            newAssessment.UserId = SessionHandler.LoggedInUser.UserName;
            newAssessment.Risks = newAssessment.Risks.ExcludeCategories(new[] { "Peace Index" });
            return View("Default", newAssessment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SupplierAssessmentModel model)//(string foo)
        {
            var questions = (IEnumerable<QuestionModel>)TempData["Questions"];

            try
            {
                double RiskRaiting = 0, PerformanceRaiting = 0;
                if (TempData.ContainsKey("RiskRaiting"))
                    RiskRaiting = double.Parse(TempData["RiskRaiting"].ToString());
                if (TempData.ContainsKey("PerformanceRaiting"))
                    PerformanceRaiting = double.Parse(TempData["PerformanceRaiting"].ToString());
                if (questions != null)
                {
                    model.Questions = questions;
                }
                var response = model.Save(RiskRaiting, PerformanceRaiting);

                //Helper.GlobalUserMessages.Append(new UserMessage(UserMessageType.Success, "Form submitted"));
                return RedirectToAction("GetAssessment", new { assessmentID = response.Id.ToString() });
            }
            catch (Exception e)
            {
                Helper.GlobalUserMessages.Append(new UserMessage(UserMessageType.Error, e.Message));
            }
            return View("Default", model);
        }

        public ActionResult PrintPDF(string assessmentID)
        {
            var srvOperation = "assessmentsupplier/?id={0}";
            var assessment = new SupplierAssessmentModel();
            try
            {
                assessment = SupplierAssessmentModel.GetAssessment(new RestService(string.Format(srvOperation, assessmentID)));
            }
            catch (Exception)
            {
                throw;
            }
            return new ViewAsPdf("Default", assessment);
        }

        public ActionResult GetAssessment(string assessmentID)
        {
            var srvOperation = "assessmentsupplier/?id={0}";
            var assessment = new SupplierAssessmentModel();
            try
            {
                assessment = SupplierAssessmentModel.GetAssessment(new RestService(string.Format(srvOperation, assessmentID)));
                TempData["Questions"] = assessment.Questions;
                TempData["RiskRaiting"] = assessment.RiskRaiting;
                TempData["PerformanceRaiting"] = assessment.PerformanceRaiting;
                TempData.Keep();
            }
            catch (Exception)
            {
                throw;
            }
            return View("Default", assessment);
        }

        [HttpGet]
        public JsonResult GetIndexes(string selectedTerritory, string selectedVersion) // could use an enum for the selectable values
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
            string riskView = string.Empty;
            double totalRisk = 0.0;
            double performance = 0.0;
            var results = new Tuple<int, int>(-1, -1);

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
                    Debug.WriteLine("Is an existing supplier true add 1 to risk");
                }

                totalRisk = risks.CalculateTotalRisk();

                if (!string.IsNullOrEmpty(selectedValues.selectedSupplyChain) && selectedValues.selectedSupplyChain != "-1")
                {
                    //Do CalculateSupplyRisk
                    var factor = float.Parse(selectedValues.selectedSupplyChain);
                    factor = (factor - 5) * 0.5f;
                    Debug.WriteLine("SupplyChain factor: " + factor);
                    totalRisk += factor;
                    if (totalRisk > 10)
                        totalRisk = 10;
                    if (totalRisk < 0)
                        totalRisk = 0;

                    Debug.WriteLine("Total risk: " + totalRisk);
                }

                if (!string.IsNullOrEmpty(selectedValues.selectedWebscanValues) && selectedValues.selectedWebscanValues != "-1")
                {
                    //Do CalculateSupplyRisk
                    var factor = float.Parse(selectedValues.selectedWebscanValues);
                    factor = factor * 0.2f;
                    Debug.WriteLine("Webscan factor: " + factor);

                    totalRisk += factor;
                    if (totalRisk > 10)
                        totalRisk = 10;
                    if (totalRisk < 0)
                        totalRisk = 0;
                    Debug.WriteLine("Total risk: " + totalRisk);
                }

                if (selectedValues.questions.Any(q => q.Answers.Any(a => a.Selected)))
                {
                    var values = new List<double>();
                    foreach (QuestionModel q in selectedValues.questions.Where(q => q.Answers.Any(a => a.Selected)))
                    {
                        if (q.Name != "AuditQuestionSupplier")
                            values.Add(double.Parse(q.Answers.FirstOrDefault(a => a.Selected).Value));
                    }

                    if (values.Count > 0)
                        performance = values.Min();
                    else
                        performance = 0;

                    Debug.WriteLine("Total performance: " + performance);
                    if (performance < 0)
                        performance = 0.0;
                }

                if (!string.IsNullOrEmpty(selectedValues.selectedAuditValue) && selectedValues.selectedAuditValue != "-1")
                {
                    var value = int.Parse(selectedValues.selectedAuditValue);
                    var factor = (value - 5) * 0.75;
                    performance = performance + factor;
                    Debug.WriteLine(string.Format("Audit value, Total Performance: {0} ", performance));
                }

                results = new Tuple<int, int>(totalRisk.Performance_Y(), performance.Performance_X());
                TempData["RiskRaiting"] = totalRisk;
                TempData["PerformanceRaiting"] = performance;
                TempData.Keep();

                risks.NormalizeRisk();

                risks = risks.ExcludeCategories(new[] { "Peace Index" });

                riskView = this.RenderPartialViewAsHtml("_RiskResultPartial", risks);
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            var jsonResult = new { success = success, message = message, risk = riskView, result = results };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetQuestion() // could use an enum for the selectable values
        {
            string questionResult = string.Empty;
            string message = string.Empty;
            bool success = true;

            var types = AssessmentTypeModel.GetCaseTypes(new RestService("assessmenttypes"));

            IEnumerable<QuestionModel> Questions = new List<QuestionModel>();
            try
            {
                Questions = QuestionModel.GetQuestionsByAssessmentType(types.FirstOrDefault(x => x.TypeName == "Supplier").TypeID);
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            var jsonResult = new { success = success, message = message, questions = Questions };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

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


    }
}
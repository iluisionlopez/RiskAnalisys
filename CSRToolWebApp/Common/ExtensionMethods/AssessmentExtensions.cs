using CSRToolWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Common.ExtensionMethods
{
    /// <summary>
    /// Extension method for risk categories, used to calculated the 
    /// </summary>
    public static class AssessmentsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offers"></param>
        /// <returns></returns>
        public static float OfferCalculatedFactor(this IEnumerable<OfferModel> offers)
        {
            float factor = 1f;
            if (offers.Any())
            {
                if (offers.Any(x => x.OfferName.StartsWith("Product")) && offers.Count() == 1)
                    factor = 1.1f;
                else
                {
                    if (offers.Any(x => x.OfferName.StartsWith("Service")) || offers.Any(x => x.OfferName.StartsWith("Financing")))
                        factor = 0.9f;
                    if (offers.Any(x => x.OfferName.StartsWith("Service")) && offers.Any(x => x.OfferName.StartsWith("Financing")))
                        factor = 0.8f;
                }
            }
            return factor;
        }

        /// <summary>
        /// Calculate the risk by territory index 
        /// </summary>
        /// <param name="RiskCategories">All risk categoris</param>
        /// <param name="Indexes">Indexes by territory</param>
        /// <param name="isNewCustomer">Bool parameter for new customer</param>
        /// <returns>Calculated risk values for categories</returns>
        public static IEnumerable<RiskCategoryModel> CalculateRisksByTerritoryIndexes(this IEnumerable<RiskCategoryModel> RiskCategories, IEnumerable<IndexModel> Indexes, IEnumerable<QuestionModel> questions, float factor, bool isNotNewCustomer)
        {
            RiskCategories.CalculateIndexesAverage(Indexes);
            if (isNotNewCustomer)
            {
                RiskCategories.Except(RiskCategories.Where(r => r.RiskCategoryName.Contains("Peace"))).ToList().ForEach(v => v.RiskCategoryValue += 1);
            }

            RiskCategories.Except(RiskCategories.Where(r => r.RiskCategoryName.Contains("Peace"))).ToList().ForEach(v => v.RiskCategoryValue = v.RiskCategoryValue * factor);

            RiskCategories.NormalizeRisk();

            return RiskCategories;
        }

        /// <summary>
        /// Calculate the average of all categories risk, 
        /// Set the color code for the progress bar presentation
        /// </summary>
        /// <param name="RiskCategories">Risk categories</param>
        /// <param name="Indexes">Territory Indexes</param>
        /// <returns>Risk categories collection (All values are from 0 To 10)</returns>
        public static IEnumerable<RiskCategoryModel> CalculateIndexesAverage(this IEnumerable<RiskCategoryModel> RiskCategories, IEnumerable<IndexModel> Indexes)
        {
            if (Indexes.Any(r => !string.IsNullOrEmpty(r.Value)))
            {
                foreach (var category in RiskCategories)
                {
                    if (Indexes.Any(y => y.Riskcategories.Any(z => z.RiskCategoryID == category.RiskCategoryID)))
                    {
                        var average = Indexes.Where(x => x.Riskcategories.Any(r => r.RiskCategoryID == category.RiskCategoryID)).Average(ind => int.Parse(ind.Value));
                        RiskCategories.FirstOrDefault(cat => cat.RiskCategoryID == category.RiskCategoryID).RiskCategoryValue = average;
                        Debug.WriteLine(category.RiskCategoryName + " :" + average.ToString());
                    }
                }
            }
            return RiskCategories;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RiskCategories"></param>
        /// <param name="Questions"></param>
        /// <returns></returns>
        public static IEnumerable<RiskCategoryModel> SpecificQuestionsRisk(this IEnumerable<RiskCategoryModel> RiskCategories, IEnumerable<QuestionModel> Questions)
        {
            foreach (var category in RiskCategories)
            {
                var takeValues = Questions.Where(r => r.Riskcategories.Any(c => c.RiskCategoryID == category.RiskCategoryID));
                double ave = 0;
                if (takeValues.Count() > 0)
                {
                    ave = 0;
                    foreach (var answer in takeValues.Select(a => a.Answers))
                    {
                        ave += double.Parse(answer.FirstOrDefault(v => v.Selected).Value);
                    }
                    ave = ave / takeValues.Count();
                    ave -= 5;
                    ave *= 0.85;

                }
                var newRiskValue = category.RiskCategoryValue + ave;
                if (newRiskValue < 0)
                    newRiskValue = 0;
                if (newRiskValue > 10)
                    newRiskValue = 10;
                RiskCategories.FirstOrDefault(cat => cat.RiskCategoryID == category.RiskCategoryID).RiskCategoryValue = newRiskValue;
            }
            return RiskCategories;
        }

        /// <summary>
        /// Calculate the total risk as average of the all risk categories
        /// If any of the risk categories is below 2,5 – that will be total risk
        /// </summary>
        /// <param name="RiskCategories">Risk categories</param>
        /// <returns>Risk categories collection (All values are from 0 To 10)</returns>
        public static double CalculateTotalRisk(this IEnumerable<RiskCategoryModel> RiskCategories)
        {
            RiskCategories = RiskCategories.ExcludeCategories(new[] { "Peace Index" });
            double totalRisk = RiskCategories.Average(x => x.RiskCategoryValue);
            if (RiskCategories.Any(r => r.RiskCategoryValue < 2.5))
            {
                totalRisk = RiskCategories.OrderBy(o => o.RiskCategoryValue).FirstOrDefault().RiskCategoryValue;
            }

            Debug.WriteLine("Total risk: " + totalRisk);
            return totalRisk;
        }

        /// <summary>
        /// Normilaze the risk values between 0 and 100
        /// Risk should be normalized for presentation purposes
        /// </summary>
        /// <param name="RiskCategories">Risk categories</param>
        /// <returns>Risk categories collection (All values are from 0 To 100)</returns>
        public static IEnumerable<RiskCategoryModel> NormalizeRisk(this IEnumerable<RiskCategoryModel> RiskCategories)
        {
            foreach (var category in RiskCategories)
            {
                if (category.RiskCategoryValue <= 0)
                    RiskCategories.FirstOrDefault(cat => cat.RiskCategoryID == category.RiskCategoryID).ProgressAttribute = "";
                else if (category.RiskCategoryValue <= 2.5)
                    RiskCategories.FirstOrDefault(cat => cat.RiskCategoryID == category.RiskCategoryID).ProgressAttribute = "progress-bar-danger";
                else if (category.RiskCategoryValue > 2.5 && category.RiskCategoryValue <= 5.5)
                    RiskCategories.FirstOrDefault(cat => cat.RiskCategoryID == category.RiskCategoryID).ProgressAttribute = "progress-bar-warning";
                else if (category.RiskCategoryValue > 5.5 && category.RiskCategoryValue <= 7.5)
                    RiskCategories.FirstOrDefault(cat => cat.RiskCategoryID == category.RiskCategoryID).ProgressAttribute = "progress-bar-info";
                else if (category.RiskCategoryValue > 7.5)
                    RiskCategories.FirstOrDefault(cat => cat.RiskCategoryID == category.RiskCategoryID).ProgressAttribute = "progress-bar-success";
                category.RiskCategoryValue = Math.Round(category.RiskCategoryValue * 10, 0);
            }
            return RiskCategories;
        }

        public static int Performance_Y(this double risk)
        {
            var _performance = -1;           
            if ( risk <= 5)
                _performance = 1;

            if (risk > 5 && risk <= 7.5)
                _performance = 2;

            if (risk > 7.55)
                _performance = 3;

            Debug.WriteLine("Risk matrix position: " + _performance);
            return _performance;
        }

        public static int Performance_X(this double risk)
        {
            var _performance = -1;
            if (risk <= 5)
                _performance = 3;

            if (risk > 5 && risk <= 7.5)
                _performance = 2;

            if (risk > 7.55)
                _performance = 1;

            Debug.WriteLine("Performance matrix position: " + _performance);
            return _performance;
        }

        public static IEnumerable<RiskCategoryModel> ExcludeCategories(this IEnumerable<RiskCategoryModel> RiskCategories, string[] excludeCategories)
        {
            if (excludeCategories != null && excludeCategories.Length > 0)
            {
                var result = RiskCategories.Where(r => (!excludeCategories.Contains(r.RiskCategoryName)));
                RiskCategories = result;
            }
            return RiskCategories;
        }
    }
}
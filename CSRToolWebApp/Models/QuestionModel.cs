using CSRToolWebApp.Common;
using CSRToolWebApp.Common.ExtensionMethods;
using CSRToolWebApp.DataContracts.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSRToolWebApp.Models
{
    /// <summary>
    /// ViewModel that represent a instance of the Question.
    /// Should be use as data representation, 
    /// and as data access layer to encapsulated business entity Question.
    /// </summary>
    public class QuestionModel
    {
        #region Properties
        public string Id { get; set; }
        public string TransactionTypeId { get; set; }
        public string AssessmentTypeId { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public string Text { get; set; }
        public string  Comment { get; set; }
        public IEnumerable<AnswerModel> Answers { get; set; }
        public string SelectedValue { get; set; }
        public string ClassName { get; set; }
        public string SelectedAnswerID { get; set; }
        public IEnumerable<RiskCategoryModel> Riskcategories { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public QuestionModel()
        {
            Answers = new List<AnswerModel>();
            Riskcategories = new List<RiskCategoryModel>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AssessmentTypeID"></param>
        /// <returns></returns>
        public static IEnumerable<QuestionModel> GetQuestionsByAssessmentType(string AssessmentTypeID, string parameters = "")
        {
            var result = new List<QuestionModel>();
            var transactions = string.IsNullOrEmpty(parameters)? null : parameters.Split(',');                     

            var str = string.Format("questionsbyassessmenttype/?assessmenttype={0}", AssessmentTypeID);
            var service = new RestService(str);
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<AssessmentTypeQuestion>>(json);

            str = string.Format("questions");
            service = new RestService(str);
            json = service.Get(new List<KeyValuePair<string, string>>());
            var allQuestions = JsonConvert.DeserializeObject<List<Question>>(json);

            if (!transactions.IsNullOrEmpty())
            {
                dataContract = (from q in dataContract
                               join t in transactions on q.TransactionTypeId.ToString() equals t.ToLower()
                               select q).ToList();
            }            

            var questions = (from qat in dataContract
                            join q in allQuestions on qat.QuestionId equals q.Id                            
                            select new { Id = q.Id, Name = q.Name, Text = q.QuestionText, Comment = q.Comment, TransactionTypID = qat.TransactionTypeId,
                                AssessmentTypeId =qat.AssessmentTypeId, SortOrder = q.SortOrder }).GroupBy(p => p.Id).Select(g => g.First()).OrderBy(o => o.SortOrder);

            var strService = "riskcategoriesbyquestion/?question={0}";

            foreach (var item in questions)
            {
                var question = new QuestionModel();
                question.Id = item.Id.ToString();
                question.Name = item.Name;
                question.Text = item.Text;
                question.Comment = item.Comment;
                question.SortOrder = item.SortOrder;
                question.SelectedAnswerID = "-1";
                question.TransactionTypeId = item.TransactionTypID.ToString();
                question.AssessmentTypeId = item.AssessmentTypeId.ToString();
                question.Answers = AnswerModel.GetAnswersByQuestion(question.Id);
                if (question.Answers.Any(a=> !string.IsNullOrEmpty(a.Comment))) {
                    question.Comment += "   Options:";
                    foreach (var answer in question.Answers)
                    {
                        if (!string.IsNullOrEmpty(answer.Comment))
                            question.Comment += "  (" + answer.Text + ")" + answer.Comment;
                    } }
                question.Riskcategories = RiskCategoryModel.GetRiskCategories(new RestService(string.Format(strService,question.Id)));
                result.Add(question);
            }
            return result;
        }

        internal static void SavePurchasingQuestions(Guid assessmentId, IEnumerable<QuestionModel> questions)
        {
            try
            {
                var list = new List<AssessmentSupplierQuestionAnswer>();
                var strService = "questionanser/?answerId={0}&questionID={1}";
                foreach (var question in questions)
                {
                    var service = new RestService(string.Format(strService, question.Answers.FirstOrDefault(a => a.Selected).Id, question.Id));
                    var json = service.Get(new List<KeyValuePair<string, string>>());
                    var qa = JsonConvert.DeserializeObject<QuestionAnswer>(json);
                    var questionAnswer = new AssessmentSupplierQuestionAnswer()
                    {
                        Id = Guid.NewGuid(),
                        AssessmentSupplierId = assessmentId,
                        QuestionAnswerId = qa.Id
                    };
                    list.Add(questionAnswer);
                }
                var newservice = new RestService("supplierquestionanswer");
                var result = newservice.Post(JsonConvert.SerializeObject(list));
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void SaveCustomerQuestions(Guid assessmentId, IEnumerable<QuestionModel> questions)
        {
            try
            {
                var list = new List<AssessmentCustomerQuestionAnswer>();
                var strService = "questionanser/?answerId={0}&questionID={1}";
                foreach (var question in questions)
                {
                    var service = new RestService(string.Format(strService, question.Answers.FirstOrDefault(a => a.Selected).Id, question.Id));
                    var json = service.Get(new List<KeyValuePair<string, string>>());
                    var qa = JsonConvert.DeserializeObject<QuestionAnswer>(json);
                    var questionAnswer = new AssessmentCustomerQuestionAnswer()
                    {
                        Id = Guid.NewGuid(),
                        AssessmentCustomerId = assessmentId,
                        QuestionAnswerId = qa.Id
                    };
                    list.Add(questionAnswer);
                }
                var newservice = new RestService("customerquestionanswer");
                var result = newservice.Post(JsonConvert.SerializeObject(list));
            }
            catch (Exception)
            {
                throw;
            }
           
        }
        #endregion
    }
}
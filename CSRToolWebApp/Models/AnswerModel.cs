using CSRToolWebApp.Common;
using CSRToolWebApp.DataContracts.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Models
{
    /// <summary>
    /// ViewModel that represent a instance of the Answer.
    /// Should be use as data representation, 
    /// and as data access layer to encapsulated business entity Answer.
    /// </summary>
    public class AnswerModel
    {
        #region Service

        #endregion

        #region Properties
        public string Id { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }
        public bool Selected { get; set; }
        public string Comment { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public AnswerModel()
        {
            
        }

        /// <summary>
        /// Overload, using a datacontract to create a instanceof AnswerModel
        /// </summary>
        /// <param name="answer">Data contract <seealso cref="Answer"/></param>
        public AnswerModel(Answer answer)
        {
            Id = answer.Id.ToString();
            Value = answer.Value.ToString();
            Name = answer.Name;
            Text = answer.AnswerText;
            Comment = answer.Comment;
            Selected = false;
            SortOrder = answer.SortOrder;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Retrives all question for a specificquestion
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public static IEnumerable<AnswerModel> GetAnswersByQuestion(string questionID)
        {
            var result = new List<AnswerModel>();
            var str = string.Format("answersbyquestion/?question={0}", questionID);
            var service = new RestService(str);
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<Answer>>(json);
            foreach(Answer answer in dataContract.OrderBy(a=>a.SortOrder))
            {
                var ans = new AnswerModel(answer);
                result.Add(ans);
            }
            var defaultAnswer = new AnswerModel();
            defaultAnswer.Id = "-1";
            defaultAnswer.Value = "-1";
            defaultAnswer.Name = "defaultAnswer";
            defaultAnswer.Text = "Select an answer";            

            result.Insert(0, defaultAnswer);
            return result;
        }
        #endregion

    }
}
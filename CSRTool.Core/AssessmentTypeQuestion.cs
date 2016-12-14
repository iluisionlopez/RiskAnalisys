using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Core
{
    public class AssessmentTypeQuestion : ObjectInfo
    {
        public Guid AssessmentTypeId { get; set; }

        public Guid? TransactionTypeId { get; set; }

        public Guid QuestionId { get; set; }

        public  Question Question { get; set; }
        public  TransactionType TransactionType { get; set; }
        public  AssessmentType AssessmentType { get; set; }

        public List<QuestionAnswer> QuestionAnswers { get; set; }

        public List<RiskCategory> RiskCategories { get; set; }
    } 

    }




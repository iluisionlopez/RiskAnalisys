using System;

namespace CSRTool.Core
{
    public class AssessmentSupplierQuestionAnswer
    {
        public Guid Id { get; set; }
        public Guid AssessmentSupplierId { get; set; }
        public Guid QuestionAnswerId { get; set; }
        public string Comment { get; set; }
    }
}
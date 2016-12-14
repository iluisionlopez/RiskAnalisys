using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Core
{
    public class AssessmentTransactionType
    {
        public Guid Id { get; set; }
        public Guid AssessmentCustomerId { get; set; }
        public Guid TransactionTypeId { get; set; }
    }
}

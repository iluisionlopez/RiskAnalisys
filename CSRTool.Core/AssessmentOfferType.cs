using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Core
{
    public class AssessmentOfferType
    {
        public Guid Id { get; set; }
        public Guid AssessmentCustomerId { get; set; }
        public Guid OfferTypeId { get; set; }
    }
}

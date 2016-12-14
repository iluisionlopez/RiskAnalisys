using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IOfferTypeService
    {
        List<OfferType> GetOfferTypes();
        List<OfferType> GetOfferTypesByAssessmentCustomerId(Guid assessmentCustomerId);
        List<AssessmentOfferType> GetAssessmentOfferTypes(Guid assessmentCustomerId);
        CSRToolNotifier SaveAssessmentOfferType(AssessmentOfferType offerType);
        CSRToolNotifier SaveAssessmentOfferTypes(IEnumerable<AssessmentOfferType> offers);
        bool DeleteAssessmentOfferTypes(Guid assessmnetID);
    }
}
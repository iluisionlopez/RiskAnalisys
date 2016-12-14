using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IOfferTypeRepository
    {
        List<OfferType> GetOfferTypes();
        List<OfferType> GetOfferTypesByAssessmentCustomerId(Guid assessmentCustomerId);
        List<AssessmentOfferType> GetAssessmentOfferTypes(Guid assessmentCustomerId);
        CSRToolNotifier SaveAssessmentOfferType(AssessmentOfferType offerType);
        CSRToolNotifier SaveAssessmentOfferTypes(IEnumerable<AssessmentOfferType> offertypes);
        bool DeleteAssessmentOfferTypes(Guid assessmnetID);
    }
}
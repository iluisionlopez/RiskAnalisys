using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using Version = CSRTool.Core.Version;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class OfferTypeService : IOfferTypeService
    {
        private readonly IOfferTypeRepository _offerTypeRepository;

        public OfferTypeService(IOfferTypeRepository offerTypeRepository)
        {
            _offerTypeRepository = offerTypeRepository;
        }

        public List<OfferType> GetOfferTypes()
        {
            return _offerTypeRepository.GetOfferTypes();
        }

        public List<OfferType> GetOfferTypesByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            return _offerTypeRepository.GetOfferTypesByAssessmentCustomerId(assessmentCustomerId);
        }

        public List<AssessmentOfferType> GetAssessmentOfferTypes(Guid assessmentCustomerId)
        {
            return _offerTypeRepository.GetAssessmentOfferTypes(assessmentCustomerId);
        }

        public CSRToolNotifier SaveAssessmentOfferType(AssessmentOfferType offerType)
        {
            return _offerTypeRepository.SaveAssessmentOfferType(offerType);
        }

        public CSRToolNotifier SaveAssessmentOfferTypes(IEnumerable<AssessmentOfferType> offers)
        {
            return _offerTypeRepository.SaveAssessmentOfferTypes(offers);
        }

        public bool DeleteAssessmentOfferTypes(Guid assessmnetID)
        {
            return _offerTypeRepository.DeleteAssessmentOfferTypes(assessmnetID);
        }
    }

}
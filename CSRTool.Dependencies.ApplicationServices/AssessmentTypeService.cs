using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class AssessmentTypeService : IAssessmentTypeService
    {
        private readonly IAssessmentTypeRepository _assessmentTypeRepository;

        public AssessmentTypeService(IAssessmentTypeRepository assessmentTypeRepository)
        {
            _assessmentTypeRepository = assessmentTypeRepository;
        }

        public List<AssessmentType> GetAssessmentTypes()
        {
            return _assessmentTypeRepository.GetAssessmentTypes();
        }

 }

}
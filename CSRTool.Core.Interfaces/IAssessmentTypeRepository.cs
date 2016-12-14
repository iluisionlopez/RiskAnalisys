using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IAssessmentTypeRepository
    {
        List<AssessmentType> GetAssessmentTypes();

        bool SaveAssessmentType(AssessmentType assessmentType);

    }
}

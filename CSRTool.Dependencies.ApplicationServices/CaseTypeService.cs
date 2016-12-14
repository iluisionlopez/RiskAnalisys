using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class CaseTypeService : ICaseTypeService
    {
        private readonly ICaseTypeRepository _caseTypeRepository;

        public CaseTypeService(ICaseTypeRepository caseTypeRepository)
        {
            _caseTypeRepository = caseTypeRepository;
        }

        public List<CaseType> GetCaseTypes()
        {
            return _caseTypeRepository.GetCaseTypes();
        }


        
    }

}
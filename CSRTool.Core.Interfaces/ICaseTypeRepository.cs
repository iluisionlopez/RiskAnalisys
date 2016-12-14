using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface ICaseTypeRepository
    {
        List<CaseType> GetCaseTypes();
    }
}
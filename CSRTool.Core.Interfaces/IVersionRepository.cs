using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IVersionRepository
    {
        List<Version> GetVersions();
    }
}
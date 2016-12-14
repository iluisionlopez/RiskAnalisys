using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IVersionService
    {
        List<Version> GetVersions();
    }
}
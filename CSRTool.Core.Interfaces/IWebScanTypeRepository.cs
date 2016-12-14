using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IWebScanTypeRepository
    {
        List<WebScanType> GetWebScanTypes();
    }
}
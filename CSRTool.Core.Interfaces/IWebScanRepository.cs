using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IWebScanRepository
    {
        List<WebScan> GetWebScans();
        List<Core.WebScanType> GetWebScanTypes();
        List<WebScan> GetWebScansByAssessmentCustomerId(Guid assessmentCustomerId);
        bool SaveCustomerWebScan(WebScan scan);
        bool SaveSupplierWebScan(Core.WebScan scan);
        List<WebScan> GetWebScansByAssessmentSupplierId(Guid assessmentId);
    }
}
using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IWebScanService
    {
        List<WebScan> GetWebScans();
        List<Core.WebScanType> GetWebScanTypes();
        List<WebScan> GetWebScansByAssessmentCustomerId(Guid assessmentCustomerId);
        bool SaveAssessmentCustomerWebScan(WebScan scan);
        bool SaveSupplierWebScan(Core.WebScan scan);
        List<WebScan> GetWebScansByAssessmentSupplierId(Guid assessmentId);
    }
}
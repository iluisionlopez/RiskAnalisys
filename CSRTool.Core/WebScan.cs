using System;

namespace CSRTool.Core
{
    public class WebScan : ObjectInfo
    {
        public Guid AssessmentId { get; set; }
        public Guid WebScanTypeId { get; set; }
        public string Name { get; set; }
        public string SearchString { get; set; }
        public string Comment { get; set; }
    }
}
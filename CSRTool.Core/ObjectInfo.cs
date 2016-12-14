using System;

namespace CSRTool.Core
{
    /// <summary>Base class, holding ID and tracing information.
    /// 
    /// </summary>
    public abstract class ObjectInfo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }
        public DateTime Changed { get; set; }
        public Guid ChangedBy { get; set; }
        public User ChangedByUser { get; set; }
        public bool IsActive { get; set; }
    }

}


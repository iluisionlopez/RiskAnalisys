using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{
    public class CSRToolFault
    {
        /// <summary>
        /// Exception Message
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Stack trace on unhandled exceptions
        /// </summary>
        [DataMember]
        public string StackTrace { get; set; }

        /// <summary>
        /// If critical, user should redirect to error page 
        /// and exception details should log.
        /// </summary>
        [DataMember]
        public bool IsCritical { get; set; }
    }
}
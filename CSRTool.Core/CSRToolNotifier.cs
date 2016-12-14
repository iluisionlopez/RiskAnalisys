namespace CSRTool.Core
{
    public enum NotificationType
    {
        None,
        Success,
        Info,
        Warning,
        Error
    }
    public sealed class CSRToolNotifier
    {
        public string Message { get; set; }
        public string Description { get; set; }
        public NotificationType NotificationType { get; set; }

        public CSRToolNotifier()
        {
            Message = "";
            Description = "";
            NotificationType = NotificationType.None;
        }
    }
}

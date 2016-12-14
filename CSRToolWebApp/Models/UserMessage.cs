using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Models
{
    public enum UserMessageType
    {
        Info,
        Warning,
        Error,
        Success
    }

    public class UserMessage
    {
        public UserMessage(UserMessageType type, string message) : this(type, string.Empty, message, true) { }
        public UserMessage(UserMessageType type, string message, bool showCloseButton) : this(type, string.Empty, message, showCloseButton) { }
        public UserMessage(UserMessageType type, string title, string message) : this(type, title, message, true) { }
        public UserMessage(UserMessageType type, string title, string message, bool showCloseButton)
        {
            this.Type = type;
            this.Title = title;
            this.Message = message;
            this.ShowCloseButton = showCloseButton;
        }

        public UserMessageType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool ShowCloseButton { get; set; }
    }
}
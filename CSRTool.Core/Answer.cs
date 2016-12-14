using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Core
{
    public class Answer : ObjectInfo
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string AnswerText { get; set; }
        public string Comment { get; set; }
        public int SortOrder { get; set; }
    }
}

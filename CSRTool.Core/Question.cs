using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Core
{
    public class Question : ObjectInfo
    {
        public string Name { get; set; }
        public string QuestionText { get; set; }        
        public string Comment { get; set; }
        public int SortOrder { get; set; }
    }
}

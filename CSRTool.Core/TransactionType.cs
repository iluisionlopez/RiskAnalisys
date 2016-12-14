using System;
using CSRTool.Core;

namespace CSRTool.Core
{
    public class TransactionType : ObjectInfo
    {
        public string Name { get; set; }
        public string Comment { get; set; }

        public TransactionType Create(string name, string comment)
        {
            var ret = new TransactionType
            {
                Id = Guid.NewGuid(),
                Name = name,
                Comment = comment
            };


            return ret;
        }

    }
}

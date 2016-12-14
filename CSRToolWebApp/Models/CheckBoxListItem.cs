using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Models
{
    public class CheckBoxListItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool IsChecked { get; set; }
    }
}
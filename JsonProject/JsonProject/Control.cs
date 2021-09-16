using System;
using System.Collections.Generic;
using System.Text;

namespace JsonProject
{
    public class Control
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public string DisplayName { get; set; }
        public bool IsHidden { get; set; }
        public string UIType { get; set; }
        public string Value { get; set; }
        public List<string> ValueList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model.persistence {
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class FileTaskAttribute : Attribute {
        public FileTaskAttribute() {
        }
        
        public string TaskReference { get; set; }

        public string Text { get; set; }

        public int Points { get; set; }
    }
}

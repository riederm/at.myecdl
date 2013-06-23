using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model {
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class TaskAttribute: Attribute {

        // This is a positional argument
        public TaskAttribute(string id, string text) {
            this.Id = id;
            this.Text = text;
        }

        public TaskAttribute() {
        }

        public string Id { get; set; }
        public string Text { get; set; }



    }
}

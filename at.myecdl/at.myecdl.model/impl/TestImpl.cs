using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model.impl
{
    public class TestImpl : ITest
    {

        public TestImpl() {
            Tasks = new List<ITask>();
        }

        public List<ITask> Tasks
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }


        public string Description {
            get;
            set;
        }
    }
}

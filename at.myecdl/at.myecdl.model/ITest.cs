using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model
{
    public interface ITest
    {
        List<at.myecdl.model.ITask> Tasks
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }

        string Description {
            get;
            set;
        }

    }

    public interface ITestFactory {
        ITest CreateTest();
    }
}

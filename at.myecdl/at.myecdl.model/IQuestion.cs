using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model
{
    public interface IQuestion : ITask
    {
        string Question
        {
            get;
            set;
        }

        List<at.myecdl.model.IAnswer> Answers
        {
            get;
            set;
        }

        bool IsAnswered {
            get;
            set;
        }
    }
}

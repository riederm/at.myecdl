using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model
{
    public interface IAnswer
    {
        bool IsCorrect
        {
            get;
            set;
        }

        bool Checked
        {
            get;
            set;
        }

        string Answer {
            get;
            set;
        }
    }
}

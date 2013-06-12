using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model.impl
{
    class AnswerImpl : IAnswer
    {
        public bool IsCorrect
        {
            get;
            set;
        }

        public bool Checked
        {
            get;
            set;
        }


        public string Answer {
            get;
            set;
        }
    }
}

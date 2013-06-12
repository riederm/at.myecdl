using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model.impl
{
    class QuestionImpl : IQuestion
    {
        public QuestionImpl() {
            Answers = new List<IAnswer>();
            Question = String.Empty;
            Description = String.Empty;
            IsAnswered = false;
        }

        public string Question
        {
            get;
            set;
        }

        public List<IAnswer> Answers
        {
            get;
            set;
        }

        public bool IsAnswered {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
    }
}

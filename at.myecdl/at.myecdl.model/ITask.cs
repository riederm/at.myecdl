using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model
{
    public interface ITask
    {
        void Setup();

        string Description
        {
            get;
            set;
        }

        IEvaluationResult Evaluate();
    }
}

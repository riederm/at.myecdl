﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model {
    public interface ICondition {
        IEvaluationResult Evaluate();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model {
    public interface IEvaluator {
        at.myecdl.model.IEvaluationResult Evaluate();

        void Setup();
    }
}

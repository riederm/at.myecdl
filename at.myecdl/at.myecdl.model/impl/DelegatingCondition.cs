using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model.impl {
    public class DelegatingCondition : ICondition {
        private Func<IEvaluationResult> functionDelegate;

        public DelegatingCondition(Func<IEvaluationResult> functionDelegate) {
            this.functionDelegate = functionDelegate;
        }

        public IEvaluationResult Evaluate() {
            return functionDelegate();
        }
    }
}

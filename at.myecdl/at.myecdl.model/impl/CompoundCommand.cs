using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model.impl {
    public class CompoundCommand : ICondition {

        IEnumerable<ICondition> conditions;

        public CompoundCommand(params ICondition[] conditions) {
            this.conditions = conditions;
        }

        public IEvaluationResult Evaluate() {
            IEvaluationResult[] results = new IEvaluationResult[conditions.Count()];
            int i = 0;
            foreach (var item in conditions) {
                results[i] = item.Evaluate();
                i++;
            }
            return new CompoundEvaluationResult(results);
        }
    }
}

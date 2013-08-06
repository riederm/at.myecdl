using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model.impl {
    public class CompoundEvaluationResult : IEvaluationResult {
        private IEnumerable<IEvaluationResult> results;

        public CompoundEvaluationResult(params IEvaluationResult[] results) {
            this.results = results;
        }

        public bool IsOk {
            get {
                return results.All<IEvaluationResult>(r => r.IsOk);
            }
        }

        public string Message {
            get {
                StringBuilder builder = new StringBuilder();
                foreach (var item in results) {
                    if (builder.Length > 0) {
                        builder.Append(Environment.NewLine);
                    }
                    builder.Append("- ").Append(item.Message);
                }
                return builder.ToString();
            }
        }
    }
}

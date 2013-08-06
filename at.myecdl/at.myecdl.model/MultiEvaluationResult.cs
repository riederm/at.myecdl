using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model {
    public class MultiEvaluationResult : IEvaluationResult {

        private IList<IEvaluationResult> results = new List<IEvaluationResult>();

        public void add(IEvaluationResult result) {
            results.Add(result);
        }


        public bool IsOk {
            get { return results.All(result => result.IsOk); }
        }

        public string Message {
            get {
                var builder = new StringBuilder();
                if (results.Count > 1) {
                    builder.Append("Folgende Bedingungen wurden nicht erfüllt:");
                    foreach (var result in results) {
                        builder.Append(Environment.NewLine)
                                .Append(result.Message);
                    }
                } else if (results.Count == 1){
                    builder.Append(results.First().Message);
                }

                return builder.ToString();
            
            }
        }
    }
}

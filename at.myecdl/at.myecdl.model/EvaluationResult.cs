using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model {
    public class EvaluationResult : IEvaluationResult {
        public static readonly IEvaluationResult OK = new EvaluationResult(true);

        public static IEvaluationResult Error(string cause) {
            return new EvaluationResult(false, cause);
        }

        public EvaluationResult(bool isOk) {
            IsOk = isOk;
        }

        public EvaluationResult(bool isOk, string message) {
            IsOk = isOk;
            Message = message;
        }

        public bool IsOk{ get; private set; }
        public string Message { get; private set; }
    }
}

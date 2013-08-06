using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model.file.task {
    [Task(Id = "ft.004")]
    public class FileTask004 : AbstractFileTask {

        public FileTask004():base(null, null) {

        }

        public override IEvaluationResult Evaluate() {
            throw new NotImplementedException();
        }

        public override void Setup() {
            throw new NotImplementedException();
        }
    }
}

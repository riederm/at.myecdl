using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using System.IO;
using at.myecdl.model.persistence;

namespace at.myecdl.model.file.task {
    
    [FileTask(TaskReference = "ft.003")]
    public class FileTask003 : AbstractFileTask {
        private const string FOLDER = "M2_Demo_Vista";
        private const string OLD = "reisen.xlsx";
        private const string NEW = "fahrten.xlsx";
        private IFileConditionFactory conditionFactory;

        [Inject]
        public FileTask003(IFileSystem fs, IVolumeProvider vp, IFileConditionFactory conditionFactory)
            : base(fs, vp) {
                this.conditionFactory = conditionFactory;
        }


        public override IEvaluationResult Evaluate() {
            var oldFile = Path.Combine(FOLDER, OLD);
            var newFile = Path.Combine(FOLDER, NEW);

            var condition = conditionFactory.File(oldFile).WasRenamedTo(newFile);
            return condition.Evaluate();            
        }

        public override void Setup() {
            fs.CreateFile(Path.Combine(FOLDER, OLD));
        }
    }
}

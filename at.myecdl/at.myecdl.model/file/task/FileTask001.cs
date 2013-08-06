using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace at.myecdl.model.file.task {

    public class FileTask001 : AbstractFileTask {
        private const string FOLDER = @"M2_Demo_Vista\arbeit\tabellen\2005";
        private IFileConditionFactory conditionFactory;

        [Inject]
        public FileTask001(IFileSystem fs, IVolumeProvider vp, IFileConditionFactory conditionFactory)
            : base(fs, vp) {
            this.conditionFactory = conditionFactory;
        }

        public override void Setup() {
            fs.CreateFolder(FOLDER);
        }

        public override IEvaluationResult Evaluate() {
            var condition = conditionFactory.Folder(FOLDER).WasDeleted();
            return condition.Evaluate();
        }
    }
}

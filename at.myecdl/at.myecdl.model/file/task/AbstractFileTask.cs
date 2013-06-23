using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace at.myecdl.model.file.task {
    public abstract class AbstractFileTask : IEvaluator {

        protected IFileSystem fs;
        protected IVolumeProvider vp;

        [Inject]
        public AbstractFileTask(IFileSystem fileSystem, IVolumeProvider volumeProvider) {
            this.fs = fileSystem;
            this.vp = volumeProvider;
        }

        public abstract IEvaluationResult Evaluate();

        public abstract void Setup();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace at.myecdl.model.file.task {

    public class FileTask001 : AbstractFileTask {
        private const string FOLDER = @"M2_Demo_Vista\arbeit\tabellen\2005";

        [Inject]
        public FileTask001(IFileSystem fs, IVolumeProvider vp) : base(fs, vp)  {
        }

        public override void Setup() {
            fs.CreateFolder(FOLDER);
        }

        public override IEvaluationResult Evaluate() {
            if (!fs.Exists(FOLDER)) {
                return EvaluationResult.OK;
            }
            return EvaluationResult.Error(String.Format("Der Ordern '{0}' wurde nicht gelöscht", vp.GetFullPath(FOLDER)));
        }
    }
}

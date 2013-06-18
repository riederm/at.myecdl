using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using at.myecdl.util;

namespace at.myecdl.model.impl {
    class ExerciseImpl : IExercise{
        public IEvaluator Evaluator{ get; set; }
        public string Description { get; set; }
    }

    public class ExerciseFactory : IFactory<IExercise> {

        public IExercise Create() {
            return new ExerciseImpl();
        }
    }
}

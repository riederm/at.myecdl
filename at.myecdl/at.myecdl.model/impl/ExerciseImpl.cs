using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using at.myecdl.util;

namespace at.myecdl.model.impl {
    public class ExerciseImpl : IExercise{


        public IEvaluator Evaluator{ get; set; }
        public string Description { get; set; }

        public void Setup() {
            IEvaluator evaluator = Evaluator;
            var setupMethods = evaluator.GetType().GetMethods()
                                        .Where(m => 
                                             m.GetCustomAttributes(typeof(SetupAttribute), false).Length > 0);

            evaluator.Setup();
        }


        public IEvaluationResult Evaluate() {
            return Evaluator.Evaluate();
        }
    }

    public class ExerciseFactory : IFactory<IExercise> {

        public IExercise Create() {
            return new ExerciseImpl();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Ninject;

namespace at.myecdl.model.persistence.impl {
    public class TaggedExerciseInitializer {

        private IKernel kernel;
        private Dictionary<string, Type> typeMap;

        [Inject]
        public TaggedExerciseInitializer(IKernel kernel) {
            this.kernel = kernel;
        }

        private Dictionary<string, Type> getTypeMap() {
            if (typeMap == null) {
                typeMap = new Dictionary<string, Type>();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var myEcdlAssemblies = assemblies.Where(a => a.FullName.StartsWith("at.myecdl"));

                var taggedFileTasks = new List<Type>();
                foreach (var assembly in myEcdlAssemblies) {
                    var taggedAssemblyTypes = assembly.GetTypes().Where(t => t.IsDefined(typeof(TaskAttribute), false));
                    taggedFileTasks.AddRange(taggedAssemblyTypes.ToArray());
                }

                foreach (var taskType in taggedFileTasks) {
                    var attribute = (TaskAttribute)Attribute.GetCustomAttribute(taskType, typeof(TaskAttribute));
                    if (!String.IsNullOrEmpty(attribute.Id)) {
                        typeMap.Add(attribute.Id, taskType);
                        kernel.Bind(taskType).ToSelf();
                    }
                }
            }
            return typeMap;
        }

        public bool FillFromTaggedTask(String id, IExercise task) {
            var typeMap = getTypeMap();

            Type taskType;
            if (typeMap.TryGetValue(id, out taskType)) {
                var attribute = (TaskAttribute)Attribute.GetCustomAttribute(taskType, typeof(TaskAttribute));

                IEvaluator evaluator = (IEvaluator)kernel.Get(taskType);
                task.Evaluator = evaluator;
                if (!String.IsNullOrEmpty(attribute.Text)) {
                    task.Description = attribute.Text;
                }
                return true;
            }
            //we could not find the type
            return false;
        }
    }
}

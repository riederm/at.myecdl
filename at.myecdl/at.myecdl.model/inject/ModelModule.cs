using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using at.myecdl.util;
using at.myecdl.model.impl;
using at.myecdl.model.persistence;

namespace at.myecdl.model.inject {
    public class ModelModule : NinjectModule {

        public override string Name {
            get {
                return GetType().Namespace;
            }
        }

        public override void Load() {
            Bind<IProvider<ITest>>().To<MockedTestProviderImpl>();
            Bind<ITestProvider>().To<SimpleTestProvider>();
            Bind<TestDeserializer>().ToSelf();
            Bind<XmlQuestionConverter>().ToSelf();
            
            BindFactories();
            
        }

        private void BindFactories() {
            Bind<IFactory<ITest>>().To<TestFactory>();
            Bind<IFactory<IQuestion>>().To<QuestionFactory>();
            Bind<IFactory<IAnswer>>().To<AnswerFactory>();
        }
    }
}

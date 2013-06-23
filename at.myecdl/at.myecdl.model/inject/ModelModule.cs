using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using at.myecdl.util;
using at.myecdl.model.impl;
using at.myecdl.model.persistence;
using at.myecdl.model.file.impl;
using at.myecdl.model.file;
using ecdl.demo.model.util;
using at.myecdl.model.persistence.impl;
using System.IO;

namespace at.myecdl.model.inject {
    public class ModelModule : NinjectModule {

        public override string Name {
            get {
                return GetType().Namespace;
            }
        }

        public override void Load() {
            Bind<IProvider<ITest>>().To<MockedTestProviderImpl>();
            Bind<ITestProvider>().To<SimpleTestProviderImpl>();


            Bind<Random>().ToMethod(x => new Random()).InSingletonScope();
            Bind<FileNameFactory>().ToSelf();

            Bind<IFileSystem>().To<FileSystemImpl>().WhenInjectedInto(typeof(VolumeFileSystemDecorator));
            Bind<IFileSystem>().To<VolumeFileSystemDecorator>();

            Bind<IVolumeProvider>().To<VirtualDriveProviderImpl>()
                .InSingletonScope()
                .OnDeactivation(new Action<VirtualDriveProviderImpl>(v => v.Dispose()));

            BindFactories();
            BindPersistence();
        }

        private void BindFactories() {
            Bind<IFactory<ITest>>().To<TestFactory>();
            Bind<IFactory<IQuestion>>().To<QuestionFactory>();
            Bind<IFactory<IAnswer>>().To<AnswerFactory>();
            Bind<IFactory<IExercise>>().To<ExerciseFactory>();
            Bind<TaggedExerciseInitializer>().ToSelf();
        }

        private void BindPersistence() {
            
            Bind<ITestDeserializer>().To<TestDeserializerImpl>(); 
            Bind<XmlQuestionConverter>().ToSelf();

            Bind<Stream>()
                .ToConstant<Stream>(File.OpenRead(@"persistence/xml/questionTest.xml"))
                .Named("test.input.xml");
            Bind<Stream>()
                .ToConstant<Stream>(File.OpenRead(@"persistence/xml/fileTest.xml"))
                .Named("test.input.xml");
        }
    }
}

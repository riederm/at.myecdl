using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using at.myecdl.util;
using Ninject;

namespace at.myecdl.model.persistence.impl {
    public class TestDeserializerImpl : at.myecdl.model.persistence.ITestDeserializer {

        private XmlQuestionConverter converter;
        private IEnumerable<Stream> files;

        [Inject]
        public TestDeserializerImpl(XmlQuestionConverter converter, 
                            [Named("test.input.xml")] IEnumerable<Stream> files) {
            this.converter = converter;
            this.files = files;
        }

        public IList<ITest> LoadTest() {
            XmlSerializer serializer = new XmlSerializer(typeof(test));
            List<ITest> convertedTests = new List<ITest>();
            foreach (var input in files) {

                var test = (test)serializer.Deserialize(input);
                ITest convertedTest = converter.convert(test);
                convertedTests.Add(convertedTest);
            }
            return convertedTests;
        }
    }

    public class XmlQuestionConverter{

        private IFactory<IAnswer> answerFactory;
        private IFactory<IQuestion> questionFactory;
        private IFactory<ITest> testFactory;
        private IFactory<IExercise> exerciseFactory;
        private IKernel kernel;
        private TaggedExerciseInitializer taggedInitializer;
        private IVolumeProvider volumeProvider;

        [Inject]
        public XmlQuestionConverter(IFactory<ITest> testFactory, IFactory<IQuestion> questionFactory, 
            IFactory<IAnswer> answerFactory, IFactory<IExercise> exerciseFactory,
            IKernel kernel, TaggedExerciseInitializer taggedInitializer, IVolumeProvider volumeProvider){
            this.testFactory = testFactory;
            this.questionFactory = questionFactory;
            this.answerFactory = answerFactory;
            this.exerciseFactory = exerciseFactory;
            this.kernel = kernel;
            this.taggedInitializer = taggedInitializer;
            this.volumeProvider = volumeProvider;
        }

        public ITest convert(test xmlTest) {
            var test = testFactory.Create();
            test.Name = xmlTest.name;
            test.Description = xmlTest.description;
            foreach (var item in xmlTest.Items) {
                ITask task = null;
                if (item is question) {
                    task = convert((question) item);
                } else if (item is exercise) {
                    task = convert((exercise)item);
                } else {
                    System.Console.WriteLine("unknown task type: " + item.GetType());
                }

                if (task != null) {
                    test.Tasks.Add(task);
                    task.Description = task.Description.Replace(VolumeProvider.PATH_PLACEHOLDER,
                                                                volumeProvider.GetPath());
                }
            }
            return test;
        }

        private ITask convert(exercise xmlExercise) {
            var exercise = exerciseFactory.Create();
            exercise.Description = xmlExercise.text;

            if (!String.IsNullOrEmpty(xmlExercise.id)) {
                taggedInitializer.FillFromTaggedTask(xmlExercise.id, exercise);
            }

            if (exercise.Evaluator == null) {
                Type evaluatorType = Type.GetType(xmlExercise.@class);
                if (evaluatorType != null) {

                    if (!typeof(IEvaluator).IsAssignableFrom(evaluatorType)) {
                        log("type is no IEvaluator");
                    }

                    kernel.Bind(evaluatorType).ToSelf();
                    IEvaluator evaluator = (IEvaluator)kernel.Get(evaluatorType);
                    exercise.Evaluator = evaluator;
                }
            }

            return exercise;
        }

        private void log(string p) {
            System.Console.WriteLine(p);
        }

        private IQuestion convert(question xmlQuestion) {
            var question = questionFactory.Create();

            question.Description = "Answer the Question.";
            question.Question = xmlQuestion.text;
            question.IsAnswered = false;

            foreach (var xmlAnswer in xmlQuestion.answer) {
                question.Answers.Add(convert(xmlAnswer));
            }
            return question;
        }

        private IAnswer convert(answer xmlAnswer) {
            var answer = answerFactory.Create();
            answer.Answer = xmlAnswer.text;
            answer.IsCorrect = xmlAnswer.isCorrect;
            answer.Checked = false;
            return answer;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using at.myecdl.util;
using Ninject;

namespace at.myecdl.model.persistence {
    public class TestDeserializer {

        private XmlQuestionConverter converter;

        [Inject]
        public TestDeserializer(XmlQuestionConverter converter) {
            this.converter = converter;
        }

        public ITest LoadTest() {
            XmlSerializer serializer = new XmlSerializer(typeof(test));
            var test = (test)serializer.Deserialize(File.OpenRead(@"persistence/questionTest.xml"));
            return converter.convert(test);
        }
    }

    public class XmlQuestionConverter{

        private IFactory<IAnswer> answerFactory;
        private IFactory<IQuestion> questionFactory;
        private IFactory<ITest> testFactory;
        private IFactory<IExercise> exerciseFactory;

        [Inject]
        public XmlQuestionConverter(IFactory<ITest> testFactory, IFactory<IQuestion> questionFactory, IFactory<IAnswer> answerFactory, IFactory<IExercise> exerciseFactory) {
            this.testFactory = testFactory;
            this.questionFactory = questionFactory;
            this.answerFactory = answerFactory;
            this.exerciseFactory = exerciseFactory;
        }

        public ITest convert(test xmlTest) {
            var test = testFactory.Create();
            test.Name = xmlTest.name;
            test.Description = xmlTest.description;
            foreach (var item in xmlTest.Items) {
                if (item is question) {
                    test.Tasks.Add(convert((question) item));
                } else if (item is exercise) {
                    test.Tasks.Add(convert((exercise)item));
                } else {
                    System.Console.WriteLine("unknown task type: " + item.GetType());
                }
            }
            return test;
        }

        private ITask convert(exercise xmlExercise) {
            var exercise = exerciseFactory.Create();
            exercise.Description = xmlExercise.text;
            Type evaluatorType = Type.GetType(xmlExercise.@class);
            if (evaluatorType == null) {
                log("cannot parse type"); 
            }

            if (!typeof(IEvaluator).IsAssignableFrom(evaluatorType)) {
                log("type is no IEvaluator");
            }
            IEvaluator evaluator = (IEvaluator)Activator.CreateInstance(evaluatorType);
            exercise.Evaluator = evaluator;
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

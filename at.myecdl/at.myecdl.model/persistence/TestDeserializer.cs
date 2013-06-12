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

        [Inject]
        public XmlQuestionConverter(IFactory<ITest> testFactory, IFactory<IQuestion> questionFactory, IFactory<IAnswer> answerFactory) {
            this.testFactory = testFactory;
            this.questionFactory = questionFactory;
            this.answerFactory = answerFactory;
        }

        public ITest convert(test xmlTest) {
            var test = testFactory.Create();
            test.Name = xmlTest.name;
            test.Description = xmlTest.description;
            foreach (var xmlQuestion in xmlTest.question) {
                test.Tasks.Add(convert(xmlQuestion));
            }
            return test;
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

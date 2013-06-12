using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using at.myecdl.model.persistence;
using at.myecdl.model.impl;
using at.myecdl.util;
using Moq;

namespace at.myecdl.model.test {
    [TestClass]
    public class XmlQuestionConverterTest {

        private XmlQuestionConverter converter;

        [TestInitialize]
        public void Init() {
            converter = new XmlQuestionConverter(new TestFactory(), new QuestionFactory(), new AnswerFactory());
        }

        [TestMethod]
        public void ShouldFillTestName() {
            var xmlTest = new global::test();
            xmlTest.name = "MyTest";
            xmlTest.description = "MyDescription";
            xmlTest.question = new question[0];
            var test = converter.convert(xmlTest);

            Assert.AreEqual(test.Name, "MyTest");
            Assert.AreEqual(test.Description, "MyDescription");
        }


        [TestMethod]
        public void ShouldFillQuestions() {
            var xmlTest = new global::test();
            xmlTest.name = "MyTest";
            xmlTest.question = new question[2];

            xmlTest.question[0] = new question();
            xmlTest.question[0].answer = new answer[0];
            xmlTest.question[0].text = "question1";

            xmlTest.question[1] = new question();
            xmlTest.question[1].answer = new answer[0];
            xmlTest.question[1].text = "question2";

            var test = converter.convert(xmlTest);

            Assert.IsNotNull(test.Tasks);
            Assert.AreEqual(test.Tasks.Count, 2);
            Assert.AreEqual("question1", ((IQuestion)test.Tasks[0]).Question);
            Assert.AreEqual("question2", ((IQuestion)test.Tasks[1]).Question);
        }

        [TestMethod]
        public void ShouldFillAnswers() {
            var xmlTest = new global::test();
            xmlTest.name = "MyTest";
            xmlTest.question = new question[2];

            xmlTest.question[0] = new question();
            xmlTest.question[0].answer = new answer[2];
            xmlTest.question[0].text = "question1";
            xmlTest.question[0].answer[0] = createXmlAnswer("answer1");
            xmlTest.question[0].answer[1] = createXmlAnswer("answer2", true);
            

            xmlTest.question[1] = new question();
            xmlTest.question[1].answer = new answer[1];
            xmlTest.question[1].text = "question2";
            xmlTest.question[1].answer[0] = createXmlAnswer("answer1", true);

            var test = converter.convert(xmlTest);

            Assert.IsNotNull(test.Tasks);
            Assert.AreEqual(test.Tasks.Count, 2);
            Assert.AreEqual("question1", ((IQuestion)test.Tasks[0]).Question);
            Assert.AreEqual(((IQuestion)test.Tasks[0]).Answers.Count, 2);
            Assert.AreEqual(((IQuestion)test.Tasks[0]).Answers[0].Answer, "answer1");
            Assert.AreEqual(((IQuestion)test.Tasks[0]).Answers[1].Answer, "answer2");

            Assert.AreEqual("question2", ((IQuestion)test.Tasks[1]).Question);
            Assert.AreEqual(((IQuestion)test.Tasks[1]).Answers.Count, 1);
            Assert.AreEqual(((IQuestion)test.Tasks[1]).Answers[0].Answer, "answer1");

        }

        private answer createXmlAnswer(string p, bool isCorrect = false) {
            answer answer = new answer();
            answer.text = p;
            answer.isCorrect = isCorrect;
            return answer;
        }
    }
}

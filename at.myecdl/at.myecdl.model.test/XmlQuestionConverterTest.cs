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
            converter = new XmlQuestionConverter(new TestFactory(), new QuestionFactory(), new AnswerFactory(), new ExerciseFactory());
        }

        [TestMethod]
        public void ShouldFillTestName() {
            var xmlTest = new at.myecdl.model.persistence.test();
            xmlTest.name = "MyTest";
            xmlTest.description = "MyDescription";
            xmlTest.Items = new List<object>();
            var test = converter.convert(xmlTest);

            Assert.AreEqual(test.Name, "MyTest");
            Assert.AreEqual(test.Description, "MyDescription");
        }


        [TestMethod]
        public void ShouldFillQuestions() {
            var xmlTest = new at.myecdl.model.persistence.test();
            xmlTest.name = "MyTest";
            xmlTest.Items = new List<object>();

            xmlTest.Items.Add(new question() {
                answer = new List<answer>(),
                text = "question1"
            });
            xmlTest.Items.Add(new question() {
                answer = new List<answer>(),
                text = "question2"
            });

            var test = converter.convert(xmlTest);

            Assert.IsNotNull(test.Tasks);
            Assert.AreEqual(test.Tasks.Count, 2);
            Assert.AreEqual("question1", ((IQuestion)test.Tasks[0]).Question);
            Assert.AreEqual("question2", ((IQuestion)test.Tasks[1]).Question);
        }

        [TestMethod]
        public void ShouldFillAnswers() {
            var xmlTest = new at.myecdl.model.persistence.test();
            xmlTest.name = "MyTest";
            xmlTest.Items = new List<object>();

            xmlTest.Items.Add(new question() {
                text = "question1",
                answer = new List<answer>() {
                    new answer(){ text = "answer1" },
                    new answer(){ text = "answer2", isCorrect=true }
                },
            });
            xmlTest.Items.Add(new question() {
                text = "question2",
                answer = new List<answer>() {
                    new answer(){ text = "answer1" },
                },
            });

            var test = converter.convert(xmlTest);

            Assert.IsNotNull(test.Tasks);
            Assert.AreEqual(test.Tasks.Count, 2);
            Assert.AreEqual("question1", ((IQuestion)test.Tasks[0]).Question);
            Assert.AreEqual(((IQuestion)test.Tasks[0]).Answers.Count, 2);
            Assert.AreEqual(((IQuestion)test.Tasks[0]).Answers[0].Answer, "answer1");
            Assert.AreEqual(((IQuestion)test.Tasks[0]).Answers[1].Answer, "answer2");
            Assert.AreEqual(((IQuestion)test.Tasks[0]).Answers[1].IsCorrect, true);
            Assert.AreEqual("question2", ((IQuestion)test.Tasks[1]).Question);
            Assert.AreEqual(((IQuestion)test.Tasks[1]).Answers.Count, 1);
            Assert.AreEqual(((IQuestion)test.Tasks[1]).Answers[0].Answer, "answer1");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using at.myecdl.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHamcrest.Core;

namespace at.myecdl.model.test {


    [TestClass]
    public class QuestionBuilderTest {

        [TestMethod]
        public void ShouldCreateEmptyQuestion() {
            QuestionTestBuilder builder = new QuestionTestBuilder();
            builder.Question("myQuestion");

            IQuestion question = (IQuestion)builder.Test.Tasks[0];
            
            Assert.IsNotNull(question);
            Assert.AreEqual("myQuestion", question.Question);
            Assert.AreEqual(0, question.Answers.Count);
            Assert.IsNotNull(question.Answers);
            Assert.IsNotNull(question.Description);
            Assert.IsFalse(question.IsAnswered);
        }

        [TestMethod]
        public void ShouldAddAnswersToQuestion() {
            QuestionTestBuilder builder = new QuestionTestBuilder();
            builder.Question("myQuestion")
                    .Answer("Answer1")
                    .Answer("Answer2", true);

            IQuestion question = (IQuestion)builder.Test.Tasks[0];
            
            Assert.AreEqual(question.Answers.Count, 2);
            Assert.IsNotNull(question.Answers[0]);

            Assert.AreEqual("Answer1", question.Answers[0].Answer);
            Assert.AreEqual(false, question.Answers[0].Checked);
            Assert.AreEqual(false, question.Answers[0].IsCorrect);

            Assert.AreEqual("Answer2", question.Answers[1].Answer);
            Assert.AreEqual(false, question.Answers[1].Checked);
            Assert.AreEqual(true, question.Answers[1].IsCorrect);
            
        }
    }
}

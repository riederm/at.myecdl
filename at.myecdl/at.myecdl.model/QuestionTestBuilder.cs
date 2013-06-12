using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using at.myecdl.model.impl;

namespace at.myecdl.model
{
    public interface IncompleteQuestion {
        IncompleteQuestion Answer(string answer, bool isCorrect);
        IncompleteQuestion Answer(string answer);
        IQuestion Build();
    }


    public class QuestionTestBuilder: IncompleteQuestion
    {
        private TestImpl test = new TestImpl();
        private IQuestion question;

        public QuestionTestBuilder() {
        }

        private void createNewQuestion() {
            question = new QuestionImpl();
        }

        public ITest Test {
            get {
                return test;
            }
        }

        public IncompleteQuestion Question(String question)
        {
            createNewQuestion();
            test.Tasks.Add(this.question);
            this.question.Question = question;

            return this;
        }
 
        public IncompleteQuestion Answer(string answer, bool isCorrect) {
            IAnswer a = new AnswerImpl();
            a.Answer = answer;
            a.IsCorrect = isCorrect;
            question.Answers.Add(a);
            return this;
        }

        public IncompleteQuestion Answer(string answer) {
            return Answer(answer, false);
        }

        public IQuestion Build() {
            IQuestion q = question;
            question = null;
            return q;
        }

    }
}

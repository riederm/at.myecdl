using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using at.myecdl.util;

namespace at.myecdl.model.impl {
    public class MockedTestProviderImpl : IProvider<ITest> {

        public ITest Get() {
            QuestionTestBuilder builder = new QuestionTestBuilder();



            builder.Question("My first Question")
                            .Answer("Answer#1")
                            .Answer("Answer#2")
                            .Answer("Answer#3", true)
                            .Answer("Answer#4");


            return builder.Test;
        }
    }
}

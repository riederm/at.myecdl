using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using at.myecdl.util;

namespace at.myecdl.model.impl {
    public class QuestionFactory : IFactory<IQuestion> {

        public IQuestion Create() {
            return new QuestionImpl();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using at.myecdl.util;

namespace at.myecdl.model.impl {
    public class AnswerFactory : IFactory<IAnswer> {

        public IAnswer Create() {
            return new AnswerImpl();
        }
    }
}

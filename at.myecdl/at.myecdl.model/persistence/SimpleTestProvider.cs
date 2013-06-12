using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Ninject;

namespace at.myecdl.model.persistence {
    public class SimpleTestProvider : ITestProvider{
        private TestDeserializer deserializer;

        [Inject]
        public SimpleTestProvider(TestDeserializer deserializer) {
            this.deserializer = deserializer;
        }

        public IList<ITest> GetTests() {
            return new List<ITest>(new ITest[]{deserializer.LoadTest()});
        }
    }
}

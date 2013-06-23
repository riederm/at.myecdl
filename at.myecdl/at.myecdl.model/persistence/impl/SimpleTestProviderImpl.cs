using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Ninject;

namespace at.myecdl.model.persistence.impl {
    public class SimpleTestProviderImpl : ITestProvider{
        private ITestDeserializer deserializer;

        [Inject]
        public SimpleTestProviderImpl(ITestDeserializer deserializer) {
            this.deserializer = deserializer;
        }

        public IList<ITest> GetTests() {
            return deserializer.LoadTest();
        }
    }
}

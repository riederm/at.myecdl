using System;
using System.Collections.Generic;
namespace at.myecdl.model.persistence {
    public interface ITestDeserializer {
        IList<at.myecdl.model.ITest> LoadTest();
    }
}

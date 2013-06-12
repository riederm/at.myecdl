using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.util {
    public interface IProvider<T> {
        T Get();
    }
}

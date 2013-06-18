using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace at.myecdl.model.file.impl {
    public abstract class EmptyContentStream  {

        public static MemoryStream Size(int bytes) {
            var content = new byte[bytes];
            return new MemoryStream(content, false);
        }

        public static MemoryStream KiloByte() {
            return Size(1024);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace at.myecdl.model.file.impl {
    public class FileSystemImpl : IFileSystem {

        public bool Exists(string fullPath) {
            return File.Exists(fullPath) || Directory.Exists(fullPath);
        }

        public void CreateFile(string fullPath) {
            using (System.IO.FileStream fs = System.IO.File.Create(fullPath)) {
            }
        }

        public void CreateFile(string fullPath, System.IO.Stream data) {
            using (System.IO.FileStream fs = System.IO.File.Create(fullPath)) {
                data.CopyTo(fs);
            }
        }

        public void CreateFolder(string fullPath) {
            Directory.CreateDirectory(fullPath);
        }

        public void CreateFiles(IEnumerable<string> fullPaths) {
            foreach (var fullpath in fullPaths) {
                CreateFile(fullpath);
            }
        }

        public void CreateFiles(IEnumerable<string> fullPaths, Stream content) {
            foreach (var item in fullPaths) {
                content.Position = 0;
                CreateFile(item, content);
            }
        }
    }
}

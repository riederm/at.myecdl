using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace at.myecdl.model.file {
    public interface IFileSystem {

        bool Exists(string fullPath);
        bool FileExists(string fullPath);
        bool DirectoryExists(string fullPath);
        void CreateFile(string fullPath);
        void CreateFile(string fullPath, Stream content);
        void CreateFolder(string fullPath);

        void CreateFiles(IEnumerable<string> fullPaths);
        void CreateFiles(IEnumerable<string> fullPaths, Stream content);

        
    
    }


}

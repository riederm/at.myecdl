using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ninject;
using at.myecdl.model.impl;
using ICSharpCode.SharpZipLib.Zip;

namespace at.myecdl.model.file.impl {
    public class FileSystemImpl : IFileSystem {

        public bool Exists(string volumeRelativePath) {
            return FileExists(volumeRelativePath) || DirectoryExists(volumeRelativePath);
        }

        public bool DirectoryExists(string volumeRelativePath) {
            return Directory.Exists(volumeRelativePath);
        }

        public bool FileExists(string volumeRelativePath) {
            return System.IO.File.Exists(volumeRelativePath);
        }


        public void CreateFile(string volumeRelativePath) {
            using (System.IO.FileStream fs = System.IO.File.Create(volumeRelativePath)) {
            }
        }

        public void CreateFile(string volumeRelativePath, System.IO.Stream data) {
            using (System.IO.FileStream fs = System.IO.File.Create(volumeRelativePath)) {
                data.CopyTo(fs);
            }
        }

        public void CreateFolder(string volumeRelativePath) {
            Directory.CreateDirectory(volumeRelativePath);
        }

        public void CreateFiles(IEnumerable<string> volumeRelativePaths) {
            foreach (var volumeRelativePath in volumeRelativePaths) {
                CreateFile(volumeRelativePath);
            }
        }

        public void CreateFiles(IEnumerable<string> volumeRelativePaths, Stream content) {
            foreach (var item in volumeRelativePaths) {
                content.Position = 0;
                CreateFile(item, content);
            }
        }



    }  
}

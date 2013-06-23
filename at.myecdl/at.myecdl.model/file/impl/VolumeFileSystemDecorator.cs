using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model.file.impl {
    public class VolumeFileSystemDecorator : IFileSystem {
        private IFileSystem fsDelegate;
        private IVolumeProvider volumeProvider;

        public VolumeFileSystemDecorator(IFileSystem fsDelegate, IVolumeProvider volumeProvider) {
            this.fsDelegate = fsDelegate;
            this.volumeProvider = volumeProvider;
        }

        public bool Exists(string fullPath) {
            string path = volumeProvider.GetFullPath(fullPath);
            return fsDelegate.Exists(path);
        }

        public void CreateFile(string fullPath) {
            string path = volumeProvider.GetFullPath(fullPath);
            fsDelegate.CreateFile(path);
        }

        public void CreateFile(string fullPath, System.IO.Stream content) {
            string path = volumeProvider.GetFullPath(fullPath);
            fsDelegate.CreateFile(path, content);
        }

        public void CreateFolder(string fullPath) {
            string path = volumeProvider.GetFullPath(fullPath);
            fsDelegate.CreateFolder(path);
        }

        public void CreateFiles(IEnumerable<string> fullPaths) {
            var volumePaths = GetVolumePaths(fullPaths);
            fsDelegate.CreateFiles(volumePaths);
        }

        public void CreateFiles(IEnumerable<string> fullPaths, System.IO.Stream content) {
            var volumePaths = GetVolumePaths(fullPaths);
            fsDelegate.CreateFiles(volumePaths, content);
        }

        private List<string> GetVolumePaths(IEnumerable<string> fullPaths) {
            var volumePaths = new List<string>(fullPaths.Count());
            foreach (var path in fullPaths) {
                volumePaths.Add(volumeProvider.GetFullPath(path));
            }
            return volumePaths;
        }
    }
}

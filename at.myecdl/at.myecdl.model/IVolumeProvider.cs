using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model {
    public interface IVolumeProvider : IDisposable{
        void Initialize();
        void Close();

        string GetPath();
        string GetFullPath(string fileName);
    }

    public abstract class VolumeProvider : IVolumeProvider {
        public const string PATH_PLACEHOLDER = "{PATH}";
        
        public virtual void Initialize() { }

        public virtual void Close() { }

        public abstract string GetPath();

        public virtual string GetFullPath(string fileName) {
            return System.IO.Path.Combine(GetPath(), fileName);
        }

        public void Dispose() {
        }
    }
}

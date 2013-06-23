using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

using System.IO;
using at.myecdl.model;

namespace ecdl.demo.model.util {
    public class VirtualDriveProviderImpl : VolumeProvider, IDisposable {

        private static char[] MAP_CHARS = { 'P', 'H', 'I', 'J', 'K', 'L', 'X', 'Y', 'Z' };

        public DirectoryInfo MappedPath { get; private set; }

        private char mappedChar;
        private bool isInitialized = false;

        public VirtualDriveProviderImpl() {
            IsMapped = false;
        }

        private void MapDrive(char letter, string path) {
            if (!DefineDosDevice(0, devName(letter), path))
                throw new Win32Exception();
        }
        private void UnmapDrive(char letter) {
            if (!DefineDosDevice(2, devName(letter), null))
                throw new Win32Exception();
        }
        private string GetDriveMapping(char letter) {
            var sb = new StringBuilder(259);
            if (QueryDosDevice(devName(letter), sb, sb.Capacity) == 0) {
                // Return empty string if the drive is not mapped
                int err = Marshal.GetLastWin32Error();
                if (err == 2)
                    return "";
                throw new Win32Exception();
            }
            return sb.ToString().Substring(4);
        }

        private string devName(char letter) {
            return new string(char.ToUpper(letter), 1) + ":";
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DefineDosDevice(int flags, string devname, string path);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int QueryDosDevice(string devname, StringBuilder buffer, int bufSize);

        public override void Initialize() {
            UnmapDrive('P');
            string availableChar = GetAvailableVolumeChar();

            if (availableChar != null) {
                MappedPath = GetLocalTemporaryDirectory();
                mappedChar = availableChar.ToCharArray()[0];
                MapDrive(mappedChar, MappedPath.FullName);
            } else {
                throw new InvalidOperationException("Keiner der folgenden Laufwerksbuchstaben ist verfügbar: " + string.Join(",", MAP_CHARS));
            }
        }

        private DirectoryInfo GetLocalTemporaryDirectory() {
            //GetTempFileName immediately creates 
            var fullPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var directory = Directory.CreateDirectory(fullPath);

            if (directory.Exists) {
                return directory;
            } else {
                throw new InvalidOperationException(String.Format("Das Virtuelle Laufwerk konnte nicht auf '{0}' erstellt werden.", fullPath));
            }
        }

        private string GetAvailableVolumeChar() {
            string availableChar = null;
            foreach (var volumeChar in MAP_CHARS) {
                if (GetDriveMapping(volumeChar).Equals(String.Empty)) {
                    availableChar = volumeChar.ToString();
                    break;
                }
            }
            return availableChar;
        }

        public override void Close() {
            Dispose();
        }

        public bool IsMapped { get; private set; }

        public override String GetPath() {
            if (!isInitialized) {
                isInitialized = true;
                Initialize();
                IsMapped = true;
            }
            if (IsMapped) {
                return Path.GetFullPath(devName(mappedChar));
            }
            return string.Empty;
        }

        public new void Dispose() {
            if (IsMapped) {
                UnmapDrive(mappedChar);
            }

            if (MappedPath != null && MappedPath.Exists) {
                try {
                    MappedPath.Delete(true);
                } catch { /*ignore*/}
            }
            IsMapped = false;
            MappedPath = null;
            base.Dispose();
        }
    }
}

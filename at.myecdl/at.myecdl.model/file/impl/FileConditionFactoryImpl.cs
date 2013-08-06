using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using at.myecdl.model.impl;
using Ninject;

namespace at.myecdl.model.file.impl {
    public class FileConditionFactoryImpl : IFileConditionFactory{

        private IFileSystem fileSystem;

        [Inject]
        public FileConditionFactoryImpl(IFileSystem fileSystem) {
            this.fileSystem = fileSystem;
        }
        
         public IIncompleteFileCondition File(string oldFile) {
            return new IncompleteFileConditionImpl(oldFile, fileSystem);
        }

        public IIncompleteZipCondition Zip(string zipFile) {
            return new IncompleteZipConditionImpl(zipFile, fileSystem);
        }

        public IIncompleteFileCondition Folder(string folder) {
            return new IncompleteDirectoryConditionImpl(folder, fileSystem);
        }

        public IIncompleteFilesCondition Files(params string[] names) {
            return new IncompleteFilesConditionImpl(names, fileSystem);
        }

        public IIncompleteFilesCondition Folders(params string[] folders) {
            return new IncompleteFoldersConditionImpl(folders, fileSystem);
        }

    }

    public class IncompleteFoldersConditionImpl : IIncompleteFilesCondition, IDisposable {
        private string[] names;
        private IFileSystem fileSystem;
        public IncompleteFoldersConditionImpl(string[] names, IFileSystem fs) {
            this.names = names;
            this.fileSystem = fs;
        }

        public ICondition WereCreated() {
            return new DelegatingCondition(() => {
                var results = new MultiEvaluationResult();
                foreach (var name in names) {
                    if (!fileSystem.DirectoryExists(name)) {
                        results.add(EvaluationResult.Error(FolderMessages.NotCreated(name)));
                    }
                }
                return results;
            });
        }

        public ICondition DeletedCreated() {
            return new DelegatingCondition(() => {
                var results = new MultiEvaluationResult();
                foreach (var name in names) {
                    if (fileSystem.DirectoryExists(name)) {
                        results.add(EvaluationResult.Error(FolderMessages.NotDeleted(name)));
                    }
                }
                return results;
            });
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }

    public class IncompleteFilesConditionImpl : IIncompleteFilesCondition, IDisposable {
        private string[] names;
        private IFileSystem fileSystem;
        public IncompleteFilesConditionImpl(string[] names, IFileSystem fs) {
            this.names = names;
            this.fileSystem = fs;
        }

        public ICondition WereCreated() {
            return new DelegatingCondition(() => {
                var results = new MultiEvaluationResult();
                foreach (var name in names) {
                    if (!fileSystem.FileExists(name)) {
                        results.add(EvaluationResult.Error(FileMessages.NotCreated(name)));
                    }
                }
                return results;
            });
        }

        public ICondition DeletedCreated() {
            return new DelegatingCondition(() => {
                var results = new MultiEvaluationResult();
                foreach (var name in names) {
                    if (fileSystem.FileExists(name)) {
                        results.add(EvaluationResult.Error(FileMessages.NotDeleted(name)));
                    }
                }
                return results;
            });
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }

    public class IncompleteZipConditionImpl : IIncompleteZipCondition, IDisposable {

        private IFileSystem fs;
        private string zipFile;
        private ZipFile theZip;

        public IncompleteZipConditionImpl(string zipFile, IFileSystem fs) {
            this.fs = fs;
            this.zipFile = zipFile;
                        
        }

        private ZipFile getZip(string zipFile) {
            if (theZip == null) {
                theZip = new ZipFile(zipFile);
            }
            return theZip;
        }

        public ICondition WasAdded(string filename) {
            return new DelegatingCondition(() => {
                if (fs.FileExists(zipFile)) {
                    ZipFile zip = getZip(zipFile);
                    if (zip.FindEntry(filename, false) >= 0) {
                        return EvaluationResult.OK;
                    }
                    return EvaluationResult.Error(ZipMessages.WasNotAdded(filename, zipFile));
                }
                return EvaluationResult.Error(ZipMessages.NotCreated(zipFile));
            });
        }
         
        public ICondition WasRemoved(string filename) {
            return new DelegatingCondition(() => {
                if (fs.FileExists(zipFile)) {
                    ZipFile zip = getZip(zipFile);
                    if (zip.FindEntry(filename, false) == 0) {
                        return EvaluationResult.OK;
                    }
                    return EvaluationResult.Error(ZipMessages.WasNotRemoved(filename, zipFile));
                }
                return EvaluationResult.Error(ZipMessages.NotCreated(zipFile));
            });
        }

        public void Dispose() {
            if (theZip != null) {
                try {
                    theZip.Close();
                } catch (Exception e) {
                    //TODO log
                }
            }
        }
    }

    public abstract class ZipMessages {
        public static string NotCreated(string name) {
            return String.Format("Das Archiv wurde nicht korrekt erstellt ({0} kann nicht gefunden werden).", name);
        }

        public static string NotDeleted(string name) {
            return String.Format("Das Archiv wurde nicht korrekt gelöscht ({0} existiert noch).", name);
        }

        public static string WasNotAdded(string entry, string name) {
            return String.Format("Der Archiveintrag {0} kann im Archiv {1} nicht gefunden werden.", entry, name);
        }

        public static string WasNotRemoved(string entry, string name) {
            return String.Format("Der Archiveintrag {0} wurde nicht korrekt aus dem Archiv {1} gelöscht.", entry, name);
        }
    }

    public class IncompleteDirectoryConditionImpl : IIncompleteFileCondition {

        private IFileSystem fs;
        private string folderName;

        public IncompleteDirectoryConditionImpl(string folderName, IFileSystem fs) {
            this.fs = fs;
            this.folderName = folderName;
        }

        public ICondition WasRenamedTo(string newFolder) {
            return new DelegatingCondition(() => {
                bool newExists = fs.FileExists(newFolder);
                bool oldExists = fs.FileExists(folderName);

                if (newExists && !oldExists) {
                    return EvaluationResult.OK;
                } else if (oldExists) {
                    return EvaluationResult.Error(FolderMessages.NotRenamedOldExists(folderName));
                } else {
                    return EvaluationResult.Error(FolderMessages.NotRenamedNewNotFound(newFolder));
                }
            });
        }

        public ICondition WasDeleted() {
            return new DelegatingCondition(() => {
                bool oldExists = fs.FileExists(folderName);
                if (oldExists) {
                    return EvaluationResult.Error(FolderMessages.NotDeleted(folderName));
                }
                return EvaluationResult.OK;
            });
        }

        public ICondition WasCreated() {
            return new DelegatingCondition(() => {
                bool fileExists = fs.FileExists(folderName);
                if (!fileExists) {
                    return EvaluationResult.Error(FolderMessages.NotCreated(folderName));
                }
                return EvaluationResult.OK;
            });
        }

        public void Dispose() {
        }
    }

    public class IncompleteFileConditionImpl : IIncompleteFileCondition {
        private IFileSystem fs;
        private string fileName;

        public IncompleteFileConditionImpl(string fileName, IFileSystem fs) {
            this.fs = fs;
            this.fileName = fileName;
        }

        public ICondition WasRenamedTo(string newFile) {
            return new DelegatingCondition(() => {
                bool newExists = fs.FileExists(newFile);
                bool oldExists = fs.FileExists(fileName);

                if (newExists && !oldExists) {
                    return EvaluationResult.OK;
                } else if (oldExists) {
                    return EvaluationResult.Error(FileMessages.NotRenamedOldExists(fileName));
                } else {
                    return EvaluationResult.Error(FileMessages.NotRenamedNewNotFound(newFile));
                }
            });
        }

        public ICondition WasDeleted() {
            return new DelegatingCondition(() => {
                bool oldExists = fs.FileExists(fileName);
                if (oldExists) {
                    return EvaluationResult.Error(FileMessages.NotDeleted(fileName));
                }
                return EvaluationResult.OK;
            });
        }

        public ICondition WasCreated() {
            return new DelegatingCondition(() => {
                bool fileExists = fs.FileExists(fileName);
                if (!fileExists) {
                    return EvaluationResult.Error(FileMessages.NotCreated(fileName));
                }
                return EvaluationResult.OK;
            });
        }

        public void Dispose() {
            
        }
    }

    public abstract class FileMessages {

        public static string NotCreated(string name){
            return String.Format("Die Datei wurde nicht korrekt erstellt ({0} kann nicht gefunden werden).", name);
        }

        public static string NotDeleted(string name){
            return String.Format("Die Datei wurde nicht korrekt gelöscht ({0} existiert noch).", name);
        }

        public static string NotRenamedNewNotFound(string name) {
            return String.Format("Die Datei wurde nicht korrekt unbenannt ({0} kann nicht gefunden werden).", name);
        }

        public static string NotRenamedOldExists(string name) {
            return String.Format("Die Datei wurde nicht korrekt unbenannt ({0} existiert noch).", name);
        }
    }

    public abstract class FolderMessages{
        public static string NotCreated(string name){
            return String.Format("Der Ordner wurde nicht korrekt erstellt ({0} kann nicht gefunden werden).", name);
        }

        public static string NotDeleted(string name){
            return String.Format("Der Ordner wurde nicht korrekt gelöscht ({0} existiert noch).", name);
        }

        public static string NotRenamedNewNotFound(string name) {
            return String.Format("Der Ordner wurde nicht korrekt unbenannt ({0} kann nicht gefunden werden).", name);
        }

        public static string NotRenamedOldExists(string name) {
            return String.Format("Der Ordner wurde nicht korrekt unbenannt ({0} existiert noch).", name);
        }
    }

   
}

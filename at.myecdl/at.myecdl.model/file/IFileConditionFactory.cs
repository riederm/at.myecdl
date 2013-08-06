using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.model.file {
    public interface IFileConditionFactory {
        IIncompleteFileCondition File(string oldFile);
        IIncompleteFilesCondition Files(params string[] name);
        IIncompleteZipCondition Zip(string zipFile);
        IIncompleteFileCondition Folder(string folder);
        IIncompleteFilesCondition Folders(params string[] folders);
    }

    public interface IIncompleteFileCondition {
        ICondition WasRenamedTo(string newFolder);
        ICondition WasDeleted();
        ICondition WasCreated();
    }

    public interface IIncompleteFilesCondition {
        ICondition WereCreated();
        ICondition DeletedCreated();
    }

    public interface IIncompleteZipCondition {
        ICondition WasAdded(string filename);
        ICondition WasRemoved(string filename);
    }
}


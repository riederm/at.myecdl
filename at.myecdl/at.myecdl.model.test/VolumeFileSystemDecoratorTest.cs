using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using at.myecdl.model.file;
using Moq;
using at.myecdl.model.file.impl;

namespace at.myecdl.model.test {
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class VolumeFileSystemDecoratorTest {

        private class MockedVolumeProvider : VolumeProvider {

            public override string GetPath() {
                return @"C:\";
            }
        }

        private Mock<IFileSystem> fileSystemMock = null;
        private IFileSystem fileSystem;

        [TestInitialize]
        public void Init() {
            fileSystemMock = new Mock<IFileSystem>();
            var volumeProvider = new MockedVolumeProvider();

            
            fileSystem = new VolumeFileSystemDecorator(fileSystemMock.Object, volumeProvider); 
        }

        [TestMethod]
        public void ShouldCreateFileInVolume() {
            fileSystem.CreateFile("test.txt");
            fileSystemMock.Verify(fs => fs.CreateFile(@"C:\test.txt"));
        }

        [TestMethod]
        public void ShouldCreateFolderInVolume() {
            fileSystem.CreateFolder("myFolder");
            fileSystemMock.Verify(fs => fs.CreateFolder(@"C:\myFolder"));
        }

        [TestMethod]
        public void ShouldCreateFilesInVolume() {
            fileSystem.CreateFiles(new string[]{"file1", "file2"});
            fileSystemMock.Verify(fs => fs.CreateFiles(new List<string>(new string[]{@"C:\file1", @"C:\file2"})));
        }

        [TestMethod]
        public void ShouldForwardExists() {
            fileSystem.Exists("test");
            fileSystemMock.Verify(fs => fs.Exists(@"C:\test"));

            fileSystem.FileExists("test2");
            fileSystemMock.Verify(fs => fs.FileExists(@"C:\test2"));

            fileSystem.DirectoryExists("test3");
            fileSystemMock.Verify(fs => fs.DirectoryExists(@"C:\test3"));
        }
    }
}

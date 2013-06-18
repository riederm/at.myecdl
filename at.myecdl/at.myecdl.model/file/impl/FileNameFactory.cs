using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace at.myecdl.model.file.impl {
    public class FileNameFactory {

        private IList<string> fileNames = new List<string>(new string[] {
                                            "Vertrag",
                                            "Sommer",
                                            "Winter",
                                            "Frühling",
                                            "Winter",
                                            "Auto01",
                                            "Auto02",
                                            "Auto03",
                                            "Auto04",
                                            "Niederlassung-Ost",
                                            "Niederlassung-West",
                                            "Niederlassung-Nord",
                                            "Niederlassung-Süd",
                                            "Projekte2010",
                                            "Projekte2011",
                                            "Projekte2012",
                                            "Projekte2013",
                                            "Projekte2014",
                                            "Anleitung-en",
                                            "Anleitung-de",
                                            "Anleitung-esp",
                                            "Anleitung-it",
                                            "Anleitung-fr",
                                            "Teileliste-Produkt-A",
                                            "Teileliste-Produkt-B",
                                            "Teileliste-Produkt-C",
                                            "Teileliste-Produkt-D",
                                            "Teileliste-Produkt-E",
                                            "Teileliste-Produkt-F"
                                           });

        private IList<string> directories = new List<string>(new string[]{
                                            "Dokumente",
                                            "Archiv",
                                            "Privat",
                                            "Abteilungen",
                                            "Temporäres",
                                            "Projekte",
                                            "Dokumentation",
                                            "Temp",
                                            "Verkauf",
                                            "Einkauf",
                                            "Lager",
                                            "LagerII"
                                         });

        private string[] extensions = new string[]{
                                            "docx",
                                            "xlsx",
                                            "txt",
                                            "jpg",
                                            "png",
                                            "gif",
                                            "bmp"
                                         };

        private Random random;

        public FileNameFactory(Random random) {
            this.random = random;
        }

        public string GetRandomDirectoryName(int dept) {
            if (dept == 0) {
                return GetRandomDirectoryName();
            }
            return Path.Combine(GetRandomDirectoryName(dept - 1), GetRandomDirectoryName());
        }

        public string GetRandomDirectoryName() {
            var index = random.Next(directories.Count);
            var dir = directories[index];
            directories.RemoveAt(index);
            return dir;
        }

        public string GetRandomFileName(string extension) {
            var index = random.Next(fileNames.Count);
            var file = fileNames[index];
            fileNames.RemoveAt(index);
            StringBuilder builder = new StringBuilder(file.Length + 1 + extension.Length);
            builder.Append(file);
            if (!extension.StartsWith(".")) {
                builder.Append(".");
            }
            builder.Append(extension);
            return builder.ToString();

        }

        public IList<string> GetRandomFileNames(string extension, int count) {
            var fileNames = new List<string>(count);
            for (int i = 0; i < count; i++) {
                fileNames[i] = GetRandomFileName(extension);
            }
            return fileNames;
        }

        public IList<string> GetRandomFileNames(string extension, int min, int max) {
            int size = random.Next(min, max + 1);
            return GetRandomFileNames(extension, size);
        }

    }
}

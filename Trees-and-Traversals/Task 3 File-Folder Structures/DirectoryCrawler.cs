namespace FileFolderStructures
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class DirectoryCrawler
    {
        private Func<DirectoryInfo, IDictionary<string, List<FileInfo>>> extractingFunction;

        public DirectoryCrawler(Func<DirectoryInfo, IDictionary<string, List<FileInfo>>> fileExtractingFunction)
        {
            this.extractingFunction = fileExtractingFunction;
        }

        public Task<Folder> Craw(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            Folder root = new Folder(dir.Name);

            return Task.Run(() => this.AddFilesAndFolders(root, dir));
        }

        private Folder AddFilesAndFolders(Folder folder, DirectoryInfo dir)
        {
            IEnumerable<FileInfo> dirFiles = extractingFunction(dir).SelectMany(key => key.Value);

            List<File> files = new List<File>();

            foreach (var file in dirFiles)
            {
                files.Add(new File(file.Name)
                {
                    Size = file.Length
                });
            }

            folder.Files = files.ToArray();

            try
            {
                var dirFolders = dir.GetDirectories();
                List<Folder> folders = new List<Folder>();

                foreach (var direcotry in dirFolders)
                {
                    var current = new Folder(direcotry.Name);
                    current.Parent = folder;

                    folders.Add(current);
                    AddFilesAndFolders(current, direcotry);
                }

                folder.ChildFolders = folders.ToArray();
            }
            catch (UnauthorizedAccessException)
            {
            }

            return folder;
        }
    }
}
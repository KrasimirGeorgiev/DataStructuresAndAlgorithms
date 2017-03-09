namespace TraverseDirectory
{
    using System;
    using System.IO;
    using System.Linq;
    using HomeworkHelpers;
    using HomeworkHelpers.Enumerations;

    /// <summary>
    /// Write a program to traverse the directory C:\WINDOWS and all its subdirectories recursively 
    /// and to display all files matching the mask *.exe. Use the class System.IO.Directory.
    /// </summary>
    public class Program
    {
        private static StreamHomeworkHelper helper = new StreamHomeworkHelper();

        [STAThread]
        private static void Main()
        {
            helper.ConsoleMio.Setup();

            helper.ConsoleMio.PrintHeading("Task 2 Traverse Directory ");

            string path = helper.GetDirectory();

            DirectoryInfo dir = new DirectoryInfo(path);

            PrintFilesWhere(dir, "exe");
        }

        private static void PrintFilesWhere(
            DirectoryInfo dir, string extension, string indent = null)
        {
            if (string.IsNullOrEmpty(indent))
            {
                indent = string.Empty;
            }

            var files = helper.GetDirectoryFiles(dir)
            .Where(ext => ext.Key.Equals(extension, StringComparison.OrdinalIgnoreCase))
            .SelectMany(ext => ext.Value);

            if (files.Any())
            {
                // Console.ReadKey(true);
                helper.ConsoleMio.WriteLine(
                    "{0}Folder: {1}", ConsoleColor.DarkRed, indent, dir.Name);

                indent = "\t" + indent;

                foreach (var file in files)
                {
                    helper.ConsoleMio.WriteLine(
                        "{0}--> {1} ({2}kb)",
                        ConsoleColor.DarkCyan,
                        indent,
                        file.Name,
                        helper.ConvertFileLength(file.Length, FileLength.KB));
                }
            }

            try
            {
                var folders = dir.GetDirectories();

                foreach (var folder in folders)
                {
                    PrintFilesWhere(folder, extension, indent);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Ups... Skip this one
            }
        }
    }
}

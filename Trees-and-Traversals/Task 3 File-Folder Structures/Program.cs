namespace FileFolderStructures
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HomeworkHelpers;

    /// <summary>
    /// Define classes File { string name, int size } and Folder { string name, File[] files, 
    /// Folder[] childFolders } and using them build a tree keeping all files and folders on the 
    /// hard drive starting from C:\WINDOWS. Implement a method that calculates the sum of the 
    /// file sizes in given subtree of the tree and test it accordingly. Use recursive DFS traversal.
    /// </summary>
    public class Program
    {
        private static StreamHomeworkHelper helper = new StreamHomeworkHelper();

        [STAThread]
        private static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Task 3 File-Folder Structures ");

            string startPath = helper.GetDirectory();

            DirectoryCrawler crawler = new DirectoryCrawler(helper.GetDirectoryFiles);

            Task<Folder> crawTask = crawler.Craw(startPath);
            string chosenPath = StartWaitingSubroutine(crawTask).Result;

            Folder selectedFolder = GetSelectedSubFolder(chosenPath, crawTask);

            VerifyResult(selectedFolder, startPath);
        }

        private static void VerifyResult(Folder selectedFolder, string startPath)
        {
            long totalFileSizes = selectedFolder.GetTotalSize();
            helper.ConsoleMio.WriteLine(
                "Folder size = {0:F3}MB (Homework result)",
                ConsoleColor.DarkGreen,
                totalFileSizes / 1024 / (double)1024);

            string actualFolderPath = startPath + GetPathFromDirectory(selectedFolder);
            long actualFolderSize = GetDirSize(new DirectoryInfo(actualFolderPath));

            ConsoleColor color = ConsoleColor.DarkGreen;
            // if above 4kb difference
            if (Math.Abs(totalFileSizes - actualFolderSize) > 4096)
            {
                color = ConsoleColor.Red;
            }

            helper.ConsoleMio.WriteLine(
                "Folder Size = {0:F3}MB (Actual result)",
                color,
                actualFolderSize / 1024 / (double)1024);
        }

        private static Folder GetSelectedSubFolder(string chosenPath, Task<Folder> crawTask)
        {
            Folder selectedFolder;
            if (chosenPath.Contains("/") || chosenPath.Contains("\\"))
            {
                selectedFolder = FindFolderByPath(chosenPath, crawTask.Result);
            }
            else
            {
                selectedFolder = FindFolder(chosenPath, crawTask.Result);
            }
            return selectedFolder;
        }

        private static Task<string> StartWaitingSubroutine(Task<Folder> crawTask)
        {
            Task<string> task = Task.Run(() =>
            {
                helper.ConsoleMio.WriteLine("Generating folder sturcture...", ConsoleColor.DarkCyan);
                helper.ConsoleMio.Write(
                    "In the mean time enter a directory name, to calculate size from\n" +
                    "when this task is completed: ", ConsoleColor.DarkCyan);

                string subDirectoryPath = helper.ConsoleMio.ReadInColor(ConsoleColor.DarkRed);

                while (!crawTask.IsCompleted)
                {
                    helper.ConsoleMio.Write("Task is: ", ConsoleColor.DarkCyan);
                    helper.ConsoleMio.WriteLine(crawTask.Status.ToString(), ConsoleColor.DarkRed);

                    helper.ConsoleMio.WriteLine("Press a key to check status", ConsoleColor.DarkCyan);
                    Console.ReadKey(true);
                }

                helper.ConsoleMio.WriteLine(
                    "Task Completed!",
                    crawTask.IsFaulted ? ConsoleColor.Red : ConsoleColor.DarkGreen);

                return subDirectoryPath;
            });

            return task;
        }

        private static Folder FindFolder(string name, Folder root)
        {
            Folder result = null;

            foreach (Folder child in root.ChildFolders)
            {
                if (child.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return child;
                }
                else
                {
                    result = FindFolder(name, child);
                    if (result != null)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private static Folder FindFolderByPath(string path, Folder root)
        {
            string[] splitPath = path.Split(new[] { '/', '\\' });
            Folder current = root;

            foreach (var name in splitPath)
            {
                current = current.ChildFolders
                    .FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (current == null)
                {
                    return current;
                }
            }

            return current;
        }

        private static string GetPathFromDirectory(Folder folder)
        {
            var sb = new StringBuilder();

            while (folder.Parent != null)
            {
                sb.Insert(0, "\\" + folder.Name);
                folder = folder.Parent;
            }

            return sb.ToString();
        }

        private static long GetDirSize(DirectoryInfo dir)
        {
            return dir.GetFiles().Sum(fi => fi.Length) +
                   dir.GetDirectories().Sum(di => GetDirSize(di));
        }
    }
}

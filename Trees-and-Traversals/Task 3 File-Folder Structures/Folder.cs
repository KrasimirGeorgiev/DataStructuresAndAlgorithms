namespace FileFolderStructures
{
    public class Folder
    {
        public Folder(string name)
        {
            this.Name = name;
            this.Files = new File[0];
            this.ChildFolders = new Folder[0];
        }

        public string Name { get; set; }

        public File[] Files { get; set; }

        public Folder[] ChildFolders { get; set; }

        public Folder Parent { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public long GetTotalSize()
        {
            long totalSize = 0;

            foreach (File file in this.Files)
            {
                totalSize += file.Size;
            }

            foreach (Folder subFolder in this.ChildFolders)
            {
                totalSize += subFolder.GetTotalSize();
            }

            return totalSize;
        }
    }
}
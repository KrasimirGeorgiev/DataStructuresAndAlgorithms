namespace FileFolderStructures
{
    public class File
    {
        public File(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public long Size { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
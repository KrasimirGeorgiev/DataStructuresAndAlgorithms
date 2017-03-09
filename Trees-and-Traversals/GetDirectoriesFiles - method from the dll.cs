public Dictionary<string, List<FileInfo>> GetDirectoryFiles(DirectoryInfo dirInfo)
{
	return dirInfo
	    .GetFiles()
	    .OrderBy(file => this.GetFileExtension(file.Name))
	    .ThenBy(file => file.Length)
	    .GroupBy(file => this.GetFileExtension(file.Name))
	    .OrderByDescending(group => group.Count())
	    .ToDictionary(group => group.Key, group => group.ToList());
}
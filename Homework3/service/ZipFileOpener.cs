using System.IO.Compression;

namespace Homework3.service;

public class ZipFileOpener
{
    string fullPathOfNewFolder = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data";
    private readonly object _locker = new object();

    public void OpenZipAsync(string filePath)
    {
        IfEndsWithDirectorySeparator(fullPathOfNewFolder);

        using (ZipArchive archive = ZipFile.OpenRead(filePath))
        {
            Parallel.ForEach(archive.Entries, (entry) =>
            {
                GettingNameOfTXT(entry);
                if (entry.Name.EndsWith(".txt"))
                {
                    string destinationPath = Path.GetFullPath(Path.Combine(fullPathOfNewFolder, entry.FullName));
                    if (destinationPath.StartsWith(fullPathOfNewFolder) && !File.Exists(destinationPath))
                    {
                        lock (_locker)
                        {
                            entry.ExtractToFile(destinationPath);
                        }
                    }
                        
                }
            });
        }
    }

    private string IfEndsWithDirectorySeparator(string path)
    {
        if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            return path += Path.DirectorySeparatorChar;
        return path;
    }

    private void GettingNameOfTXT(ZipArchiveEntry entry)
    {
        string nameOfFile = entry.FullName;
        
        if (nameOfFile.Contains(@"/") || nameOfFile.Contains(@"\\"))
        {
            string directorySeparator = @"/";
            if (nameOfFile.Contains(@"\\"))
                directorySeparator = @"\\";

            var splitted = nameOfFile.Split(directorySeparator);
            string fullPath = Path.Combine(fullPathOfNewFolder, splitted[0]);
            if(!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
        }
    }
}

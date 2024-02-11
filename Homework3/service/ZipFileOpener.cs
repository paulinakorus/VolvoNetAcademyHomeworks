using System.IO.Compression;

namespace Homework3.service;

public class ZipFileOpener
{
    string fullPathOfNewFolder = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data";

    public void OpenZip(string filePath)
    {
        IfEndsWithDirectorySeparator(fullPathOfNewFolder);

        using (ZipArchive archive = ZipFile.OpenRead(filePath))
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                //string nameOfTXT = GettingNameOfTXT(entry);
                GettingNameOfTXT(entry);
                if (entry.Name.EndsWith(".txt"))
                {
                    string destinationPath = Path.GetFullPath(Path.Combine(fullPathOfNewFolder, entry.FullName));
                    if (destinationPath.StartsWith(fullPathOfNewFolder) && !File.Exists(destinationPath))
                        entry.ExtractToFile(destinationPath);
                }
            }
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
        
        if (nameOfFile.Contains(@"/"))
        {
            int index = nameOfFile.IndexOf(@"/");
            string nameOfDirectory = nameOfFile.Substring(0, index);
            //string nameOfTXT = nameOfFile.Substring(index+1);
            string fullPath = Path.Combine(fullPathOfNewFolder, nameOfDirectory);
            if(!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            //return nameOfTXT;
        }
        //return "";
    }
}

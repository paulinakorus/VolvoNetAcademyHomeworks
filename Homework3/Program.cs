using Homework3.service;

namespace Homework3
{
    internal class Program
    {
        private const string FILE_TO_ZIP = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data\100 books.zip.zip";
        static void Main(string[] args)
        {
            ZipFileOpener zipOpener = new ZipFileOpener();
            zipOpener.OpenZip(FILE_TO_ZIP);
        }
    }
}
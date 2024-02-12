using Homework3.model;
using Homework3.service;

namespace Homework3
{
    internal class Program
    {
        private const string FILE_TO_ZIP = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data\100 books.zip.zip";
        static async Task Main(string[] args)
        {
            ZipFileOpener zipOpener = new ZipFileOpener();
            zipOpener.OpenZipAsync(FILE_TO_ZIP);

            try
            {
                DirectoryFolder directory = new DirectoryFolder();
                await directory.OpenDirectoryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //string filePath = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data\TextFile1.txt";
            //var text = await directory.GetDataFromFileAsync(filePath);

        }
    }
}
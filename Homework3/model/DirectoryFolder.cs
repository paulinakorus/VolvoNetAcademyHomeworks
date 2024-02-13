using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Homework3.model;

public class DirectoryFolder
{
    private string filePath = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data\100 books\";
    private readonly object _locker = new object();
    public async Task OpenDirectoryAsync()
    {
        if (Directory.Exists(filePath))
        {
            var files = Directory.GetFiles(filePath);
            var tasks = new List<Task>();

            /*Parallel.ForEach(files, (file) =>
            {
                var task = WorkingWithFile(file);
                tasks.Add(task);
            });
            await Task.WhenAll(tasks);*/

            string file_Path = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data\100 books\pg10007.txt";
            var task = WorkingWithFile(file_Path);
            await task;
        }
    }

    private async Task WorkingWithFile(string filePath)
    {
        //var text = await File.ReadAllTextAsync(filePath);
        var text = await GetDataFromFileAsync(filePath);
        Console.WriteLine(text);

        //string[] sentences = Regex.Split(text, @"(?<=[\.!\?])\s+");
    }

    private bool IfRemoveLine(string text, string[] wordsToRemove)
    {
        foreach (string word in wordsToRemove)
        {
            if (text.ToLower().Contains(word))
                return true;
        }
        return false;
    }

    public async Task<string> GetDataFromFileAsync(string path)
    {
        var list = new List<string>();
        bool foundStart = false;
        string startPattern = "START";
        string endPattern = "END";
        string seperationPattern = "***";
        string[] wordsToRemove = {"chapter"};

        using (var reader = new StreamReader(File.OpenRead(path)))
        {
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (!foundStart)
                {
                    if (line.Contains(startPattern) && line.Contains(seperationPattern))
                    {
                        foundStart = true;
                    }
                }
                else if ((line.Contains(endPattern) && line.Contains(seperationPattern)))
                {
                    break;
                }
                else if (IfRemoveLine(line, wordsToRemove))
                {
                    continue;
                }
                else
                {
                    list.Add(line);
                }
            }
            return list.Aggregate((current, line) => current += line);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

            var task = WorkingWithFile()
        }
    }

    private async Task WorkingWithFile(string filePath)
    {
        var text = await File.ReadAllTextAsync(filePath);
        Console.WriteLine(text);
    }

    /*public async Task<string> GetDataFromFileAsync(string path)
    {
        var list = new List<string>();
        using (var reader = new StreamReader(File.OpenRead(path)))
        {
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                list.Add(line);
            }
            return list.Aggregate((current, line) => current += line);
        }
    }*/
}
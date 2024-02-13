using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Homework3.model;

internal class DirectoryFolder
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
        //var text = await GetDataFromFileAsync(filePath);
        List<Paragraph> paragraphsList = await GetDataFromFileAsyncAndSplitToParagraphs(filePath);
        List<Sentence> allSentences = new List<Sentence>();

        foreach (Paragraph paragraph in paragraphsList)
        {
            if (paragraph != null)
            {
                paragraph.ParseToSentences();
                allSentences.AddRange(paragraph.SentencesList);
            }
        }
        List<Sentence> sentences = allSentences;

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
    private bool ContainsWords(string line)
    {
        return !string.IsNullOrWhiteSpace(line) && line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length > 0;
    }

    public async Task<List<Paragraph>> GetDataFromFileAsyncAndSplitToParagraphs(string path)
    {
        var paragraphsList = new List<Paragraph>();
        var list = new List<string>();
        bool foundStart = false;
        bool ifSentence = false;
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
                    if (!ContainsWords(line))
                    {
                        if (ifSentence)
                        {
                            string text = list.Aggregate((current, line) => current += line);
                            Paragraph paragraph = new Paragraph(text);
                            paragraphsList.Add(paragraph);
                            ifSentence = false;
                            list.Clear();
                        }
                        else
                        {
                            list.Clear();
                            ifSentence = false;
                        }
                    }
                    else
                    {
                        list.Add(line);
                        ifSentence = (line.EndsWith(".") || line.EndsWith("”"))? true : false;
                    }
                }
            }
            return paragraphsList;
        }
    }
}

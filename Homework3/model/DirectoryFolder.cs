using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;

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
                lock (_locker)
                {
                    tasks.Add(task);
                }
            });
            await Task.WhenAll(tasks);*/

            /*foreach (var file in files)
            {
                await WorkingWithFile(file);
                /*lock (_locker)
                {
                    tasks.Add(task);
                }*/
            //}
            //await Task.WhenAll(tasks);

            string file_Path = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data\100 books\pg10007.txt";
            var task = WorkingWithFile(file_Path);
            await task;
        }
    }

    private async Task WorkingWithFile(string filePath)
    {
        List<Paragraph> paragraphsList = GetDataFromFileAsyncAndSplitToParagraphs(filePath);
        FileTXT file = new FileTXT(paragraphsList);

        var longestSentenceByChar = file.LongestSentenceByChars();
        var shortestSentenceByWords = file.ShortestSentenceByWords();
        var longestWord = file.LongestWord();
        var mostCommonLetter = file.FindMostCommonLetter();
        var sortedWords = file.SortedWordsByOccurance();

        await Task.WhenAll(longestSentenceByChar, shortestSentenceByWords, longestWord, mostCommonLetter, sortedWords);
        
        List<string> resultList = new List<string>
        {
            longestSentenceByChar.Result,
            shortestSentenceByWords.Result,
            longestWord.Result,
            mostCommonLetter.Result,
            sortedWords.Result
        };
        await WriteToFileAsync(file, resultList);
    }

    private async Task WriteToFileAsync (FileTXT file, List<string> resultList)
    {
        var filePath = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data\results\" + file.Title + ".txt";
        using (StreamWriter input = new StreamWriter(filePath, false))
        {
            foreach (string line in resultList) 
            {
                input.WriteLineAsync(line);
            }
            input.Close();
        }
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

    public List<Paragraph> GetDataFromFileAsyncAndSplitToParagraphs(string path)
    {
        var paragraphsList = new List<Paragraph>();
        var list = new List<string>();
        bool foundStart = false;
        bool ifSentence = false;
        bool ifFoundTitle = false;
        string titlePattern = "Title: ";
        string startPattern = "START";
        string endPattern = "END";
        string seperationPattern = "***";
        string[] wordsToRemove = { "chapter", "volume" };

        using (var reader = new StreamReader(File.OpenRead(path)))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (!ifFoundTitle)
                {
                    if (line.Contains(titlePattern))
                    {
                        line = line.Replace(titlePattern, "");
                        Paragraph titleLine = new Paragraph(line);
                        paragraphsList.Add(titleLine);
                        ifFoundTitle = true;
                    }
                }
                else if (!foundStart)
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
                            string text = list.Aggregate((current, line) => current += (" " + line));
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
                        lock (_locker)
                        {
                            list.Add(line);
                        }
                        ifSentence = (line.EndsWith(".") || line.EndsWith("”")) ? true : false;
                    }
                }
            }
            return paragraphsList;
        }
    }
}
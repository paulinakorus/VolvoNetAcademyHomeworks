﻿using System;
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

            foreach (var file in files)
            {
                var task = WorkingWithFile(file);
                lock (_locker)
                {
                    tasks.Add(task);
                }
            }
            await Task.WhenAll(tasks);

            //string file_Path = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data\100 books\pg10007.txt";
            //var task = WorkingWithFile(file_Path);
            //await task;
            //string file_Path2 = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data\100 books\pg1080.txt";
            //var task2 = WorkingWithFile(file_Path2);
            //await task2;
        }
    }

    private async Task WorkingWithFile(string filePath)
    {
        List<Paragraph> paragraphsList = await GetDataFromFileAsyncAndSplitToParagraphs(filePath);
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
        
        /*using (StreamReader reader = new StreamReader(File.OpenRead(filePath)))   moving reader buffer in different place
        {
            string line = await reader.ReadLineAsync();
            Console.WriteLine(line);
        }*/
    }

    private async Task WriteToFileAsync (FileTXT file, List<string> resultList)
    {
        var filePath = @"C:\Users\pauko\Desktop\Studia\Kursy\Volvo NET Academy\Homework\Homework3\data\results\" + file.Title + ".txt";
        using (StreamWriter input = new StreamWriter(filePath, false))
        {
            foreach (string line in resultList) 
            {
                await input.WriteLineAsync(line).ConfigureAwait(false);;
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

    public async Task<List<Paragraph>> GetDataFromFileAsyncAndSplitToParagraphs(string path)                        // The next time it is played back, the previous output appears on the input
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
                var line = await reader.ReadLineAsync().ConfigureAwait(false); ;
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
                        list.Add(line);
                        ifSentence = (line.EndsWith(".") || line.EndsWith("”")) ? true : false;
                    }
                }
            }
            reader.Dispose();
            return paragraphsList;
        }
    }
}
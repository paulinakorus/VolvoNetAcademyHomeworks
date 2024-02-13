using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Homework3.model;

internal class FileTXT
{
    public string Title { get; set; }
    public List<Word> WordsList { get; set; }
    public List<Sentence> SentenceList { get; set; }
    public List<Paragraph> ParagraphList { get; set; }
    private readonly object _locker = new object();

    public FileTXT(List<Paragraph> paragraphList)
    {
        Title = paragraphList.First().Text;
        paragraphList.Remove(paragraphList.First());
        ParagraphList = paragraphList;
        SentenceList = new List<Sentence>();
        WordsList = new List<Word>();
        GettingSentences();
        GettingWords();
    }

    private void GettingSentences()
    {
        Parallel.ForEach(ParagraphList, (paragraph) =>
        {
            if (paragraph != null)
            {
                lock (_locker)
                {
                    SentenceList.AddRange(paragraph.SentencesList);
                }
            }
        });
    }

    private void GettingWords()
    {
        Parallel.ForEach(SentenceList, (sentence) =>
        {
            if (sentence != null)
            {
                lock (_locker)
                {
                    WordsList.AddRange(sentence.WordsList);
                }
            }
        });
    }

    public async Task<string> LongestSentenceByChars()
    {
        return SentenceList
            .AsParallel()
            .OrderByDescending(sentence => sentence.NumberOfCharacters)
            .First()
            .Text;
    }

    public async Task<string> ShortestSentenceByWords()
    {
        return SentenceList
            .AsParallel()
            .OrderBy(sentence => sentence.WordsList.Count)
            .First()
            .Text;
    }

    public async Task<string> LongestWord()
    {
        ConcurrentBag<Word> longestWordInSentenceList = new ConcurrentBag<Word>();

        /*Parallel.ForEach(SentenceList, (sentence) =>
        {
            var longestWord = sentence.WordsList
                .AsParallel()
                .OrderByDescending(word => (word).LettersNumber)
                .First();
            lock (_locker)
            {
                longestWordInSentenceList.Add(longestWord);
            }
        });*/
        
        foreach (Sentence sentence in SentenceList)
        {
            var longestWord = sentence.WordsList
                .AsParallel()
                .OrderByDescending(word => (word).LettersNumber)
                .First();

            longestWordInSentenceList.Add(longestWord);
        }

        return longestWordInSentenceList
                .AsParallel()
                .OrderByDescending(word => (word).LettersNumber)
                .First()
                .Text;
    }

    public async Task<string> FindMostCommonLetter()
    {
        IDictionary<char, int> countCharacters = CountCharacters();
        return countCharacters
            .AsParallel()
            .OrderByDescending(letter => letter.Value)
            .First().Key
            .ToString();
    }

    private ConcurrentDictionary<char, int> CountCharacters()
    {
        string[] textFromWords = WordsList
            .AsParallel()
            .Select(word => word.Text)
            .ToArray();

        string text = string.Join("", textFromWords);
        text = text.ToLower();

        IDictionary<char, int> countCharacters = new ConcurrentDictionary<char, int>();

        Parallel.ForEach(text, (mark) =>
        {
            if (mark != ' ' && IfLetter(mark))
            {
                lock (_locker)
                {
                    if (!countCharacters.ContainsKey(mark))
                        countCharacters.Add(mark, 1);
                    else
                        countCharacters[mark]++;
                }
            }
        });
        return (ConcurrentDictionary<char, int>)countCharacters;
    }

    private bool IfLetter(char character)
    {
        if (character >= 'a' && character <= 'z')
            return true;
        return false;
    }

    private ConcurrentDictionary<string, int> CountWords()
    {
        string[] textFromWords = WordsList
            .AsParallel()
            .Select(word => word.Text.ToLower())
            .ToArray();

        IDictionary<string, int> countWords = new ConcurrentDictionary<string, int>();

        Parallel.ForEach(textFromWords, (mark) =>
        {
            if (!mark.Equals(" "))
            {
                lock (_locker)
                {
                    if (!countWords.ContainsKey(mark))
                        countWords.Add(mark, 1);
                    else
                        countWords[mark]++;
                }
            }
        });
        return (ConcurrentDictionary<string, int>)countWords;
    }

    public async Task<string> SortedWordsByOccurance()
    {
        IDictionary<string, int> countedWords = CountWords();
        var list = countedWords
            .AsParallel()
            .OrderByDescending(letter => letter.Value)
            .Select(key => key.Key)
            .ToList();

        return list.Aggregate((current, line) => current += (" " + line));
    }
}
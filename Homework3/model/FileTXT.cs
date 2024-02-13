using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3.model;

internal class FileTXT
{
    public List<Sentence> SentenceList {  get; set; }
    public List<Paragraph> ParagraphList { get; set; }
    private readonly object _locker = new object();

    public FileTXT(List<Paragraph> paragraphList)
    {
        ParagraphList = paragraphList;
        SentenceList = new List<Sentence>();
        GettingSentences();
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

    public string LongestSentenceByChars()
    {
        return SentenceList
            .AsParallel()
            .OrderByDescending(sentence => sentence.NumberOfCharacters)
            .First()
            .Text;
    }

    public string ShortestSentenceByWords()
    {
        return SentenceList
            .AsParallel()
            .OrderBy(sentence => sentence.WordsList.Count)
            .First()
            .Text;
    }

    public string LongestWord()
    {
        List<Word> longestWordInSentenceList = new List<Word>();
        Parallel.ForEach(SentenceList, (sentence) =>
        {
            var longestWord = sentence.WordsList
                .AsParallel()
                .OrderByDescending(word => (word).LettersNumber)
                .First();

            lock (_locker)
            {
                longestWordInSentenceList.Add(longestWord);
            }
        });

        return longestWordInSentenceList
                .AsParallel()
                .OrderByDescending(word => (word).LettersNumber)
                .First()
                .Text;
    }
}

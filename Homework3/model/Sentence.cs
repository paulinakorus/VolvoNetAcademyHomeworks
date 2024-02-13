using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework3.model;

internal class Sentence
{
    public string Text { get; set; }
    public List<Word> WordsList { get; set; }
    public List<Punctation> PunctationList { get; set; }
    private readonly object _locker = new object();

    public Sentence(string text) 
    { 
        Text = text;
        WordsList = new List<Word>();
        PunctationList = new List<Punctation>();
        ParseToPunctationsAndWords();
    }

    private void ParseToPunctationsAndWords()
    {
        string patternForPunctations = @"[\p{P}\p{S}]";
        string[] punctations = Regex.Matches(Text, patternForPunctations)
            .OfType<Match>()
            .Select(match => match.Value)
            .ToArray();

        Parallel.ForEach(punctations, punctation =>
        {
            Punctation punctationClass = new Punctation(punctation);
            lock (_locker)
            {
                PunctationList.Add(punctationClass);
            }
        });

        Text = Regex.Replace(Text, patternForPunctations, "");

        string patternForWords = @"\s+";
        string[] words = Regex.Split(Text, patternForWords);

        Parallel.ForEach(words, word =>
        {
            word.Trim();
            Word wordClass = new Word(word);
            lock (_locker)
            {
                WordsList.Add(wordClass);
            }
        });
    }
}

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
    public int NumberOfCharacters { get; set; }
    private readonly object _locker = new object();

    public Sentence(string text) 
    {
        Text = text;
        WordsList = new List<Word>();
        PunctationList = new List<Punctation>();
        ParseToPunctationsAndWords();
        GettingNumberOfCharacters();
    }

    private void ParseToPunctationsAndWords()
    {
        var textToWorkOn = Text;
        string patternForPunctations = @"[\p{P}\p{S}]";
        string[] punctations = Regex.Matches(textToWorkOn, patternForPunctations)
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

        textToWorkOn = Regex.Replace(textToWorkOn, patternForPunctations, "");

        string patternForWords = @"\s+";
        string[] words = Regex.Split(textToWorkOn, patternForWords);

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

    private void GettingNumberOfCharacters()
    {
        int numberOfCharacters = 0;
        Parallel.ForEach(WordsList, (word) =>
        {
            lock (_locker)
            {
                numberOfCharacters += word.LettersNumber;
            }
        });

        Parallel.ForEach(PunctationList, (punctation) =>
        {
            lock (_locker)
            {
                numberOfCharacters += punctation.Text.Length;
            }
        });
        NumberOfCharacters = numberOfCharacters;
    }
}

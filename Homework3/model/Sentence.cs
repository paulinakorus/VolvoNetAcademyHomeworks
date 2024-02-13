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

    public Sentence(string text) 
    { 
        Text = text;
        WordsList = new List<Word>();
        ParseToWords();
    }

    private void ParseToWords()
    {
        string pattern = @"\s+";
        string[] words = Regex.Split(Text, pattern);

        Parallel.ForEach(words, word =>
        {
            word.Trim();
            Word wordClass = new Word(word);
            WordsList.Add(wordClass);
        });
    }
}

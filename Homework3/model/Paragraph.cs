using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Homework3.model;

internal class Paragraph
{
    public string Text { get; set; }
    public List<Sentence> SentencesList { get; set; }
    private readonly object _locker = new object();

    public Paragraph(string text) 
    {
        Text = text;
        SentencesList = new List<Sentence>();
        ParseToSentences();
    }

    private void ParseToSentences()
    {
        //string[] wordsToSkip = { "Dr.", "Mr.", "Ms."};
        string pattern = @"(?<=[\.!\?])\s+";
        string[] sentences = Regex.Split(Text, pattern);

        /*
        string text = Text;
        var sentences = new List<string>();
        string pattern = "(?<!mr|mrs|ms|dr|prof)[.!?]\\s*";
        var match = Regex.Match(text, pattern);
        int prevIndex = 0;

        while (match.Success)
        {
            var index = match.Index;
            var sentence = text.Substring(prevIndex, index - prevIndex + 1);
            prevIndex = index + 1;
            sentences.Add(sentence.Trim());
            match = match.NextMatch();
        }
        */

        Parallel.ForEach(sentences, sentence =>
        {
            sentence.Trim();
            Sentence sentenceClass = new Sentence(sentence);
            lock (_locker)
            {
                SentencesList.Add(sentenceClass);
            }
        });
    }
}

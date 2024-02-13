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
        Parallel.ForEach(sentences, sentence =>
        {
            sentence.Trim();
            Sentence sentenceClass = new Sentence(sentence);
            SentencesList.Add(sentenceClass);
        });
    }
}

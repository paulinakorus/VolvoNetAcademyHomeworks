using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3.model;

internal class Paragraph
{
    public string Text { get; set; }
    public List<Sentence> SentencesList { get; set; }

    public Paragraph(string text) 
    {
        Text = text;
        SentencesList = new List<Sentence>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3.model;

internal class Word
{
    public string Text { get; set; }
    public int LettersNumber { get; set; }

    public Word(string text) 
    { 
        Text = text;
        LettersNumber = 0;
    }
}

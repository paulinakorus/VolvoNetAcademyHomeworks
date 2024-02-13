﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }

}

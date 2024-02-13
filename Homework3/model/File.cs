using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3.model;

internal class File
{
    public List<Sentence> SentenceList {  get; set; }
    public List<Paragraph> ParagraphList { get; set; }

    public File(List<Paragraph> paragraphList, List<Sentence> sentenceList)
    {
        ParagraphList = paragraphList;
        SentenceList = sentenceList;
    }
}

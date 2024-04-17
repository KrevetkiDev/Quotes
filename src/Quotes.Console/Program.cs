using System.Xml;
using Quotes;

TaskOne();

void TaskOne()
{
    string path = "./Assets/quotes.txt";
    string newPath = "./Assets/quotesNew.txt";
    List<Bar> bars = Parser.Parse(path);
}   
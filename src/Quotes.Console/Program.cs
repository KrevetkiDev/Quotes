using System.Globalization;
using System.Text;
using System.Xml;
using Quotes;

TaskOne();

void TaskOne()
{
    string path = "./Assets/quotes.txt";
    string newPath = "./quotesNew.txt";
    List<Bar> bars = Parser.ParseBarsFromFile(path);
    Dictionary<DateOnly, List<Bar>> groupedByData = QuoteHelper.GroupByData(bars);
    List<DayRange> dayRanges = new List<DayRange>();
    List<string> lines = new List<string>();
       dayRanges = QuoteHelper.GetDayRange(groupedByData);
       foreach (var variablDayRange in dayRanges)
       {
           var line = variablDayRange.ToString();
           lines.Add(line);
       }
       File.WriteAllLines(newPath, lines);
}



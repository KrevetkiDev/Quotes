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
    QuoteHelper.NewLineFormation(groupedByData);
    DayRange dayRange = new DayRange();
    File.WriteAllLines(newPath, dayRange.ListOfDayValues);
}



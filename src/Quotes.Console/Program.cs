using Quotes;

TaskOne();

void TaskOne()
{
    string path = "./Assets/quotes.txt";
    string newPath = "./quotesNew.txt";

    List<Bar> bars = Parser.ParseBarsFromFile(path);
    Dictionary<DateOnly, List<Bar>> groupedByData = QuoteHelper.GroupByData(bars);

    List<DayRange> dayRanges = QuoteHelper.GetDayRange(groupedByData);

    List<string> lines = new List<string>();
    foreach (var dayRange in dayRanges)
    {
        var line = dayRange.ToString();
        lines.Add(line);
    }

    File.WriteAllLines(newPath, lines);
}
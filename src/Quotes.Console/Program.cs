using Quotes;

TaskTwo();

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

void TaskOneWithLinq()
{
    string path = "./Assets/quotes.txt";
    string newPath = "./quotesNew.txt";

    var bars = Parser.ParseBarsFromFileWithLinq(path);
    var groupedByData = bars.GroupBy(bar => bar.Date);
    var dayRanges = QuoteHelper.GetDayRangeWithLinq(groupedByData);
    File.WriteAllLines(newPath, dayRanges.Select(dayRange => dayRange.ToString()));
}

void TaskTwo()
{
    string path = "./Assets/quotes.txt";
    string newPath = "./quotesNew.txt";

    List<Bar> bars = Parser.ParseBarsFromFile(path);
    Dictionary<DateTime, List<Bar>> groupedByHour = QuoteHelper.GroupByTime(bars);

    List<Bar> hourRanges = QuoteHelper.GetHourRange(groupedByHour);

    List<string> lines = new List<string>();
    foreach (var hourRange in hourRanges)
    {
        var line = hourRange.ToString();
        lines.Add(line);
    }

    File.WriteAllLines(newPath, lines);
}
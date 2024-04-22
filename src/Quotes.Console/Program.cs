using Quotes;

TaskThree();

void TaskOne()
{
    string path = "./Assets/quotes.txt";
    string newPath = "./quotesTaskOne.txt";

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
    string newPath = "./quotesTaskOneWitnLinq.txt";

    var bars = Parser.ParseBarsFromFileWithLinq(path);
    var groupedByData = bars.GroupBy(bar => bar.Date);
    var dayRanges = QuoteHelper.GetDayRangeWithLinq(groupedByData);
    File.WriteAllLines(newPath, dayRanges.Select(dayRange => dayRange.ToString()));
}

void TaskTwo()
{
    string path = "./Assets/quotes.txt";
    string newPath = "./quotesTaskTwo.txt";

    List<Bar> bars = Parser.ParseBarsFromFile(path);
    Dictionary<DateTime, List<Bar>> groupedByHour = QuoteHelper.GroupByHour(bars);

    List<Bar> hourRanges = QuoteHelper.GetHourBars(groupedByHour);

    List<string> lines = new List<string>();
    foreach (var hourRange in hourRanges)
    {
        var line = hourRange.ToString();
        lines.Add(line);
    }

    File.WriteAllLines(newPath, lines);
}

void TaskTwoWithLinq()
{
    string path = "./Assets/quotes.txt";
    string newPath = "./quotesTaskTwoWithLinq.txt";

    var bars = Parser.ParseBarsFromFileWithLinq(path);
    var groupedByHour =
        bars.GroupBy(bar => new DateTime(bar.Date.Year, bar.Date.Month, bar.Date.Day, bar.Time.Hour, 0, 0));
    var hourRanges = QuoteHelper.GetHourBarsWithLinq(groupedByHour);
    File.WriteAllLines(newPath, hourRanges.Select(hourRange => hourRange.ToString()));
}

void TaskThree()
{
    var file1 = File.ReadLines("./Assets/File1.txt");
    var file2 = File.ReadLines("./Assets/File2.txt");
    File.WriteAllLines("./NewLines.txt", file2.Except(file1));
}
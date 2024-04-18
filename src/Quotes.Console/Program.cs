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
    Dictionary<DateOnly, List<Bar>> groupedByData = GroupByData(bars);
    List<string> strings = new();
    

    foreach (var group in groupedByData)
    {
        string symbol = group.Value[0].Symbol;
        string description = group.Value[0].Description;
        string date = group.Value[0].Date.ToString();
        decimal high = 0;
        decimal low = decimal.MaxValue;
        foreach (var bar in group.Value)
        {
            if (high < bar.High)
            {
                high = bar.High;
            }

            if (low > bar.Low)
            {
                low = bar.Low;
            }
        }
        var line = $"{symbol},{description},{date},{high.ToString(CultureInfo.InvariantCulture)},{low.ToString(CultureInfo.InvariantCulture)}";
        strings.Add(line);
    }
    File.WriteAllLines(newPath, strings);
}



Dictionary<DateOnly, List<Bar>> GroupByData(List<Bar> bars)
{
    Dictionary<DateOnly, List<Bar>> groupedByDate = new Dictionary<DateOnly, List<Bar>>();
    for (int i = 0; i < bars.Count; i++)
    {
        if (groupedByDate.ContainsKey(bars[i].Date))
        {
            groupedByDate[bars[i].Date].Add(bars[i]);
        }
        else
        {
            groupedByDate.Add(bars[i].Date, new List<Bar>());
            groupedByDate[bars[i].Date].Add(bars[i]);
        }
    }

    return groupedByDate;
}
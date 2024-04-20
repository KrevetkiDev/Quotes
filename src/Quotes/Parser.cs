using System.Globalization;

namespace Quotes;

public static class Parser
{
    public static List<Bar> ParseBarsFromFile(string path)
    {
        string[] lines = File.ReadAllLines(path);
        bool isFirstLine = true;
        List<Bar> bars = [];

        foreach (var line in lines)
        {
            if (isFirstLine)
            {
                isFirstLine = false;
                continue;
            }

            Bar bar = ParseBar(line);
            bars.Add(bar);
        }

        return bars;
    }

    public static IEnumerable<Bar> ParseBarsFromFileWithLinq(string path)
    {
        return File.ReadLines(path)
            .Skip(1)
            .Select(ParseBar);
    }

    public static Bar ParseBar(string line)
    {
        string[] words = line.Split(',');
        Bar bar = new Bar
        {
            Symbol = words[0],
            Description = words[1],
            Date = DateOnly.Parse(words[2]),
            Time = TimeOnly.Parse(words[3]),
            Open = decimal.Parse(words[4], CultureInfo.InvariantCulture),
            High = decimal.Parse(words[5], CultureInfo.InvariantCulture),
            Low = decimal.Parse(words[6], CultureInfo.InvariantCulture),
            Close = decimal.Parse(words[7], CultureInfo.InvariantCulture),
            TotalVolume = int.Parse(words[8])
        };

        return bar;
    }
}
using System.Globalization;

namespace Quotes;

public class Parser
{
    public static List<Bar> ParseBarsFromFile(string path)
    {
        List<string> lines = File.ReadAllLines(path).ToList();
        bool isFirstLine = true;
        List<Bar> bars = [];
        Bar bar = new Bar();

        foreach (var line in lines)
        {
            if (isFirstLine)
            {
                isFirstLine = false;
                continue;
            }

            bar = ParseBar(line);
            

            bars.Add(bar);
        }
        
        return bars;
    }

    public static Bar ParseBar(string line)
    {
        string[] words = line.Split([',']);
        Bar bar = new Bar();
        bar.Symbol = words[0];
        bar.Description = words[1];
        bar.Date = DateOnly.Parse(words[2]);
        bar.Time = TimeOnly.Parse(words[3]);
        bar.Open = decimal.Parse(words[4], CultureInfo.InvariantCulture);
        bar.High = decimal.Parse(words[5], CultureInfo.InvariantCulture);
        bar.Low = decimal.Parse(words[6], CultureInfo.InvariantCulture);
        bar.Close = decimal.Parse(words[7], CultureInfo.InvariantCulture);
        bar.TotalVolume = int.Parse(words[8]);

        return bar;
    }
}
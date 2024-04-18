namespace Quotes;

public class Parser
{
    public static List<Bar> Parse(string path)
    {
        List<string> lines = File.ReadAllLines(path).ToList();
        List<Bar> bars = [];

        foreach (var line in lines.Skip(1))
        {
            string[] words = line.Split([',']);
            Bar bar = new Bar();
            bar.Symbol = words[0];
            bar.Description = words[1];
            bar.Date = DateOnly.Parse(words[2]);
            bar.Time = TimeOnly.Parse(words[3]);
            bar.Open = double.Parse(words[4]);
            bar.High = double.Parse(words[5]);
            bar.Low = double.Parse(words[6]);
            bar.Close = double.Parse(words[7]);
            bar.TotalVolume = int.Parse(words[8]);

            bars.Add(bar);
        }

        return bars;
    }
}
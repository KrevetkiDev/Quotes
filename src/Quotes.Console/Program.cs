using System.Xml;
using Quotes;

TaskOne();

void TaskOne()
{
    string path = "./Assets/quotes.txt";
    string newPath = "./Assets/quotesNew.txt";
    List<Bar> bars = Parser.Parse(path);
    List<double> highValues = new List<double>();
    
    Dictionary<DateOnly, List<Bar>> dataCollection = new Dictionary<DateOnly, List<Bar>>();
    for (int i = 0; i < bars.Count; i++)
    {
        if (dataCollection.ContainsKey(bars[i].Date))
        {
            dataCollection[bars[i].Date].Add(bars[i]);
        }
        else
        {
            dataCollection.Add(bars[i].Date, new List<Bar>());
            dataCollection[bars[i].Date].Add(bars[i]);
        }
    }

    foreach (var item in dataCollection)
    {
        var i = item.Value;
        foreach (var bar in i)
        {
            double x = 0;
            if (x < bar.High)
            {
                x = bar.High;
            }
            else
            {
                x = x;
            }
            highValues.Add(x);
        }
    }

    foreach (var values in highValues)
    {
        Console.WriteLine(values);
    }
}   
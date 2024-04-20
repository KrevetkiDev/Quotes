using System.Globalization;

namespace Quotes;

public static class QuoteHelper
{
    
   public static Dictionary<DateOnly, List<Bar>> GroupByData(List<Bar> bars)
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

    public static void NewLineFormation(Dictionary<DateOnly, List<Bar>> groupedByData)
    {
        foreach (var group in groupedByData)
        {
            DayRange dayRange = new DayRange();
            dayRange.symbol = group.Value[0].Symbol;
            dayRange.description = group.Value[0].Description;
            dayRange.date = group.Value[0].Date.ToString();
            dayRange.high = 0;
            dayRange.low = decimal.MaxValue;
            foreach (var bar in group.Value)
            {
                if (dayRange.high < bar.High)
                {
                    dayRange.high = bar.High;
                }

                if (dayRange.low > bar.Low)
                {
                    dayRange.low = bar.Low;
                }
            }
            var line = $"{dayRange.symbol},{dayRange.description},{dayRange.date},{dayRange.high.ToString(CultureInfo.InvariantCulture)},{dayRange.low.ToString(CultureInfo.InvariantCulture)}";
            dayRange.ListOfDayValues.Add(line);
        }
    }
}
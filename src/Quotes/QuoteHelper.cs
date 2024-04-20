using System.Collections.ObjectModel;
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

    public static List<DayRange> GetDayRange(Dictionary<DateOnly, List<Bar>> groupedByData)
    {
        List <DayRange> dayRanges = new List<DayRange>();
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
            dayRanges.Add(dayRange);
        }
        return dayRanges;
    }
}
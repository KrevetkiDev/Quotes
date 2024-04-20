namespace Quotes;

public static class QuoteHelper
{
    public static Dictionary<DateOnly, List<Bar>> GroupByData(List<Bar> bars)
    {
        Dictionary<DateOnly, List<Bar>> groupedByDate = new Dictionary<DateOnly, List<Bar>>();
        foreach (var bar in bars)
        {
            if (groupedByDate.ContainsKey(bar.Date))
            {
                groupedByDate[bar.Date].Add(bar);
            }
            else
            {
                groupedByDate.Add(bar.Date, new List<Bar>());
                groupedByDate[bar.Date].Add(bar);
            }
        }

        return groupedByDate;
    }

    public static List<DayRange> GetDayRange(Dictionary<DateOnly, List<Bar>> groupedByData)
    {
        List<DayRange> dayRanges = new List<DayRange>();
        foreach (var group in groupedByData)
        {
            DayRange dayRange = new DayRange
            {
                Symbol = group.Value[0].Symbol,
                Description = group.Value[0].Description,
                Date = group.Value[0].Date,
                High = 0,
                Low = decimal.MaxValue
            };

            foreach (var bar in group.Value)
            {
                if (dayRange.High < bar.High)
                {
                    dayRange.High = bar.High;
                }

                if (dayRange.Low > bar.Low)
                {
                    dayRange.Low = bar.Low;
                }
            }

            dayRanges.Add(dayRange);
        }

        return dayRanges;
    }
}
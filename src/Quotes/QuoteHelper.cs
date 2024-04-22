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

    public static IEnumerable<DayRange> GetDayRangeWithLinq(IEnumerable<IGrouping<DateOnly, Bar>> groupedByData)
    {
        var dayRanges = groupedByData.Select(group => new DayRange
        {
            Symbol = group.First().Symbol,
            Description = group.First().Description,
            Date = group.First().Date,
            High = group.Max(bar => bar.High),
            Low = group.Min(bar => bar.Low)
        });

        return dayRanges;
    }

    public static Dictionary<DateTime, List<Bar>> GroupByHour(List<Bar> bars)
    {
        Dictionary<DateTime, List<Bar>> groupedByHour = new Dictionary<DateTime, List<Bar>>();
        foreach (var bar in bars)
        {
            var datetime = new DateTime(bar.Date.Year, bar.Date.Month, bar.Date.Day, bar.Time.Hour, 0, 0);
            if (groupedByHour.ContainsKey(datetime))
            {
                groupedByHour[datetime].Add(bar);
            }
            else
            {
                groupedByHour.Add(datetime, new List<Bar>());
                groupedByHour[datetime].Add(bar);
            }
        }

        return groupedByHour;
    }

    public static List<Bar> GetHourBars(Dictionary<DateTime, List<Bar>> groupedByHour)
    {
        List<Bar> hourBars = new List<Bar>();
        foreach (var group in groupedByHour)
        {
            Bar hourBar = new Bar
            {
                Symbol = group.Value[0].Symbol,
                Description = group.Value[0].Description,
                Date = DateOnly.FromDateTime(group.Key),
                Time = TimeOnly.FromDateTime(group.Key),
                Open = group.Value[0].Open,
                High = 0,
                Low = decimal.MaxValue
            };

            foreach (var bar in group.Value)
            {
                if (hourBar.High < bar.High)
                {
                    hourBar.High = bar.High;
                }

                if (hourBar.Low > bar.Low)
                {
                    hourBar.Low = bar.Low;
                }

                hourBar.TotalVolume += bar.TotalVolume;
            }

            hourBar.Close = hourBar.Close = group.Value.Count > 0 ? group.Value[group.Value.Count - 1].Close : 0;
            hourBars.Add(hourBar);
        }

        return hourBars;
    }
}
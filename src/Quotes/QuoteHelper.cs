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

    public static Dictionary<DateTime, List<Bar>> GroupByTime(List<Bar> bars)
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

    public static List<Bar> GetHourRange(Dictionary<DateTime, List<Bar>> groupedByHour)
    {
        List<Bar> hourRanges = new List<Bar>();
        foreach (var group in groupedByHour)
        {
            Bar hourRange = new Bar
            {
                Symbol = group.Value[0].Symbol,
                Description = group.Value[0].Description,
                Date = group.Value[0].Date,
                Time = group.Value[0].Time,
                Open = group.Value[0].Open
            };

            foreach (var bar in group.Value)
            {
                if (hourRange.High < bar.High)
                {
                    hourRange.High = bar.High;
                }

                if (hourRange.Low > bar.Low)
                {
                    hourRange.Low = bar.Low;
                }

                hourRange.TotalVolume += bar.TotalVolume;
            }

            var checkLenghtList = hourRanges.Count;
            var closedValue = checkLenghtList;
            hourRange.Close = closedValue;
            hourRanges.Add(hourRange);
        }

        return hourRanges;
    }
}
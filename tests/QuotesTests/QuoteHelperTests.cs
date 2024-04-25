using FluentAssertions;
using Quotes;

namespace QuotesTests;

public class QuoteHelperTests
{
    [Fact]
    public void GroupByData_ShouldReturnSingleGroup_WhenSameDate()
    {
        // Arrange
        var date = new DateOnly(2020, 1, 02);
        var bars = new List<Bar>
        {
            new()
            {
                Date = date
            },
            new()
            {
                Date = date
            }
        };

        // Act
        var result = QuoteHelper.GroupByData(bars);

        // Assert
        result.Should().HaveCount(1);
        result[date].Should().BeEquivalentTo(bars);
    }

    [Fact]
    public void GroupByData_ShouldReturnTwoGroup_WhenDifferentDate()
    {
        // Arrange
        var date1 = new DateOnly(2020, 1, 02);
        var date2 = new DateOnly(2020, 1, 03);
        var bar1 = new Bar
        {
            Date = date1
        };
        var bar2 = new Bar
        {
            Date = date2
        };
        var bars = new List<Bar> { bar1, bar2 };

        // Act
        var result = QuoteHelper.GroupByData(bars);

        // Assert
        result.Should().HaveCount(2);
        result[date1].Single().Should().Be(bar1);
        result[date2].Single().Should().Be(bar2);
    }

    [Fact]
    public void GetDayRange_ShouldReturnDayRange_WhenSameDay()
    {
        // Arrange
        var date = new DateOnly(2020, 1, 02);
        var symbol = "ABBV";
        var description = "NYSE";
        var dict = new Dictionary<DateOnly, List<Bar>>
        {
            {
                date, [
                    new()
                    {
                        Symbol = symbol,
                        Description = description,
                        Date = date,
                        High = 89.095m,
                        Low = 88.950m
                    },
                    new()
                    {
                        Symbol = symbol,
                        Description = description,
                        Date = date,
                        High = 89.090m,
                        Low = 88.950m
                    }
                ]
            }
        };

        var dayRanges = new List<DayRange>
        {
            new()
            {
                Symbol = symbol,
                Description = description,
                Date = date,
                High = 89.095m,
                Low = 88.950m
            }
        };

        // Act
        var resultGetDayRange = QuoteHelper.GetDayRange(dict);

        // Assert
        resultGetDayRange.Should().BeEquivalentTo(dayRanges);
    }

    [Fact]
    public void GetDayRangeWithLinq_ShouldReturnDayRange_WhenSameDay()
    {
        // Arrange
        var symbol = "ABBV";
        var description = "NYSE";
        var date = new DateOnly(2020, 1, 02);

        var bars = new List<Bar>
        {
            new()
            {
                Symbol = symbol,
                Description = description,
                Date = date,
                High = 89.095m,
                Low = 88.950m
            },
            new()
            {
                Symbol = symbol,
                Description = description,
                Date = date,
                High = 89.090m,
                Low = 88.950m
            }
        }.GroupBy(x => x.Date);

        var dayRanges = new List<DayRange>
        {
            new()
            {
                Symbol = symbol,
                Description = description,
                Date = date,
                High = 89.095m,
                Low = 88.950m
            }
        };

        // Act
        var resultGetDayRange = QuoteHelper.GetDayRangeWithLinq(bars);

        // Assert
        resultGetDayRange.Should().BeEquivalentTo(dayRanges);
    }

    [Fact]
    public void GroupByHour_ShouldReturnSingleGroup_WhenSameHour()
    {
        // Arrange
        var date = new DateOnly(2020, 1, 02);
        var hour = 8;
        var datetime = date.ToDateTime(new TimeOnly(hour, 0));
        var bars = new List<Bar>
        {
            new()
            {
                Date = date,
                Time = new TimeOnly(hour, 3, 0)
            },
            new()
            {
                Date = date,
                Time = new TimeOnly(hour, 1, 0)
            }
        };

        // Act
        var result = QuoteHelper.GroupByHour(bars);

        // Assert
        result.Should().HaveCount(1);
        result[datetime].Should().BeEquivalentTo(bars);
    }

    [Fact]
    public void GroupByHour_ShouldReturnTwoGroup_WhenDifferentHour()
    {
        // Arrange
        var date = new DateOnly(2020, 1, 02);
        var hour1 = 8;
        var hour2 = 9;
        var datetime1 = date.ToDateTime(new TimeOnly(hour1, 0));
        var datetime2 = date.ToDateTime(new TimeOnly(hour2, 0));
        var bar1 = new Bar
        {
            Date = date,
            Time = new TimeOnly(hour1, 3, 0)
        };
        var bar2 = new Bar
        {
            Date = new DateOnly(2020, 1, 02),
            Time = new TimeOnly(hour2, 1, 0)
        };
        var bars = new List<Bar> { bar1, bar2 };

        // Act
        var result = QuoteHelper.GroupByHour(bars);

        // Assert
        result.Should().HaveCount(2);
        result[datetime1].Single().Should().Be(bar1);
        result[datetime2].Single().Should().Be(bar2);
    }

    [Fact]
    public void GetHourBar_ShouldReturnHourBars_WhenSameDate()
    {
        // Arrange
        var symbol = "ABBV";
        var description = "NYSE";
        var date = new DateTime(2020, 1, 02);
        var dict = new Dictionary<DateTime, List<Bar>>
        {
            {
                date, [
                    new()
                    {
                        Symbol = symbol,
                        Description = description,
                        Date = DateOnly.FromDateTime(date),
                        Time = TimeOnly.FromDateTime(date),
                        Open = 89.095m,
                        High = 89.095m,
                        Low = 88.950m,
                        Close = 88.950m,
                        TotalVolume = 1325
                    },
                    new()
                    {
                        Symbol = symbol,
                        Description = description,
                        Date = DateOnly.FromDateTime(date),
                        Time = TimeOnly.FromDateTime(date).AddMinutes(1),
                        Open = 89.090m,
                        High = 89.090m,
                        Low = 88.950m,
                        Close = 88.950m,
                        TotalVolume = 1325
                    }
                ]
            }
        };

        var hoursBars = new List<Bar>
        {
            new()
            {
                Symbol = symbol,
                Description = description,
                Date = DateOnly.FromDateTime(date),
                Time = TimeOnly.FromDateTime(date),
                Open = 89.095m,
                High = 89.095m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 2650
            }
        };

        // Act
        var result = QuoteHelper.GetHourBars(dict);

        // Assert
        result.Should().BeEquivalentTo(hoursBars);
    }

    [Fact]
    public void GetHourBarWithLinq_ShouldReturnHourBars_WhenSameDate()
    {
        // Arrange
        var symbol = "ABBV";
        var description = "NYSE";
        var date = new DateTime(2020, 1, 02);
        var bars = new List<Bar>
            {
                new()
                {
                    Symbol = symbol,
                    Description = description,
                    Date = DateOnly.FromDateTime(date),
                    Time = TimeOnly.FromDateTime(date),
                    Open = 89.095m,
                    High = 89.095m,
                    Low = 88.950m,
                    Close = 88.950m,
                    TotalVolume = 1325
                },
                new()
                {
                    Symbol = symbol,
                    Description = description,
                    Date = DateOnly.FromDateTime(date),
                    Time = TimeOnly.FromDateTime(date).AddMinutes(1),
                    Open = 89.090m,
                    High = 89.090m,
                    Low = 88.950m,
                    Close = 88.950m,
                    TotalVolume = 1325
                }
            }
            .GroupBy(bar => new DateTime(bar.Date.Year, bar.Date.Month, bar.Date.Day, bar.Time.Hour, 0, 0));

        var hoursBars = new List<Bar>
        {
            new()
            {
                Symbol = symbol,
                Description = description,
                Date = DateOnly.FromDateTime(date),
                Time = TimeOnly.FromDateTime(date),
                Open = 89.095m,
                High = 89.095m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 2650
            }
        };

        // Act
        var result = QuoteHelper.GetHourBarsWithLinq(bars);

        // Assert
        result.Should().BeEquivalentTo(hoursBars);
    }
}
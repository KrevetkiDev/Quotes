using FluentAssertions;
using Quotes;

namespace QuotesTests;

public class QuoteHelperTests
{
    [Fact]
    public void GroupByData_ShouldCorrectGroupWithSameData()
    {
        // Arrange
        var bars = new List<Bar>()
        {
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(8, 3, 0),
                Open = 89.095m,
                High = 89.095m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            },
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(8, 1, 0),
                Open = 89.090m,
                High = 89.090m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            }
        };

        // Act
        var result = QuoteHelper.GroupByData(bars);

        // Assert
        Assert.Equal(2, result.Single().Value.Count);
    }

    [Fact]
    public void GroupByData_ShouldCorrectGroupWithDifferentData()
    {
        // Arrange
        var bars = new List<Bar>()
        {
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(8, 3, 0),
                Open = 89.095m,
                High = 89.095m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            },
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 03),
                Time = new TimeOnly(8, 1, 0),
                Open = 89.090m,
                High = 89.090m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            }
        };

        // Act
        var result = QuoteHelper.GroupByData(bars);

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetDayRange_ShouldReturnCorrectDayRanges()
    {
        // Arrange
        var dict = new Dictionary<DateOnly, List<Bar>>()
        {
            {
                new DateOnly(2020, 1, 02), [
                    new()
                    {
                        Symbol = "ABBV",
                        Description = "NYSE",
                        Date = new DateOnly(2020, 1, 02),
                        Time = new TimeOnly(8, 3, 0),
                        Open = 89.095m,
                        High = 89.095m,
                        Low = 88.950m,
                        Close = 88.950m,
                        TotalVolume = 1325
                    },

                    new()
                    {
                        Symbol = "ABBV",
                        Description = "NYSE",
                        Date = new DateOnly(2020, 1, 02),
                        Time = new TimeOnly(8, 1, 0),
                        Open = 89.090m,
                        High = 89.090m,
                        Low = 88.950m,
                        Close = 88.950m,
                        TotalVolume = 1325
                    }
                ]
            }
        };

        var dayRanges = new List<DayRange>()
        {
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                High = 89.095m,
                Low = 88.950m,
            }
        };

        // Act
        var resultGetDayRange = QuoteHelper.GetDayRange(dict);


        // Assert
        resultGetDayRange.Should().BeEquivalentTo(dayRanges);
    }

    [Fact]
    public void GetDayRangeWithLinq_ShouldReturnCorrectDayRanges()
    {
        // Arrange
        var bars = new List<Bar>()
        {
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(8, 3, 0),
                Open = 89.095m,
                High = 89.095m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            },
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(8, 1, 0),
                Open = 89.090m,
                High = 89.090m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            }
        }.GroupBy(x => x.Date);

        var dayRanges = new List<DayRange>()
        {
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                High = 89.095m,
                Low = 88.950m,
            }
        };

        // Act
        var resultGetDayRange = QuoteHelper.GetDayRangeWithLinq(bars);


        // Assert
        resultGetDayRange.Should().BeEquivalentTo(dayRanges);
    }

    [Fact]
    public void GroupByHour_ShouldCorrectGroupWithSameHour()
    {
        // Arrange
        var bars = new List<Bar>()
        {
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 03),
                Time = new TimeOnly(8, 3, 0),
                Open = 89.095m,
                High = 89.095m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            },
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 03),
                Time = new TimeOnly(8, 1, 0),
                Open = 89.090m,
                High = 89.090m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            }
        };

        // Act
        var result = QuoteHelper.GroupByHour(bars);

        // Assert
        Assert.Equal(2, result.Single().Value.Count);
    }

    [Fact]
    public void GroupByHour_ShouldCorrectGroupWithDifferentHour()
    {
        // Arrange
        var bars = new List<Bar>()
        {
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(9, 3, 0),
                Open = 89.095m,
                High = 89.095m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            },
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(8, 1, 0),
                Open = 89.090m,
                High = 89.090m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            }
        };

        // Act
        var result = QuoteHelper.GroupByHour(bars);

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetHourBar_ShouldReturnCorrectHourBars()
    {
        // Arrange
        var dict = new Dictionary<DateTime, List<Bar>>()
        {
            {
                new DateTime(2020, 1, 02, 08, 0, 0), [
                    new()
                    {
                        Symbol = "ABBV",
                        Description = "NYSE",
                        Date = new DateOnly(2020, 1, 02),
                        Time = new TimeOnly(8, 1, 0),
                        Open = 89.095m,
                        High = 89.095m,
                        Low = 88.950m,
                        Close = 88.950m,
                        TotalVolume = 1325
                    },

                    new()
                    {
                        Symbol = "ABBV",
                        Description = "NYSE",
                        Date = new DateOnly(2020, 1, 02),
                        Time = new TimeOnly(8, 3, 0),
                        Open = 89.090m,
                        High = 89.090m,
                        Low = 88.950m,
                        Close = 88.950m,
                        TotalVolume = 1325
                    }
                ]
            }
        };

        var hoursBars = new List<Bar>()
        {
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(8, 0, 0),
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
    public void GetHourBarWithLinq_ShouldReturnCorrectHourBars()
    {
        // Arrange
        var bars = new List<Bar>
        {
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(8, 1, 0),
                Open = 89.095m,
                High = 89.095m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            },
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(8, 3, 0),
                Open = 89.090m,
                High = 89.090m,
                Low = 88.950m,
                Close = 88.950m,
                TotalVolume = 1325
            }
        }.GroupBy(bar => new DateTime(bar.Date.Year, bar.Date.Month, bar.Date.Day, bar.Time.Hour, 0, 0));

        var hoursBars = new List<Bar>()
        {
            new()
            {
                Symbol = "ABBV",
                Description = "NYSE",
                Date = new DateOnly(2020, 1, 02),
                Time = new TimeOnly(8, 0, 0),
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
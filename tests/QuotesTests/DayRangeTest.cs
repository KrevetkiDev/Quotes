using FluentAssertions;
using Quotes;

namespace QuotesTests;

public class DayRangeTest
{
    [Fact]
    public void ToString_ShouldReturnDayRangeInCSVFormat()
    {
        // Arrange
        var dayRange = new DayRange()
        {
            Symbol = "ABBV",
            Description = "NYSE",
            Date = new DateOnly(2020, 1, 02),
            High = 89.090m,
            Low = 88.950m,
        };

        // Act
        var line = dayRange.ToString();

        // Assert
        line.Should().Be("ABBV,NYSE,02.01.2020,89.090,88.950");
    }
}
using FluentAssertions;
using Quotes;

namespace QuotesTests;

public class BarTest
{
    [Fact]
    public void ToString_ShouldReturnBarInCSVFormat_ValidArgs()
    {
        // Arrange
        var bar = new Bar()
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
        };

        // Act
        var line = bar.ToString();

        // Assert
        line.Should().Be("ABBV,NYSE,02.01.2020,8:01,89.090,89.090,88.950,88.950,1325");
    }
}
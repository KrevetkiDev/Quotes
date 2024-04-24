using FluentAssertions;
using Quotes;

namespace QuotesTests;

public class ParserTests
{
    public static IEnumerable<object[]> Data = new[]
    {
        new object[]
        {
            "ABBV,NYSE,02.01.2020,08:01:00,89.090,89.090,88.950,88.950,1325",
            new Bar()
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
        }
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void Parse_ParseBar_ShouldReturnBar(string line, Bar expectedBar)
    {
        // Act
        Bar bar = Parser.ParseBar(line);

        // Assert
        bar.Should().BeEquivalentTo(expectedBar);
    }
}
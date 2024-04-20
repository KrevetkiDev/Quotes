using System.Globalization;

namespace Quotes;

public class DayRange
{
    public string symbol;
    public string description;
    public string date;
    public decimal high;
    public decimal low;

    public override string ToString()
    {
        return $"{symbol},{description},{date},{high.ToString(CultureInfo.InvariantCulture)},{low.ToString(CultureInfo.InvariantCulture)},";
    }
}
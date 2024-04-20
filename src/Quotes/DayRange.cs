using System.Globalization;

namespace Quotes;

public class DayRange
{
    public string Symbol;
    public string Description;
    public DateOnly Date;
    public decimal High;
    public decimal Low;

    public override string ToString()
    {
        return
            $"{Symbol},{Description},{Date},{High.ToString(CultureInfo.InvariantCulture)},{Low.ToString(CultureInfo.InvariantCulture)}";
    }
}
using System.Globalization;

namespace Quotes;

public class Bar
{
    public string Symbol;
    public string Description;
    public DateOnly Date;
    public TimeOnly Time;
    public decimal Open;
    public decimal High;
    public decimal Low;
    public decimal Close;
    public int TotalVolume;

    public override string ToString()
    {
        return
            $"{Symbol},{Description},{Date},{Time},{Open.ToString(CultureInfo.InvariantCulture)},{High.ToString(CultureInfo.InvariantCulture)},{Low.ToString(CultureInfo.InvariantCulture)},{Close.ToString(CultureInfo.InvariantCulture)},{TotalVolume}";
    }
}
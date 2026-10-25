using System.Globalization;

namespace Algorithms.Tests.Appendix;

// The numbers quoted in appendix C that aren't produced by MoneyExperiment
public class MoneyInCodeTests
{
    [Fact]
    public void DoubleArithmetic_ShowsTheErrors()
    {
        Assert.False(0.1 + 0.2 == 0.3);

        double total = 0;
        for (var i = 0; i < 10; i++)
            total += 0.10;

        Assert.Equal("0.9999999999999999", total.ToString(CultureInfo.InvariantCulture));
    }

    [Fact]
    public void Decimal_IsAWholeNumberWithAScale()
    {
        Assert.Equal([125030, 0, 0, 131072], decimal.GetBits(1250.30m));
        Assert.Equal(2, (decimal.GetBits(1250.30m)[3] >> 16) & 0xFF);
        Assert.Equal("79228162514264337593543950335", decimal.MaxValue.ToString(CultureInfo.InvariantCulture));
        Assert.Equal(0.3333333333333333333333333333m, 1m / 3m);
    }

    [Fact]
    public void MathRound_GoesToEvenByDefault()
    {
        Assert.Equal(2m, Math.Round(2.5m));
        Assert.Equal(4m, Math.Round(3.5m));
        Assert.Equal(3m, Math.Round(2.5m, MidpointRounding.AwayFromZero));
        Assert.Equal(0.12m, Math.Round(0.125m, 2));
        Assert.Equal(0.13m, Math.Round(0.125m, 2, MidpointRounding.AwayFromZero));
    }

    [Theory]
    [InlineData("19.99", "1998.9999999999998", 1998, 1999)]
    [InlineData("0.29", "28.999999999999996", 28, 29)]
    public void ConvertingToCents_ThroughADouble_LosesACent(string price, string product, long truncated, long exact)
    {
        var asDouble = double.Parse(price, CultureInfo.InvariantCulture);
        var asDecimal = decimal.Parse(price, CultureInfo.InvariantCulture);

        Assert.Equal(product, (asDouble * 100).ToString("R", CultureInfo.InvariantCulture));
        Assert.Equal(truncated, (long)(asDouble * 100));
        Assert.Equal(exact, (long)Math.Round(asDouble * 100));
        Assert.Equal(exact, (long)(asDecimal * 100));
    }

    [Fact]
    public void IntCents_OverflowAboveAbout21MillionEuros()
    {
        Assert.Equal(2_147_483_647, int.MaxValue);
        Assert.Equal(21_474_836.47m, int.MaxValue / 100m);
    }

    [Fact]
    public void NaiveSplit_LosesACent()
    {
        var installment = Math.Round(100.00m / 3, 2);

        Assert.Equal(33.33m, installment);
        Assert.Equal(99.99m, installment * 3);
    }
}

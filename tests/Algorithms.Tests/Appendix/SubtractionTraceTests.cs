using Algorithms.Appendix;

namespace Algorithms.Tests.Appendix;

// Every bit and number quoted in appendix B is checked here
public class SubtractionTraceTests
{
    private static readonly SubtractionTrace Trace = SubtractionTrace.Of(1250.30, 430.20);

    [Fact]
    public void Align_MovesTheSmallerNumberTwoPlacesToTheRight()
    {
        Assert.Equal(10, Trace.AlignedExponent);
        Assert.Equal("1.001110001001001100110011001100110011001100110011001100", Trace.MinuendAligned);
        Assert.Equal("0.011010111000110011001100110011001100110011001100110011", Trace.SubtrahendAligned);
    }

    [Fact]
    public void Subtract_GivesTheExactDifference()
    {
        Assert.Equal("0.110011010000011001100110011001100110011001100110011001", Trace.ExactDifference);

        // The exact difference of the two stored values, in decimal
        var exactA = DoubleInspector.ExactValue(1250.30);
        var exactB = DoubleInspector.ExactValue(430.20);
        Assert.Equal("1250.299999999999954525264911353588104248046875", exactA);
        Assert.Equal("430.19999999999998863131622783839702606201171875", exactB);
        Assert.Equal("820.09999999999996589394868351519107818603515625", ExactSubtract(exactA, exactB));
    }

    [Fact]
    public void Normalize_LeavesOneBitTooMany()
    {
        Assert.Equal(9, Trace.NormalizedExponent);
        Assert.Equal("1.10011010000011001100110011001100110011001100110011001", Trace.Normalized);
        Assert.Equal("1001101000001100110011001100110011001100110011001100", Trace.FittingFraction);
        Assert.Equal("1", Trace.LeftoverBits);
    }

    [Fact]
    public void Round_TieGoesToTheEvenNeighbour()
    {
        double total = 1250.30;
        double invoice = 430.20;
        var result = total - invoice;

        // The fitting bits end in 0 (even): the processor keeps them, rounding down
        Assert.Equal(0x4089A0CCCCCCCCCC, BitConverter.DoubleToInt64Bits(result));
        Assert.Equal(0x4089A0CCCCCCCCCD, BitConverter.DoubleToInt64Bits(820.10));
        Assert.EndsWith("0", Trace.FittingFraction);

        // The two candidates are one step apart, and the exact difference is exactly halfway
        Assert.Equal("820.09999999999990905052982270717620849609375", DoubleInspector.ExactValue(result));
        Assert.Equal("820.1000000000000227373675443232059478759765625", DoubleInspector.ExactValue(820.10));
        Assert.Equal(Math.Pow(2, -43), DoubleInspector.GapToNext(result));

        const string exactDifference = "820.09999999999996589394868351519107818603515625";
        var distanceBelow = ExactSubtract(exactDifference, DoubleInspector.ExactValue(result));
        var distanceAbove = ExactSubtract(DoubleInspector.ExactValue(820.10), exactDifference);
        Assert.Equal(distanceBelow, distanceAbove);
        Assert.Equal(DoubleInspector.ExactValue(Math.Pow(2, -44)), distanceBelow);
    }

    // a - b on decimal strings with any number of digits (decimal keeps only 28-29 significant digits)
    private static string ExactSubtract(string a, string b)
    {
        var scale = Math.Max(Decimals(a), Decimals(b));
        var difference = Scaled(a, scale) - Scaled(b, scale);
        var digits = System.Numerics.BigInteger.Abs(difference).ToString().PadLeft(scale + 1, '0');
        var text = (digits[..^scale] + "." + digits[^scale..]).TrimEnd('0').TrimEnd('.');
        return (difference < 0 ? "-" : "") + text;

        static int Decimals(string number) => number.Contains('.') ? number.Length - number.IndexOf('.') - 1 : 0;
        static System.Numerics.BigInteger Scaled(string number, int scale) =>
            System.Numerics.BigInteger.Parse(number.Replace(".", "") + new string('0', scale - Decimals(number)));
    }

    [Fact]
    public void Print_UsesTheShortestTextThatIdentifiesTheDouble()
    {
        double total = 1250.30;
        double invoice = 430.20;
        var result = total - invoice;

        Assert.False(result == 820.10);
        Assert.Equal("820.0999999999999", result.ToString(System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal("820.1", 820.10.ToString(System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(result, double.Parse("820.0999999999999", System.Globalization.CultureInfo.InvariantCulture));

        // One digit less is not enough: it identifies a different double
        Assert.NotEqual(result, double.Parse("820.099999999999", System.Globalization.CultureInfo.InvariantCulture));

        Assert.Equal("820.09999999999991", result.ToString("G17", System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void Compare_WithATolerance_TheTwoDoublesAreEqual()
    {
        double total = 1250.30;
        double invoice = 430.20;

        Assert.True(Math.Abs((total - invoice) - 820.10) < 1e-9);
    }

    [Fact]
    public void Of_RejectsNumbersOutsideItsRange()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => SubtractionTrace.Of(430.20, 1250.30));
        Assert.Throws<ArgumentOutOfRangeException>(() => SubtractionTrace.Of(1250.30, 0));
    }
}

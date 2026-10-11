using Algorithms.Appendix;

namespace Algorithms.Tests.Appendix;

// Every number quoted in appendix A is checked here
public class DoubleInspectorTests
{
    [Fact]
    public void IntegersInBinary_MatchTheAppendix()
    {
        Assert.Equal("1101", Convert.ToString(13, 2));
        Assert.Equal("1010", Convert.ToString(10, 2));
        Assert.Equal("110", Convert.ToString(6, 2));
        Assert.Equal("1000", Convert.ToString(8, 2));
        Assert.Equal("1001", Convert.ToString(9, 2));
        Assert.Equal("10011100010", Convert.ToString(1250, 2));
        Assert.Equal("4e2", Convert.ToString(1250, 16));
        Assert.Equal("000004E2", $"{1250:X8}");
    }

    [Fact]
    public void Bits_Of1250Point30()
    {
        Assert.Equal(
            "0 10000001001 0011100010010011001100110011001100110011001100110011",
            DoubleInspector.Bits(1250.30));
        Assert.Equal(0x4093893333333333, BitConverter.DoubleToInt64Bits(1250.30));
        Assert.Equal(10, DoubleInspector.Exponent(1250.30));
    }

    [Theory]
    [InlineData(0.1, "0.1000000000000000055511151231257827021181583404541015625")]
    [InlineData(0.5, "0.5")]
    [InlineData(0.625, "0.625")]
    [InlineData(1250.30, "1250.299999999999954525264911353588104248046875")]
    [InlineData(430.20, "430.19999999999998863131622783839702606201171875")]
    [InlineData(820.10, "820.1000000000000227373675443232059478759765625")]
    [InlineData(-2.5, "-2.5")]
    [InlineData(1024.0, "1024")]
    public void ExactValue_ShowsEveryDigitTheDoubleHolds(double value, string expected)
    {
        Assert.Equal(expected, DoubleInspector.ExactValue(value));
    }

    [Theory]
    [InlineData(430.20, -44)]
    [InlineData(820.10, -43)]
    [InlineData(1250.30, -42)]
    [InlineData(1_000_000.0, -33)]
    [InlineData(9_007_199_254_740_992.0, 1)]
    public void GapToNext_GrowsWithTheSizeOfTheNumber(double value, int powerOf2)
    {
        Assert.Equal(Math.Pow(2, powerOf2), DoubleInspector.GapToNext(value));
    }

    [Fact]
    public void Above2To53_NotEveryIntegerExists()
    {
        // 2⁵³ + 1 falls between two doubles and is rounded to 2⁵³
        Assert.Equal(9_007_199_254_740_992.0, (double)9_007_199_254_740_993L);
    }

    [Fact]
    public void ExactValue_RejectsInfinityAndNaN()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => DoubleInspector.ExactValue(double.NaN));
        Assert.Throws<ArgumentOutOfRangeException>(() => DoubleInspector.ExactValue(double.PositiveInfinity));
    }
}

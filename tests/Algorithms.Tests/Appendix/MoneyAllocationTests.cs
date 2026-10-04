using Algorithms.Appendix;

namespace Algorithms.Tests.Appendix;

public class MoneyAllocationTests
{
    [Fact]
    public void Splitting100EurInThree_GivesTheLeftoverCentToTheFirstPart()
    {
        Assert.Equal([3_334, 3_333, 3_333], MoneyAllocation.Split(10_000, 3));
    }

    [Fact]
    public void AnExactDivision_GivesEqualParts()
    {
        Assert.Equal([2_500, 2_500, 2_500, 2_500], MoneyAllocation.Split(10_000, 4));
    }

    [Fact]
    public void ANegativeTotal_IsSplitTheSameWay()
    {
        // A refund of 100.00 EUR over three installments
        Assert.Equal([-3_334, -3_333, -3_333], MoneyAllocation.Split(-10_000, 3));
    }

    [Fact]
    public void MorePartsThanCents_SomePartsAreZero()
    {
        Assert.Equal([1, 1, 0, 0, 0], MoneyAllocation.Split(2, 5));
    }

    [Fact]
    public void LessThanOnePart_IsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => MoneyAllocation.Split(10_000, 0));
    }

    [Fact]
    public void ThePartsAlwaysAddUpToTheTotal_AndDifferByAtMostOneCent()
    {
        var random = new Random(11);

        for (var round = 0; round < 1_000; round++)
        {
            long total = random.Next(-1_000_000, 1_000_000);
            var parts = random.Next(1, 50);

            var split = MoneyAllocation.Split(total, parts);

            Assert.Equal(parts, split.Length);
            Assert.Equal(total, split.Sum());
            Assert.True(split.Max() - split.Min() <= 1);
        }
    }

    [Fact]
    public void WhyNotJustDivide_TheNaiveSplitLosesACent()
    {
        // The naive way: divide and round each part. 3 × 33.33 = 99.99: one cent disappeared
        var naivePart = Math.Round(100.00m / 3, 2);

        Assert.Equal(99.99m, naivePart * 3);
        Assert.Equal(10_000, MoneyAllocation.Split(10_000, 3).Sum());
    }
}

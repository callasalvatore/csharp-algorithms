using Algorithms.Foundations;

namespace Algorithms.Tests.Foundations;

public class SearchExamplesTests
{
    [Fact]
    public void LinearSearch_FindsItemAtTheStart_InOneStep()
    {
        var (index, steps) = SearchExamples.LinearSearch([7, 3, 9], 7);

        Assert.Equal(0, index);
        Assert.Equal(1, steps);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(1_000)]
    [InlineData(1_000_000)]
    public void LinearSearch_MissingItem_TakesExactlyNSteps(int n)
    {
        var items = Enumerable.Range(0, n).ToArray();

        var (index, steps) = SearchExamples.LinearSearch(items, -1);

        // Worst case: every item is checked. The work grows like n → O(n)
        Assert.Equal(-1, index);
        Assert.Equal(n, steps);
    }

    [Fact]
    public void BinarySearch_FindsEveryItem()
    {
        int[] sorted = [2, 5, 8, 12, 16, 23, 38, 56, 72, 91];

        for (var i = 0; i < sorted.Length; i++)
            Assert.Equal(i, SearchExamples.BinarySearch(sorted, sorted[i]).Index);
    }

    [Fact]
    public void BinarySearch_MissingItem_ReturnsMinusOne()
    {
        Assert.Equal(-1, SearchExamples.BinarySearch([2, 5, 8], 6).Index);
        Assert.Equal(-1, SearchExamples.BinarySearch([], 6).Index);
    }

    [Theory]
    [InlineData(10, 4)]
    [InlineData(1_000, 10)]
    [InlineData(1_000_000, 20)]
    public void BinarySearch_TakesAtMostLog2NSteps(int n, int maxSteps)
    {
        var sorted = Enumerable.Range(0, n).ToArray();

        var (_, steps) = SearchExamples.BinarySearch(sorted, -1);

        // A million items, at most 20 steps: the work grows like log2(n) → O(log n)
        Assert.True(steps <= maxSteps, $"Expected at most {maxSteps} steps, got {steps}");
    }
}

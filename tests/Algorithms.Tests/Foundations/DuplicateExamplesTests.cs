using Algorithms.Foundations;

namespace Algorithms.Tests.Foundations;

public class DuplicateExamplesTests
{
    [Theory]
    [InlineData(new[] { 4, 1, 7, 1 }, true)]
    [InlineData(new[] { 4, 1, 7, 2 }, false)]
    [InlineData(new int[0], false)]
    [InlineData(new[] { 5 }, false)]
    public void BothVersions_GiveTheSameAnswer(int[] items, bool expected)
    {
        Assert.Equal(expected, DuplicateExamples.WithNestedLoops(items).HasDuplicates);
        Assert.Equal(expected, DuplicateExamples.WithHashSet(items).HasDuplicates);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(1_000)]
    public void NestedLoops_WithoutDuplicates_ComparesEveryPair(int n)
    {
        var items = Enumerable.Range(0, n).ToArray();

        var (_, comparisons) = DuplicateExamples.WithNestedLoops(items);

        // n * (n - 1) / 2 pairs: about n²/2 → O(n²)
        Assert.Equal((long)n * (n - 1) / 2, comparisons);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(1_000)]
    public void HashSet_WithoutDuplicates_TakesOneStepPerItem(int n)
    {
        var items = Enumerable.Range(0, n).ToArray();

        var (_, steps) = DuplicateExamples.WithHashSet(items);

        // One step per item → O(n)
        Assert.Equal(n, steps);
    }
}

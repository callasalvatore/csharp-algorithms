using Algorithms.Appendix;

namespace Algorithms.Tests.Appendix;

public class SortingExamplesTests
{
    public static TheoryData<string> Algorithms => ["Insertion", "Merge"];

    private static (int[] Sorted, long Comparisons) Sort(string algorithm, int[] items) =>
        algorithm == "Insertion" ? SortingExamples.InsertionSort(items) : SortingExamples.MergeSort(items);

    [Theory]
    [MemberData(nameof(Algorithms))]
    public void SortsTheAppendixExample(string algorithm)
    {
        Assert.Equal([1, 2, 4, 5], Sort(algorithm, [5, 2, 4, 1]).Sorted);
    }

    [Theory]
    [MemberData(nameof(Algorithms))]
    public void HandlesEmptyInputSingleItemAndDuplicates(string algorithm)
    {
        Assert.Empty(Sort(algorithm, []).Sorted);
        Assert.Equal([7], Sort(algorithm, [7]).Sorted);
        Assert.Equal([1, 3, 3, 3, 8], Sort(algorithm, [3, 8, 3, 1, 3]).Sorted);
    }

    [Theory]
    [MemberData(nameof(Algorithms))]
    public void DoesNotModifyTheInput(string algorithm)
    {
        int[] items = [3, 1, 2];

        Sort(algorithm, items);

        Assert.Equal([3, 1, 2], items);
    }

    [Theory]
    [MemberData(nameof(Algorithms))]
    public void AgreesWithArraySortOnRandomInputs(string algorithm)
    {
        var random = new Random(5);

        for (var round = 0; round < 200; round++)
        {
            var items = Enumerable.Range(0, random.Next(0, 100)).Select(_ => random.Next(-50, 50)).ToArray();
            var expected = items.ToArray();
            Array.Sort(expected);

            Assert.Equal(expected, Sort(algorithm, items).Sorted);
        }
    }

    [Fact]
    public void InsertionSort_OnReversedInput_ComparesEveryPair()
    {
        var reversed = Enumerable.Range(0, 1_000).Reverse().ToArray();

        // Worst case: every item slides past all the previous ones → n(n-1)/2 → O(n²)
        Assert.Equal(1_000 * 999 / 2, SortingExamples.InsertionSort(reversed).Comparisons);
    }

    [Fact]
    public void InsertionSort_OnSortedInput_IsLinear()
    {
        var sorted = Enumerable.Range(0, 1_000).ToArray();

        // Best case: one comparison per item → O(n)
        Assert.Equal(999, SortingExamples.InsertionSort(sorted).Comparisons);
    }

    [Theory]
    [InlineData(1_000)]
    [InlineData(100_000)]
    public void MergeSort_NeverExceedsNTimesLog2N(int n)
    {
        var random = new Random(9);
        var items = Enumerable.Range(0, n).Select(_ => random.Next()).ToArray();

        var comparisons = SortingExamples.MergeSort(items).Comparisons;

        Assert.True(comparisons <= n * Math.Ceiling(Math.Log2(n)), $"{comparisons} comparisons for n = {n}");
    }

    [Fact]
    public void OrderBy_IsStable_ItemsWithTheSameKeyKeepTheirOrder()
    {
        // Orders already sorted by date; sorting them by amount must not shuffle orders with equal amounts
        (string Id, int Amount)[] orders = [("A", 50), ("B", 20), ("C", 50), ("D", 20)];

        var byAmount = orders.OrderBy(o => o.Amount).Select(o => o.Id);

        Assert.Equal(["B", "D", "A", "C"], byAmount);
    }
}

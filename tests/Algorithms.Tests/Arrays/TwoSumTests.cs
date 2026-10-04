using Algorithms.Arrays;

namespace Algorithms.Tests.Arrays;

public class TwoSumTests
{
    // The invoices of the lesson, in cents: 310.00, 475.00, 820.00, 155.00, 430.00 EUR
    private static readonly int[] OpenInvoices = [31_000, 47_500, 82_000, 15_500, 43_000];

    public static TheoryData<string> UnsortedApproaches => ["BruteForce", "WithDictionary"];

    private static (PairResult? Pair, int Steps) Solve(string approach, IReadOnlyList<int> amounts, int target) =>
        approach == "BruteForce" ? TwoSum.BruteForce(amounts, target) : TwoSum.WithDictionary(amounts, target);

    [Theory]
    [MemberData(nameof(UnsortedApproaches))]
    public void FindsTheTwoInvoicesPaidByTheTransfer(string approach)
    {
        // A transfer of 1,250.00 EUR pays INV-3 (820.00, index 2) and INV-5 (430.00, index 4)
        var (pair, _) = Solve(approach, OpenInvoices, 125_000);

        Assert.Equal(new PairResult(2, 4), pair);
    }

    [Theory]
    [MemberData(nameof(UnsortedApproaches))]
    public void TwoEqualAmounts_CanFormThePair(string approach)
    {
        var (pair, _) = Solve(approach, [300, 300], 600);

        Assert.Equal(new PairResult(0, 1), pair);
    }

    [Theory]
    [MemberData(nameof(UnsortedApproaches))]
    public void TheSameAmount_CannotBeUsedTwice(string approach)
    {
        // 300 + 300 would be 600, but there is only one 300
        var (pair, _) = Solve(approach, [300, 500], 600);

        Assert.Null(pair);
    }

    [Theory]
    [MemberData(nameof(UnsortedApproaches))]
    public void NoPairAddsUpToTheTarget_ReturnsNull(string approach)
    {
        Assert.Null(Solve(approach, OpenInvoices, 100_000).Pair);
        Assert.Null(Solve(approach, [], 100).Pair);
        Assert.Null(Solve(approach, [100], 100).Pair);
    }

    [Theory]
    [MemberData(nameof(UnsortedApproaches))]
    public void WorksWithNegativeAmounts(string approach)
    {
        // A credit note of -20.00 EUR and an invoice of 70.00 EUR make a payment of 50.00 EUR
        var (pair, _) = Solve(approach, [-2_000, 15_000, 7_000], 5_000);

        Assert.Equal(new PairResult(0, 2), pair);
    }

    [Theory]
    [MemberData(nameof(UnsortedApproaches))]
    public void DoesNotFindFalseMatchesBecauseOfOverflow(string approach)
    {
        // With int arithmetic, int.MaxValue + int.MaxValue wraps around to -2
        var (pair, _) = Solve(approach, [int.MaxValue, int.MaxValue], -2);

        Assert.Null(pair);
    }

    [Fact]
    public void Steps_BruteForceIsQuadratic_DictionaryIsLinear()
    {
        var amounts = Enumerable.Range(1, 1_000).ToArray();

        // No pair adds up to -1, so both approaches do their worst case
        Assert.Equal(1_000 * 999 / 2, TwoSum.BruteForce(amounts, -1).Steps);
        Assert.Equal(1_000, TwoSum.WithDictionary(amounts, -1).Steps);
    }

    [Fact]
    public void TwoPointers_FindsThePairInTheSortedInvoices()
    {
        int[] sorted = [15_500, 31_000, 43_000, 47_500, 82_000];

        // 905.00 EUR = 430.00 + 475.00
        var (pair, steps) = TwoSum.WithTwoPointers(sorted, 90_500);

        Assert.Equal(new PairResult(2, 3), pair);
        Assert.Equal(4, steps); // the trace in the lesson
    }

    [Fact]
    public void TwoPointers_TakesAtMostNMinusOneSteps()
    {
        var sorted = Enumerable.Range(1, 1_000).ToArray();

        var (pair, steps) = TwoSum.WithTwoPointers(sorted, -1);

        // Each step discards one item, so at most n - 1 steps: O(n)
        Assert.Null(pair);
        Assert.True(steps <= 999);
    }

    [Fact]
    public void TwoPointers_HandlesTheSameEdgeCases()
    {
        Assert.Equal(new PairResult(0, 1), TwoSum.WithTwoPointers([300, 300], 600).Pair);
        Assert.Null(TwoSum.WithTwoPointers([300, 500], 600).Pair);
        Assert.Null(TwoSum.WithTwoPointers([], 100).Pair);
        Assert.Null(TwoSum.WithTwoPointers([int.MaxValue, int.MaxValue], -2).Pair);
    }

    [Fact]
    public void AllApproaches_AgreeWithBruteForceOnRandomInputs()
    {
        // The brute force is slow but obviously correct: we use it to check the faster versions
        var random = new Random(42);

        for (var round = 0; round < 500; round++)
        {
            var amounts = Enumerable.Range(0, random.Next(0, 30)).Select(_ => random.Next(-50, 50)).ToArray();
            var target = random.Next(-60, 60);
            var sorted = amounts.Order().ToArray();

            var expected = TwoSum.BruteForce(amounts, target).Pair is not null;

            AssertValidPair(amounts, target, TwoSum.WithDictionary(amounts, target).Pair, expected);
            AssertValidPair(sorted, target, TwoSum.WithTwoPointers(sorted, target).Pair, expected);
        }
    }

    private static void AssertValidPair(int[] amounts, int target, PairResult? pair, bool shouldExist)
    {
        Assert.Equal(shouldExist, pair is not null);

        if (pair is not null)
        {
            Assert.NotEqual(pair.FirstIndex, pair.SecondIndex);
            Assert.Equal(target, amounts[pair.FirstIndex] + amounts[pair.SecondIndex]);
        }
    }
}

using Algorithms.Arrays;

namespace Algorithms.Tests.Arrays;

public class ThreeSumTests
{
    [Fact]
    public void FindsTheEntriesThatBalanceToZero()
    {
        // The ledger example of the lesson: -400, -100, -100, 0, 100, 200
        var triples = ThreeSum.FindAll([-100, 0, 100, 200, -100, -400], 0);

        Assert.Equal([(-100, -100, 200), (-100, 0, 100)], triples);
    }

    [Fact]
    public void RepeatedValues_ProduceEachTripleOnlyOnce()
    {
        Assert.Equal([(0, 0, 0)], ThreeSum.FindAll([0, 0, 0, 0, 0], 0));
        Assert.Equal([(1, 1, 2)], ThreeSum.FindAll([1, 1, 1, 2, 2], 4));
    }

    [Fact]
    public void FewerThanThreeValues_ReturnsNothing()
    {
        Assert.Empty(ThreeSum.FindAll([], 0));
        Assert.Empty(ThreeSum.FindAll([1, 2], 3));
    }

    [Fact]
    public void AgreesWithBruteForceOnRandomInputs()
    {
        var random = new Random(7);

        for (var round = 0; round < 300; round++)
        {
            var values = Enumerable.Range(0, random.Next(0, 15)).Select(_ => random.Next(-10, 10)).ToArray();
            var target = random.Next(-5, 5);

            var expected = BruteForce(values, target);
            var actual = ThreeSum.FindAll(values, target).ToHashSet();

            Assert.Equal(expected.Count, ThreeSum.FindAll(values, target).Count); // no duplicates
            Assert.True(expected.SetEquals(actual));
        }
    }

    // O(n³): tries every triple and normalizes it, so duplicates collapse in the set
    private static HashSet<(int, int, int)> BruteForce(int[] values, int target)
    {
        var triples = new HashSet<(int, int, int)>();

        for (var i = 0; i < values.Length; i++)
            for (var j = i + 1; j < values.Length; j++)
                for (var k = j + 1; k < values.Length; k++)
                {
                    if (values[i] + values[j] + values[k] != target)
                        continue;

                    int[] triple = [values[i], values[j], values[k]];
                    Array.Sort(triple);
                    triples.Add((triple[0], triple[1], triple[2]));
                }

        return triples;
    }
}

using Algorithms.Arrays;

namespace Algorithms.Tests.Arrays;

public class KSumTests
{
    [Fact]
    public void FourSum_FindsEveryUniqueCombination()
    {
        var results = KSum.FindAll([1, 0, -1, 0, -2, 2], k: 4, target: 0);

        Assert.Equal(
            [[-2, -1, 1, 2], [-2, 0, 0, 2], [-1, 0, 0, 1]],
            results);
    }

    [Fact]
    public void WithKEqualTo3_GivesTheSameResultAsThreeSum()
    {
        int[] values = [-100, 0, 100, 200, -100, -400];

        var expected = ThreeSum.FindAll(values, 0).Select(t => new[] { t.A, t.B, t.C });

        Assert.Equal(expected, KSum.FindAll(values, k: 3, target: 0));
    }

    [Fact]
    public void WithKEqualTo2_FindsEveryUniquePair()
    {
        Assert.Equal([[1, 5], [2, 4]], KSum.FindAll([5, 1, 4, 2, 4, 1], k: 2, target: 6));
    }

    [Fact]
    public void LargeValues_DoNotOverflow()
    {
        // The sum of four int.MaxValue is far beyond the int range: it's computed as long
        int[] values = [int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue];

        Assert.Single(KSum.FindAll(values, k: 4, target: 4L * int.MaxValue));
    }

    [Fact]
    public void KSmallerThan2_IsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => KSum.FindAll([1, 2, 3], k: 1, target: 1));
    }

    [Fact]
    public void FourSum_AgreesWithBruteForceOnRandomInputs()
    {
        var random = new Random(3);

        for (var round = 0; round < 200; round++)
        {
            var values = Enumerable.Range(0, random.Next(0, 12)).Select(_ => random.Next(-6, 6)).ToArray();
            var target = random.Next(-4, 4);

            var expected = BruteForce(values, target);
            var actual = KSum.FindAll(values, k: 4, target).Select(c => (c[0], c[1], c[2], c[3])).ToList();

            Assert.Equal(expected.Count, actual.Count); // no duplicates
            Assert.True(expected.SetEquals(actual));
        }
    }

    private static HashSet<(int, int, int, int)> BruteForce(int[] values, int target)
    {
        var results = new HashSet<(int, int, int, int)>();

        for (var a = 0; a < values.Length; a++)
            for (var b = a + 1; b < values.Length; b++)
                for (var c = b + 1; c < values.Length; c++)
                    for (var d = c + 1; d < values.Length; d++)
                    {
                        if (values[a] + values[b] + values[c] + values[d] != target)
                            continue;

                        int[] combination = [values[a], values[b], values[c], values[d]];
                        Array.Sort(combination);
                        results.Add((combination[0], combination[1], combination[2], combination[3]));
                    }

        return results;
    }
}

namespace Algorithms.Arrays;

/// <summary>
/// Lesson 01: find every unique triple of values whose sum is the target.
/// </summary>
public static class ThreeSum
{
    /// <summary>
    /// Sorts the values, fixes the first item of the triple and searches the other two
    /// with two pointers. Time O(n²), extra space O(n) for the sorted copy.
    /// </summary>
    /// <param name="values">
    /// The ledger entries, in cents, in any order. Positive values are charges, negative values are refunds;
    /// they can repeat. The list is not modified: the method sorts a copy.
    /// </param>
    /// <param name="target">The total the three entries must reach, in cents: 0 to find entries that balance out.</param>
    /// <returns>
    /// Every distinct triple of values (not positions) whose sum is exactly <paramref name="target"/>,
    /// each in ascending order (<c>A ≤ B ≤ C</c>). No triple appears twice; empty if there are none.
    /// </returns>
    public static IReadOnlyList<(int A, int B, int C)> FindAll(IReadOnlyList<int> values, int target)
    {
        var sorted = values.Order().ToArray();
        var triples = new List<(int, int, int)>();

        for (var i = 0; i < sorted.Length - 2; i++)
        {
            // Same first value as the previous round: it would produce the same triples
            if (i > 0 && sorted[i] == sorted[i - 1])
                continue;

            var left = i + 1;
            var right = sorted.Length - 1;

            while (left < right)
            {
                var sum = (long)sorted[i] + sorted[left] + sorted[right];

                if (sum < target)
                {
                    left++;
                }
                else if (sum > target)
                {
                    right--;
                }
                else
                {
                    triples.Add((sorted[i], sorted[left], sorted[right]));
                    left++;
                    right--;

                    // Skip the values we've just used, again to avoid duplicate triples
                    while (left < right && sorted[left] == sorted[left - 1])
                        left++;
                    while (left < right && sorted[right] == sorted[right + 1])
                        right--;
                }
            }
        }

        return triples;
    }
}

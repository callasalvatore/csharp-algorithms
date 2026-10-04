namespace Algorithms.Arrays;

/// <summary>
/// Lesson 01: find two different items whose sum is exactly the target.
/// Amounts are in cents (125000 = 1,250.00 EUR).
///
/// Each method also returns the number of steps it took, so the tests
/// can show how the work grows, as in lesson 00.
/// </summary>
public static class TwoSum
{
    /// <summary>
    /// Tries every pair. Time O(n²), space O(1).
    /// </summary>
    public static (PairResult? Pair, int Steps) BruteForce(IReadOnlyList<int> amounts, int target)
    {
        var steps = 0;

        for (var i = 0; i < amounts.Count; i++)
        {
            for (var j = i + 1; j < amounts.Count; j++)
            {
                steps++;

                // long: two large ints can overflow, and a wrong sum could look like a match
                if ((long)amounts[i] + amounts[j] == target)
                    return (new PairResult(i, j), steps);
            }
        }

        return (null, steps);
    }

    /// <summary>
    /// For each amount, asks "have I already seen the amount that completes it?".
    /// A dictionary answers in O(1) on average, so the whole search is O(n) on average. Space O(n).
    /// </summary>
    public static (PairResult? Pair, int Steps) WithDictionary(IReadOnlyList<int> amounts, int target)
    {
        // amount already seen -> its index
        var seen = new Dictionary<long, int>();
        var steps = 0;

        for (var i = 0; i < amounts.Count; i++)
        {
            steps++;
            long missing = (long)target - amounts[i];

            // Check BEFORE adding the current amount, so it can't be paired with itself
            if (seen.TryGetValue(missing, out var j))
                return (new PairResult(j, i), steps);

            // TryAdd keeps the first index when the same amount appears twice
            seen.TryAdd(amounts[i], i);
        }

        return (null, steps);
    }

    /// <summary>
    /// Works on amounts sorted in ascending order: one pointer starts from the smallest,
    /// one from the largest, and they move towards each other. Time O(n), space O(1).
    /// The indices refer to the sorted list.
    /// </summary>
    public static (PairResult? Pair, int Steps) WithTwoPointers(IReadOnlyList<int> sortedAmounts, int target)
    {
        var left = 0;
        var right = sortedAmounts.Count - 1;
        var steps = 0;

        while (left < right)
        {
            steps++;
            var sum = (long)sortedAmounts[left] + sortedAmounts[right];

            if (sum == target)
                return (new PairResult(left, right), steps);

            if (sum < target)
                left++;   // even with the largest amount left, the smallest is too small: discard it
            else
                right--;  // even with the smallest amount left, the largest is too big: discard it
        }

        return (null, steps);
    }
}

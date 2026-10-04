namespace Algorithms.Arrays;

/// <summary>
/// Lesson 01: the general version, k values whose sum is the target.
/// </summary>
public static class KSum
{
    /// <summary>
    /// Fixes one value and solves the same problem for k - 1 values on the rest,
    /// until only 2 values are left: then it uses two pointers.
    /// Time O(n^(k-1)), plus O(n log n) to sort. Each combination is in ascending order and unique.
    /// </summary>
    public static IReadOnlyList<int[]> FindAll(IReadOnlyList<int> values, int k, long target)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(k, 2);

        var sorted = values.Order().ToArray();
        var results = new List<int[]>();

        Search(sorted, k, target, start: 0, chosen: new Stack<int>(), results);
        return results;
    }

    private static void Search(int[] sorted, int k, long target, int start, Stack<int> chosen, List<int[]> results)
    {
        if (k == 2)
        {
            FindPairs(sorted, target, start, chosen, results);
            return;
        }

        for (var i = start; i < sorted.Length - k + 1; i++)
        {
            if (i > start && sorted[i] == sorted[i - 1])
                continue;

            chosen.Push(sorted[i]);
            Search(sorted, k - 1, target - sorted[i], i + 1, chosen, results);
            chosen.Pop();
        }
    }

    private static void FindPairs(int[] sorted, long target, int start, Stack<int> chosen, List<int[]> results)
    {
        var left = start;
        var right = sorted.Length - 1;

        while (left < right)
        {
            var sum = (long)sorted[left] + sorted[right];

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
                // The stack holds the fixed values from the last one chosen to the first: reverse it
                results.Add([.. chosen.Reverse(), sorted[left], sorted[right]]);
                left++;
                right--;

                while (left < right && sorted[left] == sorted[left - 1])
                    left++;
                while (left < right && sorted[right] == sorted[right + 1])
                    right--;
            }
        }
    }
}

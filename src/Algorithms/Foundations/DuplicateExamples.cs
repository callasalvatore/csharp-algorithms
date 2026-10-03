namespace Algorithms.Foundations;

/// <summary>
/// Lesson 00: the same question answered in O(n²) and in O(n),
/// trading memory for speed.
/// </summary>
public static class DuplicateExamples
{
    /// <summary>
    /// Compares every pair of items with two nested loops.
    /// Time O(n²): about n²/2 comparisons. Space O(1): no extra memory.
    /// </summary>
    public static (bool HasDuplicates, long Comparisons) WithNestedLoops(IReadOnlyList<int> items)
    {
        long comparisons = 0;

        for (var i = 0; i < items.Count; i++)
        {
            // Starting from i + 1: each pair is compared only once
            for (var j = i + 1; j < items.Count; j++)
            {
                comparisons++;
                if (items[i] == items[j])
                    return (true, comparisons);
            }
        }

        return (false, comparisons);
    }

    /// <summary>
    /// Remembers the items already seen in a HashSet, whose lookups take O(1) on average.
    /// Time O(n): one step per item. Space O(n): the set can grow up to n items.
    /// </summary>
    public static (bool HasDuplicates, long Steps) WithHashSet(IReadOnlyList<int> items)
    {
        var seen = new HashSet<int>();
        long steps = 0;

        foreach (var item in items)
        {
            steps++;

            // Add returns false when the item is already in the set
            if (!seen.Add(item))
                return (true, steps);
        }

        return (false, steps);
    }
}

namespace Algorithms.Foundations;

/// <summary>
/// Lesson 00: two ways to find a value, returning how many steps they took
/// so that the growth of the work can be observed directly.
/// </summary>
public static class SearchExamples
{
    /// <summary>
    /// Checks the items one by one. Worst case: n steps → O(n).
    /// </summary>
    public static (int Index, int Steps) LinearSearch(IReadOnlyList<int> items, int target)
    {
        var steps = 0;

        for (var i = 0; i < items.Count; i++)
        {
            steps++;
            if (items[i] == target)
                return (i, steps);
        }

        return (-1, steps);
    }

    /// <summary>
    /// Works on a sorted list: looks at the middle and discards half of the
    /// remaining items at every step. Worst case: about log2(n) steps → O(log n).
    /// </summary>
    public static (int Index, int Steps) BinarySearch(IReadOnlyList<int> sortedItems, int target)
    {
        var steps = 0;
        var low = 0;
        var high = sortedItems.Count - 1;

        while (low <= high)
        {
            steps++;

            // Written this way instead of (low + high) / 2 to avoid overflow with huge lists
            var middle = low + (high - low) / 2;

            if (sortedItems[middle] == target)
                return (middle, steps);

            if (sortedItems[middle] < target)
                low = middle + 1;   // the target can only be in the right half
            else
                high = middle - 1;  // the target can only be in the left half
        }

        return (-1, steps);
    }
}

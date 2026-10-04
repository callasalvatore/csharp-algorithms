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
    /// <param name="items">The values to search, in any order.</param>
    /// <param name="target">The value to look for.</param>
    /// <returns>
    /// <c>Index</c>: the position of the first item equal to <paramref name="target"/>, or -1 if there is none.
    /// <c>Steps</c>: how many items were checked.
    /// </returns>
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
    /// <param name="sortedItems">The values to search, sorted in ascending order. If they aren't sorted, the result is meaningless.</param>
    /// <param name="target">The value to look for.</param>
    /// <returns>
    /// <c>Index</c>: the position of an item equal to <paramref name="target"/>, or -1 if there is none.
    /// <c>Steps</c>: how many items were checked.
    /// </returns>
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

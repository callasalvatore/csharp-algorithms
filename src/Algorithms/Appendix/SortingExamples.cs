namespace Algorithms.Appendix;

/// <summary>
/// Appendix G: two classic ways to sort, returning how many comparisons they made
/// so their growth can be measured. For real code, use Array.Sort, List.Sort or OrderBy.
/// </summary>
public static class SortingExamples
{
    /// <summary>
    /// Insertion sort: takes the items one by one and slides each into its place among the
    /// ones already sorted, like sorting playing cards in your hand.
    /// Time O(n²) in the worst case (reversed input), O(n) if the input is already sorted. Space O(n) for the copy.
    /// </summary>
    /// <param name="items">The values to sort. The array is not modified: the method sorts a copy.</param>
    /// <returns>
    /// <c>Sorted</c>: a new array with the same values in ascending order.
    /// <c>Comparisons</c>: how many times two values were compared.
    /// </returns>
    public static (int[] Sorted, long Comparisons) InsertionSort(IReadOnlyList<int> items)
    {
        var sorted = items.ToArray();
        long comparisons = 0;

        for (var i = 1; i < sorted.Length; i++)
        {
            var current = sorted[i];
            var j = i - 1;

            // Shift the bigger sorted items one place to the right, to make room for "current"
            while (j >= 0)
            {
                comparisons++;
                if (sorted[j] <= current)
                    break;

                sorted[j + 1] = sorted[j];
                j--;
            }

            sorted[j + 1] = current;
        }

        return (sorted, comparisons);
    }

    /// <summary>
    /// Merge sort: splits the items in two halves, sorts each half the same way,
    /// then merges the two sorted halves. Time O(n log n) in every case, space O(n).
    /// </summary>
    /// <param name="items">The values to sort. The array is not modified: the method sorts a copy.</param>
    /// <returns>
    /// <c>Sorted</c>: a new array with the same values in ascending order.
    /// <c>Comparisons</c>: how many times two values were compared.
    /// </returns>
    public static (int[] Sorted, long Comparisons) MergeSort(IReadOnlyList<int> items)
    {
        long comparisons = 0;

        int[] Sort(int[] values)
        {
            // One item (or none) is already sorted: this is where the splitting stops
            if (values.Length <= 1)
                return values;

            var middle = values.Length / 2;
            var left = Sort(values[..middle]);
            var right = Sort(values[middle..]);

            return Merge(left, right);
        }

        int[] Merge(int[] left, int[] right)
        {
            var merged = new int[left.Length + right.Length];
            int l = 0, r = 0, m = 0;

            // Both halves are sorted: the smallest remaining item is always at the front of one of them
            while (l < left.Length && r < right.Length)
            {
                comparisons++;
                merged[m++] = left[l] <= right[r] ? left[l++] : right[r++];
            }

            // One half is finished: copy what's left of the other one
            while (l < left.Length)
                merged[m++] = left[l++];
            while (r < right.Length)
                merged[m++] = right[r++];

            return merged;
        }

        return (Sort(items.ToArray()), comparisons);
    }
}
